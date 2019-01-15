package com.inner.satisfaction.backend.cycle;

import static com.inner.satisfaction.backend.cycle.CycleState.OPENED;

import com.inner.satisfaction.backend.appointment.AppointmentPosition;
import com.inner.satisfaction.backend.appointment.AppointmentPositionService;
import com.inner.satisfaction.backend.appointment.AppointmentPositionState;
import com.inner.satisfaction.backend.base.BaseEntity;
import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.person.appointment.PersonAppointment;
import com.inner.satisfaction.backend.person.appointment.PersonAppointmentService;
import java.sql.Timestamp;
import java.util.Comparator;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import javax.transaction.Transactional;
import javax.validation.constraints.AssertFalse;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;
import org.springframework.util.Assert;
import org.springframework.util.CollectionUtils;

@Slf4j
@Service
public class CycleService extends BaseService<Cycle> {

  private final AppointmentPositionService appointmentPositionService;
  private final PersonAppointmentService personAppointmentService;

  protected CycleService(
    AppointmentPositionService appointmentPositionService,
    CycleRepository baseRepository,
    CycleValidation cycleValidation,
    PersonAppointmentService personAppointmentService) {
    super(baseRepository, cycleValidation);
    this.appointmentPositionService = appointmentPositionService;
    this.personAppointmentService = personAppointmentService;
  }

  @Transactional
  public void closeCycle(long cycleId) {
    Cycle one = findOne(cycleId);
    Assert.notNull(one, "Invalid Previous Cycle Given");

    // Setting every recommended to appointee
    List<AppointmentPosition> aps = appointmentPositionService.findByCycleId(cycleId);
    List<Long> apIds = aps.stream().map(BaseEntity::getId).collect(Collectors.toList());
    personAppointmentService.appointRecommendedPeople(apIds);

    // POST Actions
  }

  @Transactional
  public void openCycle(CycleCreateRequestDto cycleRequestDto) {
    Cycle one = findOne(cycleRequestDto.getPreviousCycleId());
    Assert.notNull(one, "Invalid Previous Cycle Given");

    cycleRequestDto.getCycleDetails().setState(OPENED);
    Cycle savedCycle = save(cycleRequestDto.getCycleDetails());
    List<AppointmentPosition> newPositions = appointmentPositionService
      .findByCycleId(cycleRequestDto.getPreviousCycleId())
      .stream()
      .map(ap -> copy(ap, savedCycle.getId(), cycleRequestDto.getStartDate()))
      .map(this::saveAppointmentPosition)
      .collect(Collectors.toList());

    // Previous Appointee as current Incumbtee
    for (AppointmentPosition ap : newPositions) {
      Optional<Long> incumbentId = fetchIncumbentId(cycleRequestDto.getPreviousCycleId(),
        ap.getInstitutionId(), ap.getPositionId(), ap.getSeatNo());
      // Fetch incumbent instead of id.
      if (incumbentId.isPresent()) {
        // increase count of reappointment
        personAppointmentService.save(
          PersonAppointment.builder()
            .appointmentPositionId(ap.getId())
            .personId(incumbentId.get())
            .isAppointed(false)
            .isRecommended(false)
            .priority(0)
            .build()
        );
      }
    }
  }

  private AppointmentPosition saveAppointmentPosition(AppointmentPosition appointmentPosition) {
    try {
      return appointmentPositionService.save(appointmentPosition);
    } catch (Exception e) {
      log
        .info("Unable to save this appointmentPosition Please Debug it {}", appointmentPosition, e);
      throw e;
    }
  }

  @Transactional
  public void dismissCycle(long cycleId) {
    // Dismiss cycle
  }

  /**
   * There exist a case where incumbent doesn't exist for a cycle.
   */
  private Optional<Long> fetchIncumbentId(long cycleId, long institutionId, long positionId,
    long seatNo) {
    List<AppointmentPosition> aps = appointmentPositionService
      .findByInstitutionIdAndSeatNoAndCycleIdAndPositionId(cycleId, institutionId, seatNo,
        positionId);

    List<AppointmentPosition> appointmentPositions = aps.stream()
      .sorted(Comparator.comparing(AppointmentPosition::getFrom))
      .collect(Collectors.toList());

    Assert.isTrue(!CollectionUtils.isEmpty(appointmentPositions),
      "No Appointment Positions in Cycle" + cycleId + ", institutionId " + institutionId
        + ", positionId " + positionId + ", seatNo " + seatNo);

    return Optional.ofNullable(appointmentPositions.get(appointmentPositions.size() - 1))
      .map(BaseEntity::getId)
      .map(personAppointmentService::findByAppointmentPositionIdAndIsAppointedTrue)
      .map(PersonAppointment::getPersonId);
  }

  private AppointmentPosition copy(AppointmentPosition appointmentPosition, long newCycleId,
    Timestamp from) {
    return AppointmentPosition.builder()
      .cycleId(newCycleId)
      .institutionId(appointmentPosition.getInstitutionId())
      .positionId(appointmentPosition.getPositionId())
      .seatNo(appointmentPosition.getSeatNo())
      .isMowlaAppointee(appointmentPosition.isMowlaAppointee())
      .nominationsRequired(appointmentPosition.getNominationsRequired())
      .from(from)
      .rank(appointmentPosition.getRank())
      .state(AppointmentPositionState.CREATED)
      .isActive(true)
      .build();
  }
}

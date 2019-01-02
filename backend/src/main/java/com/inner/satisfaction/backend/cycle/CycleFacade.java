package com.inner.satisfaction.backend.cycle;

import static com.inner.satisfaction.backend.cycle.CycleState.APPOINTED;
import static com.inner.satisfaction.backend.cycle.CycleState.MIDTERM;
import static com.inner.satisfaction.backend.cycle.CycleState.OPENED;

import com.inner.satisfaction.backend.appointment.AppointmentPosition;
import com.inner.satisfaction.backend.appointment.AppointmentPositionService;
import com.inner.satisfaction.backend.appointment.AppointmentPositionState;
import com.inner.satisfaction.backend.base.BaseEntity;
import com.inner.satisfaction.backend.person.appointment.PersonAppointment;
import com.inner.satisfaction.backend.person.appointment.PersonAppointmentService;
import java.sql.Timestamp;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import java.util.stream.Stream;
import javax.transaction.Transactional;
import org.springframework.stereotype.Component;
import org.springframework.util.Assert;

@Component
public class CycleFacade {

  private final CycleService cycleService;
  private final PersonAppointmentService personAppointmentService;
  private final AppointmentPositionService appointmentPositionService;

  public CycleFacade(CycleService cycleService,
    PersonAppointmentService personAppointmentService,
    AppointmentPositionService appointmentPositionService) {
    this.cycleService = cycleService;
    this.personAppointmentService = personAppointmentService;
    this.appointmentPositionService = appointmentPositionService;
  }

  public CycleSummaryDto getCycleSummary(long cycleId) {
    Cycle one = cycleService.findOne(cycleId);
    return CycleSummaryDto.builder()
      .nominatedPerson(Optional.ofNullable(one.getNominatedCount()).orElse(0l))
      .recommendedPerson(Optional.ofNullable(one.getRecommendedCount()).orElse(0l))
      .build();
  }

  @Transactional
  public void openMidtermAppointment(long cycleId) {
    Cycle cycle = cycleService.findOne(cycleId);
    if (cycle.getState() == null) {
      cycle.setState(CycleState.OPENED);
    }
    if (cycle.getState().equals(APPOINTED)) {
      cycle.setState(MIDTERM);
    } else {
      throw new RuntimeException("Lala, Midterm nahi khul sakta, state invalid error");
    }
    cycleService.save(cycle);
  }

  @Transactional
  public void appointInCycle(long cycleId) {
    Cycle cycle = cycleService.findOne(cycleId);
    Assert.notNull(cycle, "Invalid Cycle Id Provided");
    if (cycle.getState() == null) {
      cycle.setState(CycleState.OPENED);
      cycleService.save(cycle);
    }

    if (cycle.getState().equals(OPENED) || cycle.getState().equals(MIDTERM)) {
      List<AppointmentPosition> appointmentPositions = appointmentPositionService
        .fetchActiveAppointmentsForCycle(cycle.getId());
      // Some other checks should be there that if all positions are recommended or not.

      List<Long> appointmentPositionIds = appointmentPositions.stream()
        .filter(
          appointmentPosition -> appointmentPosition.getState() == null || appointmentPosition.getState().equals(AppointmentPositionState.CREATED))
        .map(BaseEntity::getId)
        .collect(Collectors.toList());

      // There are positions to work on
      if(appointmentPositionIds.size() > 0) {
        int recommendedPersons = personAppointmentService
          .findRecommendedCountInAppointmentPositionIds(appointmentPositionIds);

        if (appointmentPositions.size() > recommendedPersons) {
          throw new RuntimeException("Recommendations are less than positions");
        }
      } else {
        throw new RuntimeException("Something is wrong, no appointment position in cycle");
      }

      appointmentPositionIds.stream().forEach((id) ->  {
        personAppointmentService.appointRecommendedPeople(id);
        appointmentPositionService.updateState(id, AppointmentPositionState.APPOINTED);
      });

      // Change State of Cycle
      cycle.setState(CycleState.APPOINTED);
    } else {
      throw new RuntimeException("Cycle state is incorrect, should be either opened / midterm");
    }
    cycleService.save(cycle);
  }

  @Transactional
  public void closeCycle(long cycleId, Timestamp endDate) {
    Cycle cycle = cycleService.findOne(cycleId);
    Assert.notNull(cycle, "Invalid Cycle Id Provided");

    if (!cycle.getState().equals(CycleState.APPOINTED)) {
      throw new RuntimeException("Invalid cycle state, should be appointed before close");
    }

    List<AppointmentPosition> appointmentPositions = appointmentPositionService
      .fetchActiveAppointmentsForCycle(cycle.getId());
    appointmentPositions.stream().map(ap -> {
      ap.setTo(endDate);
      return ap;
    }).forEach(appointmentPositionService::save);

    cycle.setState(CycleState.CLOSED);
    cycleService.save(cycle);
  }

  private AppointmentPosition changeAppointmentPositionStateToAppointed(
    AppointmentPosition appointmentPosition) {
    appointmentPosition.setState(AppointmentPositionState.APPOINTED);
    return appointmentPosition;
  }
}
package com.inner.satisfaction.backend.appointment;

import com.google.common.collect.Lists;
import com.inner.satisfaction.backend.base.BaseEntity;
import com.inner.satisfaction.backend.cycle.Cycle;
import com.inner.satisfaction.backend.cycle.CycleService;
import com.inner.satisfaction.backend.cycle.CycleState;
import com.inner.satisfaction.backend.institution.InstitutionService;
import com.inner.satisfaction.backend.person.PersonService;
import com.inner.satisfaction.backend.person.appointment.PersonAppointment;
import com.inner.satisfaction.backend.person.appointment.PersonAppointmentDto;
import com.inner.satisfaction.backend.person.appointment.PersonAppointmentService;
import com.inner.satisfaction.backend.position.PositionService;
import java.sql.Timestamp;
import java.util.List;
import java.util.stream.Collectors;
import org.springframework.stereotype.Component;
import org.springframework.util.Assert;

@Component
public class AppointmentPositionFacade {

  private final AppointmentPositionService appointmentPositionService;
  private final PersonAppointmentService personAppointmentService;
  private final CycleService cycleService;
  private final PositionService positionService;
  private final InstitutionService institutionService;
  private final PersonService personService;

  public AppointmentPositionFacade(
    AppointmentPositionService appointmentPositionService,
    PersonAppointmentService personAppointmentService,
    CycleService cycleService,
    PositionService positionService,
    InstitutionService institutionService,
    PersonService personService) {
    this.appointmentPositionService = appointmentPositionService;
    this.personAppointmentService = personAppointmentService;
    this.cycleService = cycleService;
    this.positionService = positionService;
    this.institutionService = institutionService;
    this.personService = personService;
  }

  public List<ApptPositionDto> findByCycleIdAndInstitutionId(long cycleId,
    long institutionId) {
    return appointmentPositionService.findByCycleIdAndInstitutionId(cycleId, institutionId)
      .stream()
      .map(this::convertToApptPositionDto)
      .collect(Collectors.toList());
  }

  public List<AppointmentPositionDto> findAppointmentsOfPersonIdAndIsMowlaAppointee(
    long personId, boolean isMowlaAppointee) {
    return personAppointmentService.findAppointmentsOfPerson(personId)
      .stream()
      .map(PersonAppointment::getAppointmentPositionId)
      .map(appointmentPositionService::findOne)
      .filter((o) -> o.isMowlaAppointee() == isMowlaAppointee)
      .map(this::convert)
      .collect(Collectors.toList());
  }

  public ApptPositionDto findByCycleIdAndInstitutionIdPositionIdAndSeatNo(long cycleId,
    long institutionId, long positionId, long seatId) {
    return appointmentPositionService
      .findByCycleIdAndInstitutionIdAndPositionIdAndSeatNo(cycleId, institutionId, positionId,
        seatId)
      .stream()
      .map(this::convertToApptPositionDto)
      .findFirst()
      .orElseThrow(() -> new RuntimeException(
        "Lala !! Combination of cycle, institution, position and seat given doesn't exist"));
  }

  private ApptPositionDto convertToApptPositionDto(AppointmentPosition appointmentPosition) {
    return ApptPositionDto.builder()
      .appointmentPositionId(appointmentPosition.getId())
      .cycle(cycleService.findOne(appointmentPosition.getCycleId()))
      .institution(institutionService.findOne(appointmentPosition.getInstitutionId()))
      .position(positionService.findOne(appointmentPosition.getPositionId()))
      .seatId(appointmentPosition.getSeatNo())
      .personAppointmentList(fetchPersonAppointments(appointmentPosition.getId()))
      .nominationsRequired(appointmentPosition.getNominationsRequired())
      .isMowlaAppointee(appointmentPosition.isMowlaAppointee())
      .rank(appointmentPosition.getRank())
      .isActive(appointmentPosition.isActive())
      .from(appointmentPosition.getFrom())
      .to(appointmentPosition.getTo())
      .state(appointmentPosition.getState())
      .build();
  }

  private List<PersonAppointmentDto> fetchPersonAppointments(long appointmentPositionId) {
    return personAppointmentService
      .findByAppointmentPositionId(appointmentPositionId)
      .stream()
      .map(this::convert)
      .collect(Collectors.toList());
  }

  private PersonAppointmentDto convert(PersonAppointment personAppointment) {
    return PersonAppointmentDto
      .builder()
      .isAppointed(personAppointment.getAppointed())
      .isRecommended(personAppointment.getRecommended())
      .priority(personAppointment.getPriority())
      .remarks(personAppointment.getRemarks())
      .personAppointmentId(personAppointment.getId())
      .person(personService.findOne(personAppointment.getPersonId()))
      .build();
  }

  private AppointmentPositionDto convert(AppointmentPosition appointmentPosition) {
    return AppointmentPositionDto.builder()
      .id(appointmentPosition.getId())
      .cycleId(cycleService.findOne(appointmentPosition.getCycleId()))
      .institution(institutionService.findOne(appointmentPosition.getInstitutionId()))
      .isActive(appointmentPosition.isActive())
      .isMowlaAppointee(appointmentPosition.isMowlaAppointee())
      .nominationsRequired(appointmentPosition.getNominationsRequired())
      .position(positionService.findOne(appointmentPosition.getPositionId()))
      .seatNo(appointmentPosition.getSeatNo())
      .from(appointmentPosition.getFrom())
      .to(appointmentPosition.getTo())
      .state(appointmentPosition.getState())
      .build();
  }

  public List<AppointmentPosition> createMidtermPosition(
    MidtermPositionCreateRequestDto requestDto) {
    long cycleId = requestDto.getCycleId();
    Timestamp startdate = requestDto.getMidtermPositionStartdate();
    List<Long> apptPositionIds = requestDto.getAppointmentPositionIds();

    Cycle one = getVerifiedCycle(cycleId);
    if (!one.getState().equals(CycleState.MIDTERM)) {
      throw new RuntimeException("Expected Cycle State to Midterm, but it isn't");
    }

    /**
     * It Does the Following in order
     * 1. Update End Date and Retire Old Positions
     * 2. It Create New Positions (Based on Some previous value, end date of previous one is start date of new one
     * 3. Return those values
     */
    return apptPositionIds.stream()
      .map(appointmentPositionService::findOne)
      // Validation should only create those positions who are appointed state.
      .filter(a -> a.getState().equals(AppointmentPositionState.APPOINTED))
      .map(ap -> {
        ap.setTo(startdate);
        ap.setState(AppointmentPositionState.RETIRED);
        ap.setIsActive(Boolean.FALSE);
        return ap;
      })
      .map(appointmentPositionService::save)
      .map(ap -> AppointmentPosition.builder()
        .from(startdate)
        .state(AppointmentPositionState.CREATED)
        .seatNo(ap.getSeatNo())
        .positionId(ap.getPositionId())
        .cycleId(ap.getCycleId())
        .institutionId(ap.getInstitutionId())
        .rank(ap.getRank())
        .isActive(Boolean.TRUE)
        .nominationsRequired(ap.getNominationsRequired())
        .isMowlaAppointee(ap.isMowlaAppointee())
        .build()
      ).map(appointmentPositionService::save)
      .collect(Collectors.toList());
  }

  private Cycle getVerifiedCycle(long cycleId) {
    Cycle cycle = cycleService.findOne(cycleId);
    Assert.notNull(cycle, "Invalid cycle id");
    return cycle;
  }

  public AppointmentPosition save(AppointmentPosition appointmentPosition) {
    Cycle cycle = getVerifiedCycle(appointmentPosition.getCycleId());
    if (!cycle.getState().equals(CycleState.OPENED)) {
      throw new RuntimeException("Incorrect Cycle State");
    }

    Assert.notNull(appointmentPosition.getFrom(), "Invalid from field");
    if (appointmentPosition.getRank() == null) {
      appointmentPosition.setRank(1); // Order to display
    }
    if (appointmentPosition.getState() == null) {
      appointmentPosition.setState(AppointmentPositionState.CREATED);
    }
    return appointmentPositionService.save(appointmentPosition);
  }

  public List<ApptPositionDto> findByCycleIdWhereNoOneIsRecommended(long cycleId) {
    return appointmentPositionService.findByCycleIdWhereNoOneIsRecommended(cycleId)
      .stream()
      .map(this::convertToApptPositionDto)
      .collect(Collectors.toList());
  }
}

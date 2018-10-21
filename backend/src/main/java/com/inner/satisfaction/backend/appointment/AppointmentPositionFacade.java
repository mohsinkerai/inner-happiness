package com.inner.satisfaction.backend.appointment;

import com.inner.satisfaction.backend.cycle.CycleService;
import com.inner.satisfaction.backend.institution.InstitutionService;
import com.inner.satisfaction.backend.person.PersonService;
import com.inner.satisfaction.backend.person.appointment.PersonAppointment;
import com.inner.satisfaction.backend.person.appointment.PersonAppointmentDto;
import com.inner.satisfaction.backend.person.appointment.PersonAppointmentService;
import com.inner.satisfaction.backend.position.PositionService;
import java.util.List;
import java.util.stream.Collectors;
import org.springframework.stereotype.Component;

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
    return appointmentPositionService.findByCycleIdAndInstitutionIdAndPositionIdAndSeatNo(cycleId, institutionId, positionId, seatId)
      .stream()
      .map(this::convertToApptPositionDto)
      .findFirst()
      .orElseThrow(() -> new RuntimeException("Lala !! Combination of cycle, institution, position and seat given doesn't exist"));
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
      .build();
  }
}

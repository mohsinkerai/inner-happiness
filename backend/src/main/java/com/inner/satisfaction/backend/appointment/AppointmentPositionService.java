package com.inner.satisfaction.backend.appointment;

import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.cycle.CycleService;
import com.inner.satisfaction.backend.institution.InstitutionService;
import com.inner.satisfaction.backend.person.PersonService;
import com.inner.satisfaction.backend.person.appointment.PersonAppointment;
import com.inner.satisfaction.backend.person.appointment.PersonAppointmentDto;
import com.inner.satisfaction.backend.person.appointment.PersonAppointmentService;
import com.inner.satisfaction.backend.position.PositionService;
import java.util.List;
import java.util.stream.Collectors;
import org.springframework.stereotype.Service;

@Service
public class AppointmentPositionService extends BaseService<AppointmentPosition> {

  private final AppointmentPositionRepository appointmentPositionRepository;

  private final CycleService cycleService;
  private final PersonService personService;
  private final PositionService positionService;
  private final InstitutionService institutionService;
  private final PersonAppointmentService personAppointmentService;

  protected AppointmentPositionService(
    AppointmentPositionRepository baseRepository,
    AppointmentPositionValidation appointmentPositionValidation,
    CycleService cycleService,
    PersonService personService,
    PositionService positionService,
    InstitutionService institutionService,
    PersonAppointmentService personAppointmentService) {
    super(baseRepository, appointmentPositionValidation);
    this.cycleService = cycleService;
    this.personService = personService;
    this.positionService = positionService;
    this.institutionService = institutionService;
    this.personAppointmentService = personAppointmentService;
    this.appointmentPositionRepository = baseRepository;
  }

  public List<AppointmentPositionDto> findAppointmentsOfPersonIdAndIsMowlaAppointee(
    long personId, boolean isMowlaAppointee) {
    return personAppointmentService.findAppointmentsOfPerson(personId)
      .stream()
      .map(PersonAppointment::getAppointmentPositionId)
      .map(this::findOne)
      .filter((o) -> o.isMowlaAppointee() == isMowlaAppointee)
      .map(this::convert)
      .collect(Collectors.toList());
  }

  public List<ApptPositionDto> findByCycleIdAndInstitutionId(long cycleId,
    long institutionId) {
    return appointmentPositionRepository.findByCycleIdAndInstitutionId(cycleId, institutionId)
      .stream()
      .map(this::convertToApptPositionDto)
      .collect(Collectors.toList());
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

  private ApptPositionDto convertToApptPositionDto(AppointmentPosition appointmentPosition) {
    return ApptPositionDto.builder()
      .cycle(cycleService.findOne(appointmentPosition.getCycleId()))
      .institution(institutionService.findOne(appointmentPosition.getInstitutionId()))
      .position(positionService.findOne(appointmentPosition.getPositionId()))
      .seatId(appointmentPosition.getSeatNo())
      .personAppointmentList(fetchPersonAppointments(appointmentPosition.getId()))
      .nominationsRequired(appointmentPosition.getNominationsRequired())
      .isMowlaAppointee(appointmentPosition.isMowlaAppointee())
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
      .isAppointed(personAppointment.isAppointed())
      .isRecommended(personAppointment.isRecommended())
      .priority(personAppointment.getPriority())
      .remarks(personAppointment.getRemarks())
      .person(personService.findOne(personAppointment.getId()))
      .build();
  }
}

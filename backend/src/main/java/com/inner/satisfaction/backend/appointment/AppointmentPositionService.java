package com.inner.satisfaction.backend.appointment;

import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.person.appointment.PersonAppointment;
import com.inner.satisfaction.backend.person.appointment.PersonAppointmentService;
import java.util.List;
import java.util.stream.Collectors;
import org.springframework.stereotype.Service;

@Service
public class AppointmentPositionService extends BaseService<AppointmentPosition> {

  private final PersonAppointmentService personAppointmentService;

  protected AppointmentPositionService(
    AppointmentPositionRepository baseRepository,
    AppointmentPositionValidation appointmentPositionValidation,
    PersonAppointmentService personAppointmentService) {
    super(baseRepository, appointmentPositionValidation);
    this.personAppointmentService = personAppointmentService;
  }

  public List<AppointmentPosition> findAppointmentsOfPersonIdAndIsMowlaAppointee(
    long personId, boolean isMowlaAppointee) {
    return personAppointmentService.findAppointmentsOfPerson(personId)
      .stream()
      .map(PersonAppointment::getCpiId)
      .map(this::findOne)
      .filter((o) -> o.isMowlaAppointee() == isMowlaAppointee)
      .collect(Collectors.toList());
  }
}

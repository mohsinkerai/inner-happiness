package com.inner.satisfaction.backend.person.appointment;

import org.springframework.stereotype.Component;

@Component
public class PersonAppointmentFacade {

  private final PersonAppointmentService personAppointmentService;

  public PersonAppointmentFacade(
    PersonAppointmentService personAppointmentService) {
    this.personAppointmentService = personAppointmentService;
  }

  public PersonAppointment save(PersonAppointment personAppointment) {
    return personAppointmentService.save(personAppointment);
  }
}

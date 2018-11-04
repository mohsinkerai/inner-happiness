package com.inner.satisfaction.backend.person.appointment;

import com.inner.satisfaction.backend.person.Person;
import com.inner.satisfaction.backend.person.PersonService;
import org.springframework.stereotype.Component;

@Component
public class PersonAppointmentFacade {

  private final PersonAppointmentService personAppointmentService;
  private final PersonService personService;

  public PersonAppointmentFacade(
    PersonAppointmentService personAppointmentService,
    PersonService personService) {
    this.personAppointmentService = personAppointmentService;
    this.personService = personService;
  }

  public PersonAppointment save(PersonAppointment personAppointment) {
    if (personAppointment.getId() != null) {
      performOnlyUpdateValidations(personAppointment);
    } else {
      performOnlyCreateValidations(personAppointment);
    }
    performValidations(personAppointment);
    return personAppointmentService.save(personAppointment);
  }

  private void performOnlyCreateValidations(PersonAppointment personAppointment) {
    if (personAppointment.getReappointmentCount() == null) {
      personAppointment.setReappointmentCount(0);
    }
    if (!personAppointment.getReappointmentCount().equals(0)) {
      throw new RuntimeException(
        "Larkay!! re-appointment ka count tera-bhai dekh lega, tu mat dal");
    }
  }

  private void performValidations(PersonAppointment personAppointment) {
    if (personAppointment.getPriority() == 0) {
      throw new RuntimeException("Excuseme!! you can't make new incumbtee or update incumbtee");
    }
    if (personAppointment.getAppointed() == true) {
      throw new RuntimeException(
        "Please come low!! Appoint ku ker rahe ho? ya appointed ko edit ku ker rahe ho?");
    }
    Person one = personService.findOne(personAppointment.getPersonId());
    if (one == null && one.isActive() == true) {
      throw new RuntimeException("Jani!! Sahi person id dedo");
    }
  }

  private void performOnlyUpdateValidations(PersonAppointment personAppointment) {
    Long id = personAppointment.getId();
    PersonAppointment dbPersonAppointment = personAppointmentService.findOne(id);
    if (dbPersonAppointment.getPriority() == 0) {
      throw new RuntimeException("Excuseme!! you can't update incumbtee");
    }
  }
}

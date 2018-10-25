package com.inner.satisfaction.backend.person.appointment.validations;

import com.inner.satisfaction.backend.person.appointment.PersonAppointment;

public interface PersonAppointmentValidation {

  /**
   * Validate appointment of a person
   */
  void validate(PersonAppointment personAppointment) throws PersonAppointmentValidationException;

  /**
   * returns validation name
   */
  String getName();
}
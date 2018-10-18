package com.inner.satisfaction.backend.person.appointment.validations;

import com.inner.satisfaction.backend.person.appointment.PersonAppointment;

public interface PersonAppointmentValidation {

  void validate(PersonAppointment personAppointment) throws PersonAppointmentValidationException;
}
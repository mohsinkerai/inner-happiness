package com.inner.satisfaction.backend.person.appointment.validation;

import com.inner.satisfaction.backend.person.appointment.PersonAppointment;
import com.inner.satisfaction.backend.person.appointment.validation.message.ValidationMessage;
import java.util.List;
import java.util.Optional;

public interface PersonAppointmentValidation {

  /**
   * Validate appointment of a person
   */
  Optional<ValidationMessage> validate(PersonAppointment personAppointment);

  /**
   * returns impl name
   */
  String getName();
}
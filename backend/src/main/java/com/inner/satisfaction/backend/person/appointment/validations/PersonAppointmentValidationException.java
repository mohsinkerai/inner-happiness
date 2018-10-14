package com.inner.satisfaction.backend.person.appointment.validations;

import java.util.List;
import lombok.EqualsAndHashCode;
import lombok.Getter;
import lombok.ToString;

@Getter
@ToString
@EqualsAndHashCode
public class PersonAppointmentValidationException extends Exception {

  private final List<String> errors;

  public PersonAppointmentValidationException(List<String> errors) {
    super(String.join(",", errors));
    this.errors = errors;
  }
}

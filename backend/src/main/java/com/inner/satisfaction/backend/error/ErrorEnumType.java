package com.inner.satisfaction.backend.error;

import lombok.AllArgsConstructor;
import lombok.Getter;

@AllArgsConstructor
@Getter
public enum ErrorEnumType {

  INCUMBENT_CAN_NOT_BE_UPDATED_OR_CREATED(1, "Incumbent can not be updated or created through program."),
  PERSON_DOES_NOT_EXIST_IN_DB(3, "Person is either inactive or invalid."),
  CAN_NOT_NOMINATE_WITH_IS_APPOINTED_TRUE(2, "Can not nominate a person with given appointment. Please recommend it first."),
  APPOINTMENT_POSITION_DOES_NOT_EXIST(4, "Appointment position does not exist");

  private final int errorCode;
  private final String message;
}

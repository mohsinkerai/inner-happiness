package com.inner.satisfaction.backend.error;

import lombok.AllArgsConstructor;
import lombok.Getter;

@AllArgsConstructor
@Getter
public enum ErrorEnumType {

  AS_INCUMBENT_CAN_NOT_BE_UPDATED_OR_CREATED(1, "As incumbent can not be updated or created through program."),
  PERSON_DOES_NOT_EXIST_IN_DB(3, "Person is either inactive or invalid."),
  CAN_NOT_NOMINATE_WITH_IS_APPOINTED_TRUE(2, "Can not nominate a person with given appointment. Please recommend it first."),
  APPOINTMENT_POSITION_DOES_NOT_EXIST(4, "Appointment position does not exist"),
  INCUMBENT_INFORMATION_CAN_NOT_BE_UPDATED(5, "You can't update information of existing incumbent"),
  INVALID_REAPPOINTMENT_COUNT_PROVIDED(6, "Reappointment count should be 0 when creating or updating position"),
  PERSON_APPOINTMENT_DOES_NOT_EXIST(7, "Person Appointment that is tried to modified doesn't exist"),

  INVALID_INSTITUTION_CATEGORY_GIVEN(101, "Invalid Institution Category Provided, Possible values are CAB, ITREB & COUNCIL"),

  PERSON_ALREADY_RECOMMENDED(1001, "Person is already recommended at ")
  ;


  /**
   * 1001 is reserved for person already recommended
   */
  private final int errorCode;
  private final String message;
}

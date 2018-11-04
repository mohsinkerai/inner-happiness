package com.inner.satisfaction.backend.person.appointment.validation.impl;

import com.inner.satisfaction.backend.appointment.AppointmentPosition;
import com.inner.satisfaction.backend.appointment.AppointmentPositionService;
import com.inner.satisfaction.backend.person.appointment.PersonAppointment;
import com.inner.satisfaction.backend.person.appointment.validation.PersonAppointmentValidation;
import com.inner.satisfaction.backend.person.appointment.validation.message.ValidationMessage;
import com.inner.satisfaction.backend.person.lookup.relation.PersonRelationPersonService;
import java.util.Optional;
import org.springframework.stereotype.Component;

@Component
public class ThirdTimeRecommendationValidation implements PersonAppointmentValidation {

  private static final String VALIDATION_NAME = "PERSON_THIRD_TIME_RECOMMENDED";
  private static final String MESSAGE = "This person %d is being recommended third time";
  private static final boolean BLOCKING_VALIDATION = false;

  private final AppointmentPositionService appointmentPositionService;
  private final PersonRelationPersonService personRelationPersonService;

  public ThirdTimeRecommendationValidation(
    AppointmentPositionService appointmentPositionService,
    PersonRelationPersonService personRelationPersonService) {
    this.appointmentPositionService = appointmentPositionService;
    this.personRelationPersonService = personRelationPersonService;
  }

  @Override
  public Optional<ValidationMessage> validate(PersonAppointment personAppointment) {
    if (personAppointment.getRecommended() == true && personAppointment.getPriority() == 0
      && personAppointment.getReappointmentCount() == 2) {

      String errorMessage = String.format(MESSAGE, personAppointment.getPersonId());

      AppointmentPosition appointmentPosition = appointmentPositionService
        .findOne(personAppointment.getAppointmentPositionId());

      ValidationMessage validationMessage = ValidationMessage.builder()
        .isActive(true)
        .isRequired(BLOCKING_VALIDATION)
        .isResolved(false)
        .appointmentPositionId(appointmentPosition.getId())
        .cycleId(appointmentPosition.getCycleId())
        .personId(personAppointment.getPersonId())
        .code(VALIDATION_NAME)
        .message(errorMessage)
        .build();

      return Optional.of(validationMessage);
    } else {
      return Optional.empty();
    }
  }

  @Override
  public String getName() {
    return VALIDATION_NAME;
  }
}

package com.inner.satisfaction.backend.person.appointment.validation.impl;

import com.google.common.collect.Lists;
import com.inner.satisfaction.backend.appointment.AppointmentPosition;
import com.inner.satisfaction.backend.appointment.AppointmentPositionService;
import com.inner.satisfaction.backend.base.BaseEntity;
import com.inner.satisfaction.backend.person.appointment.PersonAppointment;
import com.inner.satisfaction.backend.person.appointment.validation.PersonAppointmentValidation;
import com.inner.satisfaction.backend.person.appointment.validation.message.ValidationMessage;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import org.springframework.stereotype.Component;

@Component
public class MultiplePersonRecommendationValidation implements PersonAppointmentValidation {

  private static final String VALIDATION_NAME = "SHOULD_NOT_RECOMMEND_MULTIPLE_TIMES";
  private static final String MESSAGE = "Person %d is recommended multiple times in %s";
  private static final boolean BLOCKING_VALIDATION = false;

  private final AppointmentPositionService appointmentPositionService;

  public MultiplePersonRecommendationValidation(
    AppointmentPositionService appointmentPositionService) {
    this.appointmentPositionService = appointmentPositionService;
  }

  @Override
  public Optional<ValidationMessage> validate(PersonAppointment personAppointment) {
    if (personAppointment.getRecommended() == true) {
      final long personId = personAppointment.getPersonId();
      final long appointmentPositionId = personAppointment.getAppointmentPositionId();

      AppointmentPosition appointmentPosition = appointmentPositionService
        .findOne(appointmentPositionId);

      List<AppointmentPosition> appointmentPositions = appointmentPositionService
        .findAppointmentsOfPersonInCycle(personId, appointmentPosition.getCycleId());

      List<AppointmentPosition> alreadyAppointedPositions = Lists.newArrayList();

      for (AppointmentPosition ap : appointmentPositions) {
        if (!ap.getId().equals(personAppointment.getAppointmentPositionId())) {
          alreadyAppointedPositions.add(ap);
        }
      }

      String errorMessage = String.format(MESSAGE, personId,
        alreadyAppointedPositions.stream().map(BaseEntity::getId).collect(
          Collectors.toList()));

      ValidationMessage validationMessage = ValidationMessage.builder()
        .isActive(true)
        .isRequired(BLOCKING_VALIDATION)
        .isResolved(false)
        .appointmentPositionId(appointmentPositionId)
        .cycleId(appointmentPosition.getCycleId())
        .personId(personId)
        .code(VALIDATION_NAME)
        .message(errorMessage)
        .build();

      return Optional.ofNullable(validationMessage);
    } else {
      return Optional.empty();
    }
  }

  @Override
  public String getName() {
    return VALIDATION_NAME;
  }
}

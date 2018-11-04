package com.inner.satisfaction.backend.person.appointment.validation.impl;

import com.google.common.collect.Lists;
import com.inner.satisfaction.backend.appointment.AppointmentPosition;
import com.inner.satisfaction.backend.appointment.AppointmentPositionService;
import com.inner.satisfaction.backend.base.BaseEntity;
import com.inner.satisfaction.backend.person.appointment.PersonAppointment;
import com.inner.satisfaction.backend.person.appointment.validation.PersonAppointmentValidation;
import com.inner.satisfaction.backend.person.appointment.validation.message.ValidationMessage;
import com.inner.satisfaction.backend.person.lookup.relation.PersonRelationPerson;
import com.inner.satisfaction.backend.person.lookup.relation.PersonRelationPersonService;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import org.springframework.stereotype.Component;

@Component
public class BloodRelationRecommendationValidation implements PersonAppointmentValidation {

  private static final String VALIDATION_NAME = "PERSON_RELATIVE_RECOMMENDED";
  private static final String MESSAGE = "Person %d's rishtedaars are recommended in %s";
  private static final boolean BLOCKING_VALIDATION = false;

  private final AppointmentPositionService appointmentPositionService;
  private final PersonRelationPersonService personRelationPersonService;

  public BloodRelationRecommendationValidation(
    AppointmentPositionService appointmentPositionService,
    PersonRelationPersonService personRelationPersonService) {
    this.appointmentPositionService = appointmentPositionService;
    this.personRelationPersonService = personRelationPersonService;
  }

  @Override
  public Optional<ValidationMessage> validate(PersonAppointment personAppointment) {
    if (personAppointment.getRecommended() == true) {
      final long personId = personAppointment.getPersonId();
      final long appointmentPositionId = personAppointment.getAppointmentPositionId();

      AppointmentPosition appointmentPosition = appointmentPositionService
        .findOne(appointmentPositionId);

      List<PersonRelationPerson> relations = personRelationPersonService
        .findByFirstPersonId(personId);
      List<AppointmentPosition> alreadyAppointedPositions = Lists.newArrayList();

      for(PersonRelationPerson prp : relations) {
        long rishtedaar = prp.getSecondPersonId();

        List<AppointmentPosition> appointmentPositions = appointmentPositionService
          .findAppointmentsOfPersonInCycle(personId, appointmentPosition.getCycleId());
        // Check if rishtedaar has any appointment

        for (AppointmentPosition ap : appointmentPositions) {
          if (!ap.getId().equals(personAppointment.getAppointmentPositionId())) {
            alreadyAppointedPositions.add(ap);
          }
        }
      }

      List<Long> appointmentPositionIds = alreadyAppointedPositions.stream().map(BaseEntity::getId).collect(
        Collectors.toList());

      String errorMessage = String.format(MESSAGE, personId, appointmentPositionIds);

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

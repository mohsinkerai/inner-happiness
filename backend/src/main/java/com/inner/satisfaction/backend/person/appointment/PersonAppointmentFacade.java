package com.inner.satisfaction.backend.person.appointment;

import com.inner.satisfaction.backend.appointment.AppointmentPosition;
import com.inner.satisfaction.backend.appointment.AppointmentPositionService;
import com.inner.satisfaction.backend.appointment.AppointmentPositionState;
import com.inner.satisfaction.backend.cycle.Cycle;
import com.inner.satisfaction.backend.cycle.CycleService;
import com.inner.satisfaction.backend.person.Person;
import com.inner.satisfaction.backend.person.PersonService;
import com.inner.satisfaction.backend.person.appointment.event.PersonRecommendedEventDto;
import java.util.List;
import java.util.stream.Collectors;
import javax.transaction.Transactional;
import lombok.extern.slf4j.Slf4j;
import org.springframework.context.ApplicationEventPublisher;
import org.springframework.stereotype.Component;
import org.springframework.util.Assert;
import org.springframework.util.CollectionUtils;

/**
 * Move it to event driven, i.e. application event.
 */
@Slf4j
@Component
public class PersonAppointmentFacade {

  private final CycleService cycleService;
  private final PersonService personService;
  private final PersonAppointmentService personAppointmentService;
  private final AppointmentPositionService appointmentPositionService;
  private final ApplicationEventPublisher applicationEventPublisher;

  public PersonAppointmentFacade(
    CycleService cycleService,
    AppointmentPositionService appointmentPositionService,
    PersonAppointmentService personAppointmentService,
    PersonService personService,
    ApplicationEventPublisher applicationEventPublisher) {
    this.cycleService = cycleService;
    this.appointmentPositionService = appointmentPositionService;
    this.personAppointmentService = personAppointmentService;
    this.personService = personService;
    this.applicationEventPublisher = applicationEventPublisher;
  }

  public PersonAppointment save(PersonAppointment personAppointment) {
    performValidations(personAppointment);
    if (personAppointment.getId() != null) {
      performOnlyUpdateValidations(personAppointment);
      return updatePersonAppointment(personAppointment);
    } else {
      performOnlyCreateValidations(personAppointment);
      return savePersonAppointment(personAppointment);
    }
  }

  @Transactional
  public void delete(Long entityId) {
    PersonAppointment personAppointment = personAppointmentService.findOne(entityId);
    AppointmentPosition appointmentPosition = appointmentPositionService
      .findOne(personAppointment.getAppointmentPositionId());
    Cycle cycle = cycleService.findOne(appointmentPosition.getCycleId());
    if (personAppointment.getRecommended()) {
      cycle = adjustRecommendedCount(cycle, false);
    }
    cycle = adjustNominationCount(cycle, false);
    cycleService.save(cycle);
    personAppointmentService.delete(personAppointment);
  }

  private PersonAppointment updatePersonAppointment(PersonAppointment personAppointment) {
    PersonAppointment existingPersonAppointment = personAppointmentService
      .findOne(personAppointment.getId());
    AppointmentPosition appointmentPosition = appointmentPositionService
      .findOne(personAppointment.getAppointmentPositionId());
    Cycle cycle = cycleService.findOne(appointmentPosition.getCycleId());
    if (existingPersonAppointment.getRecommended() && !personAppointment.getRecommended()) {
      cycle = adjustRecommendedCount(cycle, false);
    } else if (!existingPersonAppointment.getRecommended() && personAppointment.getRecommended()) {
      cycle = adjustRecommendedCount(cycle, true);
    }
    cycleService.save(cycle);

    removeExistingRecommendation(personAppointment);
    return personAppointmentService.save(personAppointment);
  }

  private PersonAppointment savePersonAppointment(PersonAppointment personAppointment) {
    /**
     * Adjust Appointment Position Count (Can be done async)
     */
    AppointmentPosition appointmentPosition = appointmentPositionService
      .findOne(personAppointment.getAppointmentPositionId());
    Cycle cycle = cycleService.findOne(appointmentPosition.getCycleId());
    if (personAppointment.getRecommended()) {
      cycle = adjustRecommendedCount(cycle, true);
    }
    cycle = adjustNominationCount(cycle, true);
    cycleService.save(cycle);

    /**
     * Lets unrecommend previous person first
     * TODO: Create Index on Person Appointment isRecommended and isAppointed
     */
    removeExistingRecommendation(personAppointment);
    return personAppointmentService.save(personAppointment);
  }

  private void removeExistingRecommendation(PersonAppointment personAppointment) {
    if (personAppointment.getIsRecommended()) {
      List<PersonAppointment> alreadyRecommended = personAppointmentService
        .findByAppointmentPositionIdAndIsRecommendedTrue(
          personAppointment.getAppointmentPositionId());

      if (!CollectionUtils.isEmpty(alreadyRecommended)) {
        for (PersonAppointment recommended : alreadyRecommended) {
          recommended.setIsRecommended(false);
          personAppointmentService.save(recommended);
        }
      }
    }
  }

  private Cycle adjustRecommendedCount(Cycle cycle, boolean increment) {
    Long recommendedCount = cycle.getRecommendedCount();
    if (increment) {
      cycle.setRecommendedCount(recommendedCount++);
    } else {
      cycle.setRecommendedCount(recommendedCount--);
    }
    return cycle;
  }

  private Cycle adjustNominationCount(Cycle cycle, boolean increment) {
    Long nominatedCount = cycle.getNominatedCount();
    if (increment) {
      cycle.setNominatedCount(nominatedCount++);
    } else {
      cycle.setNominatedCount(nominatedCount--);
    }
    return cycle;
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
    if (one == null || (one.getIsActive() != null && one.getIsActive() == false)) {
      log.info("Invalid Person {}", one);
      throw new RuntimeException("Jani!! Sahi person id dedo");
    }
    AppointmentPosition appointmentPosition = appointmentPositionService
      .findOne(personAppointment.getAppointmentPositionId());
    if (appointmentPosition == null || (appointmentPosition.getIsActive() != null
      && appointmentPosition.getIsActive() == false)
      || !appointmentPosition.getState().equals(AppointmentPositionState.CREATED)) {

      log.info("Invalid Appointment Position {}", appointmentPosition);
      throw new RuntimeException("Position Dekh k do");
    }
  }

  private void performOnlyUpdateValidations(PersonAppointment personAppointment) {
    Long id = personAppointment.getId();
    PersonAppointment dbPersonAppointment = personAppointmentService.findOne(id);
    if (dbPersonAppointment.getPriority() == 0) {
      throw new RuntimeException("Excuseme!! you can't update incumbtee");
    }
  }

  @Transactional
  public void recommendPersonInAppointment(PersonRecommendationDto personRecommendationDto) {
    Long personAppointmentId = personRecommendationDto.getPersonAppointmentId();
    PersonAppointment personAppointment = personAppointmentService.findOne(personAppointmentId);
    Assert.notNull(personAppointment,
      "Invalid personAppointment Id, personAppointment does not exists");

    Long appointmentPositionId = personAppointment.getAppointmentPositionId();
    List<PersonAppointment> alreadyRecommended = personAppointmentService
      .findByAppointmentPositionIdAndIsRecommendedTrue(
        personAppointment.getAppointmentPositionId());

    alreadyRecommended.stream().map(pa -> {
      pa.setIsRecommended(Boolean.FALSE);
      return pa;
    }).map(personAppointmentService::save);

    personAppointment.setIsRecommended(true);
    personAppointmentService.save(personAppointment);

    applicationEventPublisher.publishEvent(PersonRecommendedEventDto.builder()
      .personAppointmentId(personAppointmentId)
      .appointmentPositionId(appointmentPositionId)
      .personId(personAppointment.getPersonId())
      .previousRecommendationCount(alreadyRecommended.size())
      .build());
  }

  public List<PersonAppointment> findRecommendationAndNominationByPersonIdAndCycleId(long personId, long cycleId) {
    List<PersonAppointment> personAppointments = personAppointmentService
      .findByPersonId(personId, true, true);

    return personAppointments.stream().filter(pa -> {
      AppointmentPosition ap = appointmentPositionService.findOne(pa.getAppointmentPositionId());
      return ap.getCycleId() == cycleId;
    }).collect(Collectors.toList());
  }
}

package com.inner.satisfaction.backend.person.appointment;

import static com.inner.satisfaction.backend.error.ErrorEnumType.PERSON_APPOINTMENT_DOES_NOT_EXIST;

import com.google.common.collect.ImmutableMap;
import com.inner.satisfaction.backend.appointment.AppointmentPosition;
import com.inner.satisfaction.backend.appointment.AppointmentPositionService;
import com.inner.satisfaction.backend.appointment.AppointmentPositionState;
import com.inner.satisfaction.backend.authentication.token.AuthenticationToken;
import com.inner.satisfaction.backend.company.Company;
import com.inner.satisfaction.backend.company.CompanyService;
import com.inner.satisfaction.backend.cycle.Cycle;
import com.inner.satisfaction.backend.cycle.CycleService;
import com.inner.satisfaction.backend.error.AmsException;
import com.inner.satisfaction.backend.error.ErrorEnumType;
import com.inner.satisfaction.backend.institution.InstitutionService;
import com.inner.satisfaction.backend.person.Person;
import com.inner.satisfaction.backend.person.PersonService;
import com.inner.satisfaction.backend.person.appointment.dto.PersonAppointmentExtendedDto;
import com.inner.satisfaction.backend.person.appointment.dto.PersonRecommendationDto;
import com.inner.satisfaction.backend.person.appointment.dto.PersonRemarksDto;
import com.inner.satisfaction.backend.person.appointment.event.PersonRecommendedEventDto;
import com.inner.satisfaction.backend.position.PositionService;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import javax.transaction.Transactional;
import lombok.extern.slf4j.Slf4j;
import org.springframework.context.ApplicationEventPublisher;
import org.springframework.security.core.context.SecurityContextHolder;
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
  private final InstitutionService institutionService;
  private final PositionService positionService;
  private final PersonService personService;
  private final PersonAppointmentService personAppointmentService;
  private final AppointmentPositionService appointmentPositionService;
  private final CompanyService companyService;
  private final ApplicationEventPublisher applicationEventPublisher;

  public PersonAppointmentFacade(
    CycleService cycleService,
    InstitutionService institutionService,
    PositionService positionService,
    AppointmentPositionService appointmentPositionService,
    PersonAppointmentService personAppointmentService,
    PersonService personService,
    CompanyService companyService,
    ApplicationEventPublisher applicationEventPublisher) {
    this.cycleService = cycleService;
    this.institutionService = institutionService;
    this.positionService = positionService;
    this.appointmentPositionService = appointmentPositionService;
    this.personAppointmentService = personAppointmentService;
    this.personService = personService;
    this.companyService = companyService;
    this.applicationEventPublisher = applicationEventPublisher;
  }

  public PersonAppointment save(PersonAppointment personAppointment) {
    performValidations(personAppointment);

    if(personAppointment.getCompany() == null) {
      AuthenticationToken authenticationToken = (AuthenticationToken) SecurityContextHolder.getContext()
        .getAuthentication();
      int companyId = authenticationToken.getCompanyId();
      Company company = companyService.findOne((long) companyId);
      personAppointment.setCompany(company);
    }

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
  }

  private void performValidations(PersonAppointment personAppointment) {
    if (personAppointment.getPriority() == 0) {
      throw new AmsException(ErrorEnumType.AS_INCUMBENT_CAN_NOT_BE_UPDATED_OR_CREATED);
    }
    if (personAppointment.getAppointed() == true) {
      throw new AmsException(ErrorEnumType.CAN_NOT_NOMINATE_WITH_IS_APPOINTED_TRUE);
    }
    Person person = personService.findOne(personAppointment.getPersonId());
    if (person == null || (person.getIsActive() != null && person.getIsActive() == false)) {
      throw new AmsException(ErrorEnumType.PERSON_DOES_NOT_EXIST_IN_DB,
        ImmutableMap.of("person-from-db", personAppointment));
    }
    if (personAppointment.getReappointmentCount() != null && !personAppointment
      .getReappointmentCount().equals(0)) {
      throw new AmsException(ErrorEnumType.INVALID_REAPPOINTMENT_COUNT_PROVIDED,
        ImmutableMap.of("given-payload", person));
    }

    AppointmentPosition appointmentPosition = appointmentPositionService
      .findOne(personAppointment.getAppointmentPositionId());
    if (appointmentPosition == null || (appointmentPosition.getIsActive() != null
      && appointmentPosition.getIsActive() == false)
      || !appointmentPosition.getState().equals(AppointmentPositionState.CREATED)) {
      throw new AmsException(ErrorEnumType.APPOINTMENT_POSITION_DOES_NOT_EXIST,
        ImmutableMap.of("appointment-position-from-db", appointmentPosition));
    }
  }

  private void performOnlyUpdateValidations(PersonAppointment personAppointment) {
    Long id = personAppointment.getId();
    PersonAppointment dbPersonAppointment = personAppointmentService.findOne(id);
    if (dbPersonAppointment.getPriority() == 0) {
      throw new AmsException(ErrorEnumType.INCUMBENT_INFORMATION_CAN_NOT_BE_UPDATED,
        ImmutableMap.of("id-trying-to-update", id));
    }
  }

  @Transactional
  public void recommendPersonInAppointment(PersonRecommendationDto personRecommendationDto) {
    Long personAppointmentId = personRecommendationDto.getPersonAppointmentId();
    PersonAppointment personAppointment = personAppointmentService.findOne(personAppointmentId);
    Assert.notNull(personAppointment,
      "Invalid personAppointment Id, personAppointment does not exists");
    AppointmentPosition appointmentPosition = appointmentPositionService
      .findOne(personAppointment.getAppointmentPositionId());

    List<PersonAppointmentExtendedDto> nominationsAndRecommendationOfPerson = findRecommendationAndNominationByPersonIdAndCycleId(
      personAppointment.getPersonId(), appointmentPosition.getCycleId());

    List<PersonAppointmentExtendedDto> recommendationsOfPerson = nominationsAndRecommendationOfPerson
      .stream()
      .filter(paeDto -> paeDto.isRecommended())
      .collect(Collectors.toList());

    if (recommendationsOfPerson.size() > 0) {
      ErrorEnumType personAlreadyRecommended = ErrorEnumType.PERSON_ALREADY_RECOMMENDED;
      throw new AmsException(personAlreadyRecommended.getErrorCode(),
        personAlreadyRecommended.getMessage() + formatify(recommendationsOfPerson),
        ImmutableMap.of("payload", personRecommendationDto));
    }

    Long appointmentPositionId = personAppointment.getAppointmentPositionId();
    List<PersonAppointment> alreadyRecommended = personAppointmentService
      .findByAppointmentPositionIdAndIsRecommendedTrue(
        personAppointment.getAppointmentPositionId());

    alreadyRecommended.stream().forEach(pa -> {
      pa.setIsRecommended(Boolean.FALSE);
      personAppointmentService.save(pa);
    });

    personAppointment.setIsRecommended(true);
    personAppointmentService.save(personAppointment);

    applicationEventPublisher.publishEvent(PersonRecommendedEventDto.builder()
      .personAppointmentId(personAppointmentId)
      .appointmentPositionId(appointmentPositionId)
      .personId(personAppointment.getPersonId())
      .previousRecommendationCount(alreadyRecommended.size())
      .build());
  }

  private String formatify(List<PersonAppointmentExtendedDto> recommendationsOfPerson) {
    return recommendationsOfPerson.stream()
      .map(paeDto -> paeDto.getPosition() + " " + paeDto.getInstitution().getName())
      .collect(Collectors.joining(", "));
  }

  public List<PersonAppointmentExtendedDto> findRecommendationAndNominationByPersonIdAndCycleId(
    long personId, long cycleId) {
    List<PersonAppointment> personAppointments = personAppointmentService
      .findByPersonId(personId, true, true);

    return personAppointments.stream()
      .map(pa -> PersonAppointmentExtendedDto.builder()
        .personAppointmentId(pa.getId())
        .personId(pa.getPersonId())
        .appointmentPositionId(pa.getAppointmentPositionId())
        .priority(pa.getPriority())
        .isRecommended(pa.getIsRecommended())
        .isAppointed(pa.getIsAppointed())
        .build())
      .map(paeDto -> {
        AppointmentPosition ap = appointmentPositionService
          .findOne(paeDto.getAppointmentPositionId());
        return paeDto.toBuilder()
          .cycleId(ap.getCycleId())
          .positionId(ap.getPositionId())
          .institutionId(ap.getInstitutionId())
          .seatId(ap.getSeatNo())
          .build();
      })
      .filter(paeDto ->
        cycleId == paeDto.getCycleId()
      ).map(
        paeDto -> {
          paeDto.setPerson(personService.findOne(paeDto.getPersonId()));
          return paeDto;
        }
      ).map(
        paeDto -> {
          paeDto.setInstitution(institutionService.findOne(paeDto.getInstitutionId()));
          return paeDto;
        }
      ).map(
        paeDto -> {
          paeDto.setPosition(positionService.findOne(paeDto.getPositionId()));
          return paeDto;
        }
      )
      .collect(Collectors.toList());
  }

  public void updateRemarks(PersonRemarksDto personRemarksDto) {
    PersonAppointment personAppointment = Optional.ofNullable(personAppointmentService
      .findOne(personRemarksDto.getPersonAppointmentId()))
      .orElseThrow(() -> new AmsException(PERSON_APPOINTMENT_DOES_NOT_EXIST,
        ImmutableMap.of("requestPayload", personRemarksDto)));

    personAppointment.setRemarks(personRemarksDto.getRemarks());
    personAppointmentService.save(personAppointment);
  }
}

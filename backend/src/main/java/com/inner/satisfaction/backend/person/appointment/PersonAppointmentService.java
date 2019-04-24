package com.inner.satisfaction.backend.person.appointment;

import com.google.common.collect.Lists;
import com.inner.satisfaction.backend.authentication.token.AuthenticationToken;
import com.inner.satisfaction.backend.base.SimpleBaseService;
import java.util.List;
import java.util.Optional;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Service;
import org.springframework.util.Assert;

@Service
public class PersonAppointmentService extends SimpleBaseService<PersonAppointment> {

  private final PersonAppointmentRepository personAppointmentRepository;

  protected PersonAppointmentService(
    PersonAppointmentRepository baseRepository) {
    super(baseRepository);
    this.personAppointmentRepository = baseRepository;
  }

  @Override
  public PersonAppointment save(PersonAppointment personAppointment) {
    return super.save(personAppointment);
  }

  public PersonAppointment update(PersonAppointment personAppointment) {
    return save(personAppointment);
  }

  public PersonAppointment saveNew(PersonAppointment personAppointment) {
    return save(personAppointment);
  }

  public List<PersonAppointment> findByAppointmentPositionId(long cpiId) {
    return personAppointmentRepository.findByAppointmentPositionIdAndCompanyId(cpiId,
      getCurrentUserCompanyId().get());
  }

  public PersonAppointment findByAppointmentPositionIdAndIsAppointedTrue(
    long appointmentPositionId) {
    return personAppointmentRepository
      .findByAppointmentPositionIdAndIsAppointedTrue(appointmentPositionId);
  }

  public List<PersonAppointment> findByAppointmentPositionIdAndIsRecommendedTrue(
    long appointmentPositionId) {
    return personAppointmentRepository
      .findByAppointmentPositionIdAndIsRecommendedTrue(appointmentPositionId);
  }

  public List<PersonAppointment> findAppointmentsOfPerson(long personId) {
    return personAppointmentRepository.findByPersonIdAndIsAppointedTrue(personId);
  }

  public void appointRecommendedPeople(List<Long> appointmentPositionId) {
    personAppointmentRepository.appointRecommendedPeople(appointmentPositionId);
  }

  public void appointRecommendedPeople(Long appointmentPositionId) {
    personAppointmentRepository.appointRecommendedPeople(appointmentPositionId);
  }

  public int findRecommendedCountInAppointmentPositionIds(List<Long> appointmentPositionIds) {
    return personAppointmentRepository
      .findRecommendedCountInAppointmentPositionIds(appointmentPositionIds);
  }

  public List<PersonAppointment> findByPersonId(long personId, boolean isRecommended,
    boolean isNominated) {
    Assert
      .isTrue(isRecommended || isNominated, "Either isRecommended or isNominated should be true");
    Optional<Integer> currentUserCompanyId = getCurrentUserCompanyId();
    if (isRecommended && isNominated) {
      return personAppointmentRepository.findByPersonId(personId);
    } else if (isRecommended
      && !isNominated) { // !isNominated is redundent check, but for clear code, need to do it
      return personAppointmentRepository.findByPersonIdAndIsRecommendedEqualsAndCompanyId(personId, true, currentUserCompanyId.get());
    } else {
      return personAppointmentRepository.findByPersonIdAndIsRecommendedEqualsAndCompanyId(personId, false, currentUserCompanyId.get());
    }
  }

  public void deleteAllPersonAppointmentForAppointmentPosition(long appointmentPositionId) {
    personAppointmentRepository.deleteByAppointmentPositionId(appointmentPositionId, getCurrentUserCompanyId().get());
  }

  @Override
  public List<PersonAppointment> findAll() {
    return getCurrentUserCompanyId()
      .map(Integer::longValue)
      .map(personAppointmentRepository::findByCompanyId)
      .orElse(Lists.newArrayList());
  }

  @Override
  public Page<PersonAppointment> findAll(Pageable pageable) {
    return getCurrentUserCompanyId()
      .map(Integer::longValue)
      .map(companyId -> personAppointmentRepository.findByCompanyId(companyId, pageable))
      .orElse(Page.empty());
  }

  @Override
  public PersonAppointment findOne(Long id) {
    PersonAppointment one = super.findOne(id);
    Optional<Integer> currentUserCompanyId = getCurrentUserCompanyId();
    if(one != null && currentUserCompanyId.isPresent()) {
      return one.getCompany().getId() == currentUserCompanyId.map(Integer::longValue).get() ? one : null;
    }
    return one;
  }

  private Optional<Integer> getCurrentUserCompanyId() {
    Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
    if (authentication instanceof AuthenticationToken) {
      AuthenticationToken authenticationToken = (AuthenticationToken) authentication;
      return Optional.of(authenticationToken.getCompanyId());
    }
    return Optional.empty();
  }
}

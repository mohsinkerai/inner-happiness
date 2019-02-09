package com.inner.satisfaction.backend.person.appointment;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import java.util.List;
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
    return personAppointmentRepository.findByAppointmentPositionId(cpiId);
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

    if (isRecommended && isNominated) {
      return personAppointmentRepository.findByPersonId(personId);
    } else if (isRecommended
      && !isNominated) { // !isNominated is redundent check, but for clear code, need to do it
      return personAppointmentRepository.findByPersonIdAndIsRecommendedEquals(personId, true);
    } else {
      return personAppointmentRepository.findByPersonIdAndIsRecommendedEquals(personId, false);
    }
  }
}

package com.inner.satisfaction.backend.person.appointment;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import java.util.List;
import org.springframework.stereotype.Service;

@Service
public class PersonAppointmentService extends SimpleBaseService<PersonAppointment> {

  private final PersonAppointmentRepository personAppointmentRepository;

  protected PersonAppointmentService(
    PersonAppointmentRepository baseRepository) {
    super(baseRepository);
    this.personAppointmentRepository = baseRepository;
  }

  public List<PersonAppointment> findByAppointmentPositionId(long cpiId) {
    return personAppointmentRepository.findByAppointmentPositionId(cpiId);
  }

  public PersonAppointment findByAppointmentPositionIdAndIsAppointedTrue(
    long appointmentPositionId) {
    return personAppointmentRepository
      .findByAppointmentPositionIdAndIsAppointedTrue(appointmentPositionId);
  }

  public List<PersonAppointment> findAppointmentsOfPerson(long personId) {
    return personAppointmentRepository.findByPersonIdAndIsAppointedTrue(personId);
  }

  public void appointRecommendedPeople(List<Long> appointmentPositionId) {
//    personAppointmentRepository.appointRecommendedPeople(appointmentPositionId);
  }
}

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

  public List<PersonAppointment> findByCpiId(long cpiId) {
    return personAppointmentRepository.findByCpiId(cpiId);
  }

  public PersonAppointment findByCpiIdAndIsAppointedTrue(long cpiId) {
    return personAppointmentRepository.findByCpiIdAndIsAppointedTrue(cpiId);
  }

  public List<PersonAppointment> findAppointmentsOfPerson(long personId) {
    return personAppointmentRepository.findByPersonIdAndIsAppointedTrue(personId);
  }
}

package com.inner.satisfaction.backend.person.appointment;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.stereotype.Repository;

@Repository
public interface PersonAppointmentRepository extends BaseRepository<PersonAppointment> {

  List<PersonAppointment> findByAppointmentPositionId(long appointmentPositionId);

  PersonAppointment findByAppointmentPositionIdAndIsAppointedTrue(long appointmentPositionId);

  List<PersonAppointment> findByPersonIdAndIsAppointedTrue(long personId);
}

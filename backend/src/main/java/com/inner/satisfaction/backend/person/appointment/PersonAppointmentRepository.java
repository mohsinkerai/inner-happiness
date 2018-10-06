package com.inner.satisfaction.backend.person.appointment;

import com.inner.satisfaction.backend.appointment.AppointmentPosition;
import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
public interface PersonAppointmentRepository extends BaseRepository<PersonAppointment> {

  List<PersonAppointment> findByAppointmentPositionId(long appointmentPositionId);

  PersonAppointment findByAppointmentPositionIdAndIsAppointedTrue(long appointmentPositionId);

  List<PersonAppointment> findByPersonIdAndIsAppointedTrue(long personId);

//  @Modifying
//  @Query("UPDATE PersonAppointment SET isAppointed=true WHERE isRecommended=true AND appointmentPositionId IN :appointmentPositionId")
//  void appointRecommendedPeople(List<Long> appointmentPositionId);
}

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

  List<PersonAppointment> findByAppointmentPositionIdAndIsRecommendedTrue(
    long appointmentPositionId);

  List<PersonAppointment> findByPersonIdAndIsAppointedTrue(long personId);

  @Modifying
  @Query("UPDATE person_appointment p SET p.isAppointed = true WHERE p.isRecommended=true AND p.appointmentPositionId IN :appointmentPositionId")
  void appointRecommendedPeople(List<Long> appointmentPositionId);

  @Modifying
  @Query("UPDATE person_appointment p SET p.isAppointed = true WHERE p.isRecommended=true AND p.appointmentPositionId = :appointmentPositionId")
  void appointRecommendedPeople(Long appointmentPositionId);

  @Query("SELECT count(pa) FROM PersonAppointment pa WHERE pa.appointmentPositionId in :appointmentPositionIds and pa.isRecommended = true")
  int findRecommendedCountInAppointmentPositionIds(List<Long> appointmentPositionIds);
}

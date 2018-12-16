package com.inner.satisfaction.backend.person.appointment;

import com.inner.satisfaction.backend.appointment.AppointmentPosition;
import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

@Repository
public interface PersonAppointmentRepository extends BaseRepository<PersonAppointment> {

  List<PersonAppointment> findByAppointmentPositionId(long appointmentPositionId);

  PersonAppointment findByAppointmentPositionIdAndIsAppointedTrue(long appointmentPositionId);

  List<PersonAppointment> findByAppointmentPositionIdAndIsRecommendedTrue(
    long appointmentPositionId);

  List<PersonAppointment> findByPersonIdAndIsAppointedTrue(long personId);

  @Modifying
  @Query("UPDATE person_appointment p SET p.is_appointed = true WHERE p.is_recommended=true AND p.appointment_position_id IN :appointmentPositionId")
  void appointRecommendedPeople(@Param("appointmentPositionId") List<Long> appointmentPositionId);

  @Modifying
  @Query("UPDATE person_appointment p SET p.is_appointed = true WHERE p.is_recommended=true AND p.appointment_position_id = :appointmentPositionId")
  void appointRecommendedPeople(@Param("appointmentPositionId") Long appointmentPositionId);

  @Query(nativeQuery = true, value = "SELECT count(*) FROM person_appointment pa WHERE pa.appointment_position_id in :appointmentPositionIds AND pa.is_recommended = true")
  int findRecommendedCountInAppointmentPositionIds(@Param("appointmentPositionIds") List<Long> appointmentPositionIds);
}

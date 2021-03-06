package com.inner.satisfaction.backend.person.appointment;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

@Repository
public interface PersonAppointmentRepository extends BaseRepository<PersonAppointment> {

  List<PersonAppointment> findByAppointmentPositionIdAndCompanyId(long appointmentPositionId, long companyId);

  PersonAppointment findByAppointmentPositionIdAndIsAppointedTrue(long appointmentPositionId);

  List<PersonAppointment> findByAppointmentPositionIdAndIsRecommendedTrue(
    long appointmentPositionId);

  List<PersonAppointment> findByPersonIdAndIsAppointedTrue(long personId);

  @Modifying
  @Query("UPDATE person_appointment p SET p.isAppointed = true WHERE p.isRecommended=true AND p.appointmentPositionId IN :appointmentPositionId")
  void appointRecommendedPeople(@Param("appointmentPositionId") List<Long> appointmentPositionId);

  @Modifying
  @Query("UPDATE person_appointment p SET p.isAppointed = true WHERE p.isRecommended =true AND p.appointmentPositionId = :appointmentPositionId")
  void appointRecommendedPeople(@Param("appointmentPositionId") Long appointmentPositionId);

  @Query(nativeQuery = true, value = "SELECT count(*) FROM person_appointment pa WHERE pa.appointment_position_id in :appointmentPositionIds AND pa.is_recommended = true")
  int findRecommendedCountInAppointmentPositionIds(
    @Param("appointmentPositionIds") List<Long> appointmentPositionIds);

  List<PersonAppointment> findByPersonId(long personId);

  List<PersonAppointment> findByPersonIdAndIsRecommendedEqualsAndCompanyId(long personId,
    boolean isRecommended, long companyId);

  @Modifying
  @Query("DELETE from person_appointment where appointmentPositionId = :appointmentPositionId and companyId = :companyId")
  void deleteByAppointmentPositionId(long appointmentPositionId, long companyId);

  Page<PersonAppointment> findByCompanyId(Long companyId, Pageable pageable);

  List<PersonAppointment> findByCompanyId(Long companyId);
}

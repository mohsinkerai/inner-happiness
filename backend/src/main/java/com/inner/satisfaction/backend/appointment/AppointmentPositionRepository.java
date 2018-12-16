package com.inner.satisfaction.backend.appointment;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
public interface AppointmentPositionRepository extends BaseRepository<AppointmentPosition> {

  String searchQuery = "SELECT ap.* FROM person_appointment as pa "
    + " INNER JOIN appointment_position as ap "
    + "  ON ap.id = pa.appointment_position_id"
    + " WHERE pa.person_id = :personId"
    + " AND ap.cycle_id = :cycleId"
    + " AND pa.is_recommended = true";

  List<AppointmentPosition> findByCycleIdAndInstitutionId(long cycleId, long institutionId);

  List<AppointmentPosition> findByCycleId(long cycleId);

  AppointmentPosition findByInstitutionIdAndSeatNoAndCycleIdAndPositionId(long institutionId, long seatNo, long positionId, long cycleId);

  List<AppointmentPosition> findByCycleIdAndInstitutionIdAndPositionIdAndSeatNo(long cycleId, long institutionId, long positionId, long seatNo);

  @Query(nativeQuery =  true, value = AppointmentPositionRepository.searchQuery)
  List<AppointmentPosition> findByCycleIdAndPersonIdAndRecommendedTrue(long cycleId, long personId);

  List<AppointmentPosition> findByCycleIdAndState(Long id, String state);
}

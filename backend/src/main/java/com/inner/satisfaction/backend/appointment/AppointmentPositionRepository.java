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

  String unrecommendedPositionsQuery = "SELECT ap.* "
    + " FROM appointment_position as ap "
    + " LEFT OUTER JOIN (select * from person_appointment where is_recommended=true) pa "
    + "   ON pa.appointment_position_id = ap.id "
    + " WHERE ap.cycle_id = :cycleId "
    + " AND pa.appointment_position_id is null";

  List<AppointmentPosition> findByCycleIdAndInstitutionId(long cycleId, long institutionId);

  List<AppointmentPosition> findByCycleId(long cycleId);

  // It can exist multiple, as of midterm case
  List<AppointmentPosition> findByInstitutionIdAndSeatNoAndCycleIdAndPositionId(long institutionId, long seatNo, long cycleId, long positionId);

  List<AppointmentPosition> findByCycleIdAndInstitutionIdAndPositionIdAndSeatNo(long cycleId, long institutionId, long positionId, long seatNo);

  @Query(nativeQuery =  true, value = AppointmentPositionRepository.searchQuery)
  List<AppointmentPosition> findByCycleIdAndPersonIdAndRecommendedTrue(long cycleId, long personId);

  List<AppointmentPosition> findByCycleIdAndState(Long id, String state);

  @Query(nativeQuery = true, value = unrecommendedPositionsQuery)
  List<AppointmentPosition> findByCycleIdWhereNoOneIsRecommended(long cycleId);
}

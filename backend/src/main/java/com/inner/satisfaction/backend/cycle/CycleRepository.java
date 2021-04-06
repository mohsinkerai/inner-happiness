package com.inner.satisfaction.backend.cycle;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
public interface CycleRepository extends BaseRepository<Cycle> {

  String findPositionsWithoutRecommendation =
    "SELECT p.name as 'position', i.name as 'institution'"
    + " FROM "
    + "  person_appointment pa "
    + "    INNER JOIN appointment_position ap ON ap.id = pa.appointment_position_id "
    + "    INNER JOIN institution i ON i.id = ap.institution_id "
    + "    INNER JOIN position p ON p.id = ap.position_id "
    + " WHERE "
    + "  ap.cycle_id = ?1 AND (ap.`state` = null or ap.`state` = 'CREATED') "
    + " GROUP BY "
    + "  ap.id, p.name, i.name "
    + " HAVING "
    + "  count( if(pa.is_recommended = TRUE, TRUE, NULL)) != 1 ";

  @Query(nativeQuery = true, value = findPositionsWithoutRecommendation)
  List<CycleClosureResponseDto> findPositionsWithoutOrWithExceededRecommendation(long cycleId);
}

package com.is.migration.updatefields.jk.persistance.poi;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface PoiRepository extends JpaRepository<Poi, Long> {

  List<Poi> findByCycleIdEqualsAndPositionIdEqualsAndInstitutionIdEquals(Long cycleId, Long positionId, Long institutionId);
}

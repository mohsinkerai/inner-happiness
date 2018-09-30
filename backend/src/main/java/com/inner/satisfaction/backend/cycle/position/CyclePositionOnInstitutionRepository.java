package com.inner.satisfaction.backend.cycle.position;

import com.inner.satisfaction.backend.base.BaseRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface CyclePositionOnInstitutionRepository extends
  BaseRepository<CyclePositionOnInstitution> {

  CyclePositionOnInstitution findByCycleIdAndPositionOnInstitutionId(long cycleId,
    long positionOnInstitutionId);
}

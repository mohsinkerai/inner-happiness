package com.inner.satisfaction.backend.cycle.position;

import com.inner.satisfaction.backend.base.BaseService;
import org.springframework.stereotype.Service;

@Service
public class CyclePositionOnInstitutionService extends BaseService<CyclePositionOnInstitution> {

  private final CyclePositionOnInstitutionRepository cyclePositionOnInstitutionRepository;

  protected CyclePositionOnInstitutionService(
    CyclePositionOnInstitutionRepository baseRepository,
    CyclePositionOnInstitutionValidation cyclePositionOnInstitutionValidation) {
    super(baseRepository, cyclePositionOnInstitutionValidation);
    cyclePositionOnInstitutionRepository = baseRepository;
  }

  public CyclePositionOnInstitution findByCycleIdAndPositionOnInstitutionId(
    long positionOnInstitutionId,
    long cycleId) {
    return cyclePositionOnInstitutionRepository
      .findByCycleIdAndPositionOnInstitutionId(cycleId, positionOnInstitutionId);
  }
}

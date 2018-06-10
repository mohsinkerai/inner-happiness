package com.inner.satisfaction.backend.cycle.position;

import com.inner.satisfaction.backend.base.BaseService;
import org.springframework.stereotype.Service;

@Service
public class CyclePositionOnInstitutionService extends BaseService<CyclePositionOnInstitution>{

  protected CyclePositionOnInstitutionService(
      CyclePositionOnInstitutionRepository baseRepository,
      CyclePositionOnInstitutionValidation cyclePositionOnInstitutionValidation) {
    super(baseRepository, cyclePositionOnInstitutionValidation);
  }
}

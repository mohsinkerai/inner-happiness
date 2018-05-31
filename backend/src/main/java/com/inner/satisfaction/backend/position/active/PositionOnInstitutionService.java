package com.inner.satisfaction.backend.position.active;

import com.inner.satisfaction.backend.base.BaseService;
import org.springframework.stereotype.Service;

@Service
public class PositionOnInstitutionService extends BaseService<PositionOnInstitution>{

  protected PositionOnInstitutionService(
      PositionOnInstitutionRepository baseRepository,
      PositionOnInstitutionValidation positionOnInstitutionValidation) {
    super(baseRepository, positionOnInstitutionValidation);
  }
}

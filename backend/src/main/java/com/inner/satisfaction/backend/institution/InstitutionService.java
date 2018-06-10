package com.inner.satisfaction.backend.institution;

import com.inner.satisfaction.backend.base.BaseService;
import org.springframework.stereotype.Service;

@Service
public class InstitutionService extends BaseService<Institution>{

  protected InstitutionService(
      InstitutionRepository baseRepository,
      InstitutionValidation institutionValidation) {
    super(baseRepository, institutionValidation);
  }
}

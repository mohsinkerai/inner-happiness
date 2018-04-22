package com.inner.satisfaction.backend.lookups.institution;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class InstitutionService extends SimpleBaseService<Institution> {

  protected InstitutionService(
      InstitutionRepository baseRepository) {
    super(baseRepository);
  }
}

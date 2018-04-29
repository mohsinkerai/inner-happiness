package com.inner.satisfaction.backend.lookups.publicserviceinstitution;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class PublicServiceInstitutionService extends SimpleBaseService<PublicServiceInstitution> {

  protected PublicServiceInstitutionService(
      PublicServiceInstitutionRepository baseRepository) {
    super(baseRepository);
  }
}

package com.inner.satisfaction.backend.lookups.voluntaryinstitution;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class VoluntaryInstitutionService extends SimpleBaseService<VoluntaryInstitution> {

  protected VoluntaryInstitutionService(
      VoluntaryInstitutionRepository baseRepository) {
    super(baseRepository);
  }
}

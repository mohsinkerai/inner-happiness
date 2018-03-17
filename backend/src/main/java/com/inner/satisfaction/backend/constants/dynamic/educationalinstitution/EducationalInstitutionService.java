package com.inner.satisfaction.backend.constants.dynamic.educationalinstitution;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class EducationalInstitutionService extends SimpleBaseService<EducationalInstitution> {

  protected EducationalInstitutionService(
      EducationalInstitutionRepository baseRepository) {
    super(baseRepository);
  }
}

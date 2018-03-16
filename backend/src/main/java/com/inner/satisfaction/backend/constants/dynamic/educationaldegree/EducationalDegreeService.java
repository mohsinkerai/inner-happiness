package com.inner.satisfaction.backend.constants.dynamic.educationaldegree;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import com.inner.satisfaction.backend.company.Company;
import org.springframework.stereotype.Service;

@Service
public class EducationalDegreeService extends SimpleBaseService<EducationalDegree> {

  protected EducationalDegreeService(
      EducationalDegreeRepository baseRepository) {
    super(baseRepository);
  }
}

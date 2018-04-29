package com.inner.satisfaction.backend.lookups.educationaldegree;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class EducationalDegreeService extends SimpleBaseService<EducationalDegree> {

  protected EducationalDegreeService(
      EducationalDegreeRepository baseRepository) {
    super(baseRepository);
  }
}

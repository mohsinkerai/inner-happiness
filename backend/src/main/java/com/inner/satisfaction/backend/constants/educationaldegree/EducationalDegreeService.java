package com.inner.satisfaction.backend.constants.educationaldegree;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import com.inner.satisfaction.backend.company.Company;
import com.inner.satisfaction.backend.company.CompanyValidation;
import org.springframework.stereotype.Service;

@Service
public class EducationalDegreeService extends SimpleBaseService<Company> {

  protected EducationalDegreeService(
      EducationalDegreeRepository baseRepository) {
    super(baseRepository);
  }
}

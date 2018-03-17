package com.inner.satisfaction.backend.constants.dynamic.areaofstudy;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import com.inner.satisfaction.backend.company.Company;
import org.springframework.stereotype.Service;

@Service
public class AreaOfStudyService extends SimpleBaseService<AreaOfStudy> {

  protected AreaOfStudyService(
      AreaOfStudyRepository baseRepository) {
    super(baseRepository);
  }
}

package com.inner.satisfaction.backend.lookups.areaofstudy;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class AreaOfStudyService extends SimpleBaseService<AreaOfStudy> {

  protected AreaOfStudyService(
      AreaOfStudyRepository baseRepository) {
    super(baseRepository);
  }
}

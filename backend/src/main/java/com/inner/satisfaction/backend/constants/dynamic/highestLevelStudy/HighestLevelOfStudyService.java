package com.inner.satisfaction.backend.constants.dynamic.highestLevelStudy;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class HighestLevelOfStudyService extends SimpleBaseService<HighestLevelOfStudy> {

  protected HighestLevelOfStudyService(
      HighestLevelOfStudyRepository baseRepository) {
    super(baseRepository);
  }
}

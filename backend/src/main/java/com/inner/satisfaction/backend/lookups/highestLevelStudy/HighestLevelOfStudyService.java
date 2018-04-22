package com.inner.satisfaction.backend.lookups.highestLevelStudy;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class HighestLevelOfStudyService extends SimpleBaseService<HighestLevelOfStudy> {

  protected HighestLevelOfStudyService(
      HighestLevelOfStudyRepository baseRepository) {
    super(baseRepository);
  }
}

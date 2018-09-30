package com.inner.satisfaction.backend.lookups.secularstudylevel;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class SecularStudyLevelService extends SimpleBaseService<SecularStudyLevel> {

  protected SecularStudyLevelService(
      SecularStudyLevelRepository baseRepository) {
    super(baseRepository);
  }
}

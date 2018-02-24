package com.inner.satisfaction.backend.level.active;

import com.inner.satisfaction.backend.base.BaseService;
import org.springframework.stereotype.Service;

@Service
public class LevelActiveService extends BaseService<LevelActive>{

  protected LevelActiveService(
      LevelActiveRepository baseRepository,
      LevelActiveValidation levelActiveValidation) {
    super(baseRepository, levelActiveValidation);
  }
}

package com.inner.satisfaction.backend.level.type;

import com.inner.satisfaction.backend.base.BaseService;
import org.springframework.stereotype.Service;

@Service
public class LevelTypeService extends BaseService<LevelType> {

  protected LevelTypeService(
      LevelTypeRepository levelTypeRepository,
      LevelTypeValidation levelTypeValidation) {
    super(levelTypeRepository, levelTypeValidation);
  }
}

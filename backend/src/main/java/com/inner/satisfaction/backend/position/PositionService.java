package com.inner.satisfaction.backend.position;

import com.inner.satisfaction.backend.base.BaseService;
import org.springframework.stereotype.Service;

@Service
public class PositionService extends BaseService<Position>{

  protected PositionService(
      PositionRepository baseRepository,
      PositionValidation positionValidation) {
    super(baseRepository, positionValidation);
  }
}

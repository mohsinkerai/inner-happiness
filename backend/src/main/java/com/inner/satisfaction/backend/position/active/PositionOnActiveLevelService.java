package com.inner.satisfaction.backend.position.active;

import com.inner.satisfaction.backend.base.BaseService;
import org.springframework.stereotype.Service;

@Service
public class PositionOnActiveLevelService extends BaseService<PositionOnActiveLevel>{

  protected PositionOnActiveLevelService(
      PositionOnActiveLevelRepository baseRepository,
      PositionOnActiveLevelValidation positionOnActiveLevelValidation) {
    super(baseRepository, positionOnActiveLevelValidation);
  }
}

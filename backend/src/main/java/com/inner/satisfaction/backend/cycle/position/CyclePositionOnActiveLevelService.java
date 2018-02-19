package com.inner.satisfaction.backend.cycle.position;

import com.inner.satisfaction.backend.base.BaseService;
import org.springframework.stereotype.Service;

@Service
public class CyclePositionOnActiveLevelService extends BaseService<CyclePositionOnActiveLevel>{

  protected CyclePositionOnActiveLevelService(
      CyclePositionOnActiveLevelRepository baseRepository,
      CyclePositionOnActiveLevelValidation cyclePositionOnActiveLevelValidation) {
    super(baseRepository, cyclePositionOnActiveLevelValidation);
  }
}

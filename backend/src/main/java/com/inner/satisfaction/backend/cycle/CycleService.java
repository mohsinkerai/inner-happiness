package com.inner.satisfaction.backend.cycle;

import com.inner.satisfaction.backend.base.BaseService;
import org.springframework.stereotype.Service;

@Service
public class CycleService extends BaseService<Cycle>{

  protected CycleService(
      CycleRepository baseRepository,
      CycleValidation cycleValidation) {
    super(baseRepository, cycleValidation);
  }
}

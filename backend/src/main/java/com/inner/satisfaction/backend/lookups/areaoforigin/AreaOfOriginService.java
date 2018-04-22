package com.inner.satisfaction.backend.lookups.areaoforigin;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class AreaOfOriginService extends SimpleBaseService<AreaOfOrigin> {

  protected AreaOfOriginService(
      AreaOfOriginRepository baseRepository) {
    super(baseRepository);
  }
}

package com.inner.satisfaction.backend.lookups.businessnature;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class BusinessNatureService extends SimpleBaseService<BusinessNature> {

  protected BusinessNatureService(
      BusinessNatureRepository baseRepository) {
    super(baseRepository);
  }
}

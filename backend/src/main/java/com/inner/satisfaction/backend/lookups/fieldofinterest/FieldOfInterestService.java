package com.inner.satisfaction.backend.lookups.fieldofinterest;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class FieldOfInterestService extends SimpleBaseService<FieldOfInterest> {

  protected FieldOfInterestService(
      FieldOfInterestRepository baseRepository) {
    super(baseRepository);
  }
}

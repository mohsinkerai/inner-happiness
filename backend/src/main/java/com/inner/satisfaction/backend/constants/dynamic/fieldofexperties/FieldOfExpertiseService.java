package com.inner.satisfaction.backend.constants.dynamic.fieldofexperties;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class FieldOfExpertiseService extends SimpleBaseService<FieldOfExpertise> {

  protected FieldOfExpertiseService(
      FieldOfExpertiseRepository baseRepository) {
    super(baseRepository);
  }
}

package com.inner.satisfaction.backend.lookups.occupation;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class OccupationService extends SimpleBaseService<Occupation> {

  protected OccupationService(
      OccupationRepository baseRepository) {
    super(baseRepository);
  }
}

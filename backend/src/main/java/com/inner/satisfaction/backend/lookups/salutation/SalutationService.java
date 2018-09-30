package com.inner.satisfaction.backend.lookups.salutation;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class SalutationService extends SimpleBaseService<Salutation> {

  protected SalutationService(
      SalutationRepository baseRepository) {
    super(baseRepository);
  }
}

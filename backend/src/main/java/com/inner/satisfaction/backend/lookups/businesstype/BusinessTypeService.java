package com.inner.satisfaction.backend.lookups.businesstype;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class BusinessTypeService extends SimpleBaseService<BusinessType> {

  protected BusinessTypeService(
      BusinessTypeRepository baseRepository) {
    super(baseRepository);
  }
}

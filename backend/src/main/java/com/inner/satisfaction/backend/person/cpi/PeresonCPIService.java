package com.inner.satisfaction.backend.person.cpi;

import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class PeresonCPIService extends SimpleBaseService<PersonCPI> {

  protected PeresonCPIService(
      PersonCPIRepository baseRepository) {
    super(baseRepository);
  }
}

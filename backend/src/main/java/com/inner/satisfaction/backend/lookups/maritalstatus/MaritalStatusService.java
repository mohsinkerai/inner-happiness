package com.inner.satisfaction.backend.lookups.maritalstatus;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class MaritalStatusService extends SimpleBaseService<MaritalStatus> {

  protected MaritalStatusService(
      MaritalStatusRepository baseRepository) {
    super(baseRepository);
  }
}

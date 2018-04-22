package com.inner.satisfaction.backend.lookups.religiousqualification;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class ReligiousQualificationService extends SimpleBaseService<ReligiousQualification> {

  protected ReligiousQualificationService(
      ReligiousQualificationRepository baseRepository) {
    super(baseRepository);
  }
}

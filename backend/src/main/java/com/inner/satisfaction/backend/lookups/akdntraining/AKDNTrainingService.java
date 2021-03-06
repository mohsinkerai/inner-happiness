package com.inner.satisfaction.backend.lookups.akdntraining;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class AKDNTrainingService extends SimpleBaseService<AKDNTraining> {

  protected AKDNTrainingService(
      AKDNTrainingRepository baseRepository) {
    super(baseRepository);
  }
}

package com.inner.satisfaction.backend.lookups.akdntraining;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;
import static com.inner.satisfaction.backend.lookups.akdntraining.AKDNTrainingController.PATH;

@RestController
@RequestMapping(CONSTANTS_PREFIX + PATH)
public class AKDNTrainingController extends BaseController<AKDNTraining> {

  public static final String PATH = "akdn-training";

  public AKDNTrainingController(AKDNTrainingService akdnTrainingService) {
    super(akdnTrainingService);
  }
}

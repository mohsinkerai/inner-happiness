package com.inner.satisfaction.backend.constants.dynamic.akdntraining;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;
import static com.inner.satisfaction.backend.constants.dynamic.akdntraining.AKDNTrainingController.PATH;

@RestController
@RequestMapping(PREFIX + PATH)
public class AKDNTrainingController extends BaseController<AKDNTraining> {

  public static final String PATH = "akdn-training";

  public AKDNTrainingController(AKDNTrainingService akdnTrainingService) {
    super(akdnTrainingService);
  }
}

package com.inner.satisfaction.backend.lookups.religiousqualification;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + ReligiousQualificationController.PATH)
public class ReligiousQualificationController extends BaseController<ReligiousQualification> {

  public static final String PATH = "religious-qualification";

  public ReligiousQualificationController(ReligiousQualificationService religiousQualificationService) {
    super(religiousQualificationService);
  }
}

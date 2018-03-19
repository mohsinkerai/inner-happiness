package com.inner.satisfaction.backend.constants.dynamic.highestLevelStudy;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;
import static com.inner.satisfaction.backend.constants.dynamic.highestLevelStudy.HighestLevelOfStudyController.PATH;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + PATH)
public class HighestLevelOfStudyController extends BaseController<HighestLevelOfStudy> {

  public static final String PATH = "highest-level-of-study";

  public HighestLevelOfStudyController(HighestLevelOfStudyService highestLevelOfStudyService) {
    super(highestLevelOfStudyService);
  }
}

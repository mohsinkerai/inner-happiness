package com.inner.satisfaction.backend.lookups.areaofstudy;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;
import static com.inner.satisfaction.backend.lookups.areaofstudy.AreaOfStudyController.PATH;

@RestController
@RequestMapping(CONSTANTS_PREFIX + PATH)
public class AreaOfStudyController extends BaseController<AreaOfStudy> {

  public static final String PATH = "area-of-study";

  public AreaOfStudyController(AreaOfStudyService areaOfStudyService) {
    super(areaOfStudyService);
  }
}

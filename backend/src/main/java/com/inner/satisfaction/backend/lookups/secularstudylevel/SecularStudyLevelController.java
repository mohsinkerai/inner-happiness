package com.inner.satisfaction.backend.lookups.secularstudylevel;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + SecularStudyLevelController.PATH)
public class SecularStudyLevelController extends BaseController<SecularStudyLevel> {

  public static final String PATH = "secular-study-level";

  public SecularStudyLevelController(SecularStudyLevelService secularStudyLevelService) {
    super(secularStudyLevelService);
  }
}

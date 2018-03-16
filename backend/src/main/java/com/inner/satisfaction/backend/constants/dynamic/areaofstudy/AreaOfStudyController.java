package com.inner.satisfaction.backend.constants.dynamic.areaofstudy;

import com.inner.satisfaction.backend.base.BaseController;
import com.inner.satisfaction.backend.company.Company;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;
import static com.inner.satisfaction.backend.constants.dynamic.educationaldegree.EducationalDegreeController.PATH;

@RestController
@RequestMapping(PREFIX + PATH)
public class AreaOfStudyController extends BaseController<AreaOfStudy> {

  public static final String PATH = "area-of-study";

  public AreaOfStudyController(AreaOfStudyService areaOfStudyService) {
    super(areaOfStudyService);
  }
}

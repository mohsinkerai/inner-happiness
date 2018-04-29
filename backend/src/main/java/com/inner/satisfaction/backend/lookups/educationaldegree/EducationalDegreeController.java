package com.inner.satisfaction.backend.lookups.educationaldegree;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;
import static com.inner.satisfaction.backend.lookups.educationaldegree.EducationalDegreeController.PATH;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + PATH)
public class EducationalDegreeController extends BaseController<EducationalDegree> {

  public static final String PATH = "educational-degree";

  public EducationalDegreeController(EducationalDegreeService educationalDegreeService) {
    super(educationalDegreeService);
  }
}

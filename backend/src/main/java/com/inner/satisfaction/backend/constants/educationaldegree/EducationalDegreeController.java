package com.inner.satisfaction.backend.constants.educationaldegree;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;
import static com.inner.satisfaction.backend.constants.educationaldegree.EducationalDegreeController.PATH;

import com.inner.satisfaction.backend.base.BaseController;
import com.inner.satisfaction.backend.company.Company;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + PATH)
public class EducationalDegreeController extends BaseController<Company> {

  public static final String PATH = "educational-degree";

  public EducationalDegreeController(EducationalDegreeService educationalDegreeService) {
    super(educationalDegreeService);
  }
}

package com.inner.satisfaction.backend.lookups.educationalinstitution;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;
import static com.inner.satisfaction.backend.lookups.educationalinstitution.EducationalInstitutionController.PATH;

@RestController
@RequestMapping(CONSTANTS_PREFIX + PATH)
public class EducationalInstitutionController extends BaseController<EducationalInstitution> {

  public static final String PATH = "educational-institution";

  public EducationalInstitutionController(
    EducationalInstitutionService educationalInstitutionService) {
    super(educationalInstitutionService);
  }
}

package com.inner.satisfaction.backend.constants.dynamic.fieldofexperties;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;
import static com.inner.satisfaction.backend.constants.dynamic.educationaldegree.EducationalDegreeController.PATH;

@RestController
@RequestMapping(PREFIX + PATH)
public class FieldOfExpertiseController extends BaseController<FieldOfExpertise> {

  public static final String PATH = "field-of-expertise";

  public FieldOfExpertiseController(FieldOfExpertiseService fieldOfExpertiseService) {
    super(fieldOfExpertiseService);
  }
}

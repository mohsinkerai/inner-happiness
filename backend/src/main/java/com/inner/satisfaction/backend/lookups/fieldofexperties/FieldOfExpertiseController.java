package com.inner.satisfaction.backend.lookups.fieldofexperties;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;
import static com.inner.satisfaction.backend.lookups.fieldofexperties.FieldOfExpertiseController.PATH;

@RestController
@RequestMapping(CONSTANTS_PREFIX + PATH)
public class FieldOfExpertiseController extends BaseController<FieldOfExpertise> {

  public static final String PATH = "field-of-expertise";

  public FieldOfExpertiseController(FieldOfExpertiseService fieldOfExpertiseService) {
    super(fieldOfExpertiseService);
  }
}

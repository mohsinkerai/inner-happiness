package com.inner.satisfaction.backend.lookups.fieldofinterest;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + FieldOfInterestController.PATH)
public class FieldOfInterestController extends BaseController<FieldOfInterest> {

  public static final String PATH = "field-of-interest";

  public FieldOfInterestController(FieldOfInterestService fieldOfInterestService) {
    super(fieldOfInterestService);
  }
}

package com.inner.satisfaction.backend.lookups.occupation;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;
import static com.inner.satisfaction.backend.lookups.occupation.OccupationController.PATH;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + PATH)
public class OccupationController extends BaseController<Occupation> {

  public static final String PATH = "occupation";

  public OccupationController(OccupationService occupationService) {
    super(occupationService);
  }
}

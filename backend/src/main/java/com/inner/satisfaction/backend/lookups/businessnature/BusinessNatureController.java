package com.inner.satisfaction.backend.lookups.businessnature;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + BusinessNatureController.PATH)
public class BusinessNatureController extends BaseController<BusinessNature> {

  public static final String PATH = "business-nature";

  public BusinessNatureController(BusinessNatureService businessNatureService) {
    super(businessNatureService);
  }
}

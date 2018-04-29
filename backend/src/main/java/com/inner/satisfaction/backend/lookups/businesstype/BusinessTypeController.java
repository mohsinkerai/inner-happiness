package com.inner.satisfaction.backend.lookups.businesstype;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + BusinessTypeController.PATH)
public class BusinessTypeController extends BaseController<BusinessType> {

  public static final String PATH = "business-type";

  public BusinessTypeController(BusinessTypeService businessTypeService) {
    super(businessTypeService);
  }
}

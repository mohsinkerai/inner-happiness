package com.inner.satisfaction.backend.lookups.areaoforigin;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;
import static com.inner.satisfaction.backend.lookups.areaoforigin.AreaOfOriginController.PATH;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + PATH)
public class AreaOfOriginController extends BaseController<AreaOfOrigin> {

  public static final String PATH = "area-of-origin";

  public AreaOfOriginController(AreaOfOriginService areaOfOriginService) {
    super(areaOfOriginService);
  }
}

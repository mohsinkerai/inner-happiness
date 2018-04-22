package com.inner.satisfaction.backend.lookups.maritalstatus;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;
import static com.inner.satisfaction.backend.lookups.maritalstatus.MaritalStatusController.PATH;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + PATH)
public class MaritalStatusController extends BaseController<MaritalStatus> {

  public static final String PATH = "marital-status";

  public MaritalStatusController(MaritalStatusService maritalStatusService) {
    super(maritalStatusService);
  }
}

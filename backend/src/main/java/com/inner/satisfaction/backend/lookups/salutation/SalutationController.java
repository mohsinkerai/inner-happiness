package com.inner.satisfaction.backend.lookups.salutation;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;
import static com.inner.satisfaction.backend.lookups.salutation.SalutationController.PATH;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + PATH)
public class SalutationController extends BaseController<Salutation> {

  public static final String PATH = "salutation";

  public SalutationController(SalutationService salutationService) {
    super(salutationService);
  }
}

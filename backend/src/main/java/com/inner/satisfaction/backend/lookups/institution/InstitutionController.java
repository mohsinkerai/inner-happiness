package com.inner.satisfaction.backend.lookups.institution;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + InstitutionController.PATH)
public class InstitutionController extends BaseController<Institution> {

  public static final String PATH = "institution";

  public InstitutionController(InstitutionService institutionService) {
    super(institutionService);
  }
}

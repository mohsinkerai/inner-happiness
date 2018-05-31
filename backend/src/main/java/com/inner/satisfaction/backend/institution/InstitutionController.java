package com.inner.satisfaction.backend.institution;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + "institution")
public class InstitutionController extends BaseController<Institution> {

  public InstitutionController(InstitutionService institutionService) {
    super(institutionService);
  }
}

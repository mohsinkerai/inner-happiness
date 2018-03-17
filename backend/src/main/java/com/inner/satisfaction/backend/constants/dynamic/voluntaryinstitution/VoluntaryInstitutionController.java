package com.inner.satisfaction.backend.constants.dynamic.voluntaryinstitution;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;
import static com.inner.satisfaction.backend.constants.dynamic.voluntaryinstitution.VoluntaryInstitutionController.PATH;

@RestController
@RequestMapping(PREFIX + PATH)
public class VoluntaryInstitutionController extends BaseController<VoluntaryInstitution> {

  public static final String PATH = "voluntary-institution";

  public VoluntaryInstitutionController(VoluntaryInstitutionService voluntaryInstitutionService) {
    super(voluntaryInstitutionService);
  }
}

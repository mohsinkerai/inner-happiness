package com.inner.satisfaction.backend.lookups.voluntaryinstitution;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;
import static com.inner.satisfaction.backend.lookups.voluntaryinstitution.VoluntaryInstitutionController.PATH;

@RestController
@RequestMapping(CONSTANTS_PREFIX + PATH)
public class VoluntaryInstitutionController extends BaseController<VoluntaryInstitution> {

  public static final String PATH = "voluntary-institution";

  public VoluntaryInstitutionController(VoluntaryInstitutionService voluntaryInstitutionService) {
    super(voluntaryInstitutionService);
  }
}

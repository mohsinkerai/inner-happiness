package com.inner.satisfaction.backend.lookups.publicserviceinstitution;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + PublicServiceInstitutionController.PATH)
public class PublicServiceInstitutionController extends BaseController<PublicServiceInstitution> {

  public static final String PATH = "public-service-institution";

  public PublicServiceInstitutionController(PublicServiceInstitutionService publicServiceInstitutionService) {
    super(publicServiceInstitutionService);
  }
}

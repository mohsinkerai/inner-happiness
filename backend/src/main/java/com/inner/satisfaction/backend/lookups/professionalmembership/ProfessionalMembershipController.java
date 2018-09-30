package com.inner.satisfaction.backend.lookups.professionalmembership;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;
import static com.inner.satisfaction.backend.lookups.professionalmembership.ProfessionalMembershipController.PATH;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + PATH)
public class ProfessionalMembershipController extends BaseController<ProfessionalMembership> {

  public static final String PATH = "professional-membership";

  public ProfessionalMembershipController(ProfessionalMembershipService professionalMembershipService) {
    super(professionalMembershipService);
  }
}

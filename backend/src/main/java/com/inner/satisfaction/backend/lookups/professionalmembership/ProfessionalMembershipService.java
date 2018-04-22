package com.inner.satisfaction.backend.lookups.professionalmembership;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class ProfessionalMembershipService extends SimpleBaseService<ProfessionalMembership> {

  protected ProfessionalMembershipService(
      ProfessionalMembershipRepository baseRepository) {
    super(baseRepository);
  }
}

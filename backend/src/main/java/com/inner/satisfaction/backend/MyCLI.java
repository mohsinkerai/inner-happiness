package com.inner.satisfaction.backend;

import com.inner.satisfaction.backend.person.professionalmembership.PersonProfessionalMembership;
import com.inner.satisfaction.backend.person.professionalmembership.PersonProfessionalMembershipService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

@Component
public class MyCLI implements CommandLineRunner {

  @Autowired
  PersonProfessionalMembershipService personProfessionalMembershipService;

  @Override
  public void run(String... args) throws Exception {

    PersonProfessionalMembership ppm = PersonProfessionalMembership.builder()
      .personId(21)
      .professionalMembershipId(2)
      .build();

    personProfessionalMembershipService.save(ppm);
  }
}

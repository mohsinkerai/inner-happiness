package com.inner.satisfaction.backend.person.professionalmembership;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import java.util.List;
import org.springframework.stereotype.Component;

@Component
public class PersonProfessionalMembershipService extends SimpleBaseService<PersonProfessionalMembership> {

  private final PersonProfessionalMembershipRepository personProfessionalMembershipRepository;

  public PersonProfessionalMembershipService(
    PersonProfessionalMembershipRepository personProfessionalMembershipRepository) {
    super(personProfessionalMembershipRepository);
    this.personProfessionalMembershipRepository = personProfessionalMembershipRepository;
  }

  public void removeByPersonId(long personId) {
    personProfessionalMembershipRepository.removeByPersonId(personId);
  }

  public List<PersonProfessionalMembership> findByPersonId(long personId) {
    return personProfessionalMembershipRepository.findByPersonId(personId);
  }
}

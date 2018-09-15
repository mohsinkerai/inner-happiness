package com.inner.satisfaction.backend.person.lookup.fieldofexpertise;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import java.util.List;
import org.springframework.stereotype.Component;

@Component
public class PersonFieldOfExpertiseService extends SimpleBaseService<PersonFieldOfExpertise> {

  private final PersonFieldOfExpertiseRepository personFieldOfExpertiseRepository;

  public PersonFieldOfExpertiseService(
    PersonFieldOfExpertiseRepository personFieldOfExpertiseRepository) {
    super(personFieldOfExpertiseRepository);
    this.personFieldOfExpertiseRepository = personFieldOfExpertiseRepository;
  }

  public void removeByPersonId(long personId) {
    personFieldOfExpertiseRepository.removeByPersonId(personId);
  }

  public List<PersonFieldOfExpertise> findByPersonId(long personId) {
    return personFieldOfExpertiseRepository.findByPersonId(personId);
  }
}

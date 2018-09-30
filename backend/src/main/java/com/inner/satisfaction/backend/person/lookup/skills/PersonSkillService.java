package com.inner.satisfaction.backend.person.lookup.skills;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import java.util.List;
import org.springframework.stereotype.Component;

@Component
public class PersonSkillService extends SimpleBaseService<PersonSkill> {

  private final PersonSkillRepository personSkillRepository;

  public PersonSkillService(
    PersonSkillRepository personSkillRepository) {
    super(personSkillRepository);
    this.personSkillRepository = personSkillRepository;
  }

  public void removeByPersonId(long personId) {
    personSkillRepository.removeByPersonId(personId);
  }

  public List<PersonSkill> findByPersonId(long personId) {
    return personSkillRepository.findByPersonId(personId);
  }
}

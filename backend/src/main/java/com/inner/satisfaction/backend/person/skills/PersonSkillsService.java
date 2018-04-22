package com.inner.satisfaction.backend.person.skills;

import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.person.relation.PersonRelationPerson;
import org.springframework.stereotype.Service;

@Service
public class PersonSkillsService extends BaseService<PersonRelationPerson>{

  protected PersonSkillsService(
      PersonSkillsRepository baseRepository,
      PersonSkillsValidation personSkillsValidation) {
    super(baseRepository, personSkillsValidation);
  }
}

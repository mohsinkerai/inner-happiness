package com.inner.satisfaction.backend.person.education;

import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.person.relation.PersonRelationPerson;
import org.springframework.stereotype.Service;

@Service
public class PersonEducationService extends BaseService<PersonRelationPerson>{

  protected PersonEducationService(
      PersonEducationRepository baseRepository,
      PersonEducationValidation personEducationValidation) {
    super(baseRepository, personEducationValidation);
  }
}

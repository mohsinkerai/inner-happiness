package com.inner.satisfaction.backend.person.relation;

import com.inner.satisfaction.backend.base.BaseService;
import org.springframework.stereotype.Service;

@Service
public class PersonRelationPersonService extends BaseService<PersonRelationPerson>{

  protected PersonRelationPersonService(
      PersonRelationPersonRepository baseRepository,
      PersonRelationPersonValidation personRelationPersonValidation) {
    super(baseRepository, personRelationPersonValidation);
  }
}

package com.inner.satisfaction.backend.person.relation;

import com.inner.satisfaction.backend.base.BaseEntityValidation;
import com.inner.satisfaction.backend.base.MyValidationException;
import org.springframework.stereotype.Component;

@Component
public class PersonRelationPersonValidation implements BaseEntityValidation<PersonRelationPerson> {

  @Override
  public void isValidToSave(PersonRelationPerson entity) throws MyValidationException {

  }

  @Override
  public void isValidToDelete(PersonRelationPerson entity) throws MyValidationException {

  }
}

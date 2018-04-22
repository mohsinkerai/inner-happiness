package com.inner.satisfaction.backend.person.akdntraining;

import com.inner.satisfaction.backend.base.BaseEntityValidation;
import com.inner.satisfaction.backend.base.MyValidationException;
import com.inner.satisfaction.backend.person.relation.PersonRelationPerson;
import org.springframework.stereotype.Component;

@Component
public class PersonAkdnTrainingValidation implements BaseEntityValidation<PersonAkdnTraining> {

  @Override
  public void isValidToSave(PersonAkdnTraining entity) throws MyValidationException {

  }

  @Override
  public void isValidToDelete(PersonAkdnTraining entity) throws MyValidationException {

  }
}

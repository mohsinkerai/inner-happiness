package com.inner.satisfaction.backend.person;

import com.inner.satisfaction.backend.base.BaseEntityValidation;
import com.inner.satisfaction.backend.base.MyValidationException;
import org.springframework.stereotype.Component;

@Component
public class PersonValidation implements BaseEntityValidation<Person> {

  @Override
  public void isValidToSave(Person entity) throws MyValidationException {

  }

  @Override
  public void isValidToDelete(Person entity) throws MyValidationException {

  }
}

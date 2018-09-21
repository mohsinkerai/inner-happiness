package com.inner.satisfaction.backend.person;

import com.inner.satisfaction.backend.base.BaseEntityValidation;
import com.inner.satisfaction.backend.base.MyValidationException;
import org.springframework.stereotype.Component;
import org.springframework.util.Assert;

@Component
public class PersonValidation implements BaseEntityValidation<Person> {

  @Override
  public void isValidToSave(Person entity) throws MyValidationException {
    Assert.notNull(entity, "Person Entity should not be null");
  }

  @Override
  public void isValidToDelete(Person entity) throws MyValidationException {

  }
}

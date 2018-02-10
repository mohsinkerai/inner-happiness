package com.inner.satisfaction.backend.cycle;

import com.inner.satisfaction.backend.base.BaseEntityValidation;
import com.inner.satisfaction.backend.base.MyValidationException;
import org.springframework.stereotype.Component;

@Component
public class CycleValidation implements BaseEntityValidation<Cycle> {

  @Override
  public void isValidToSave(Cycle entity) throws MyValidationException {

  }

  @Override
  public void isValidToDelete(Cycle entity) throws MyValidationException {

  }
}

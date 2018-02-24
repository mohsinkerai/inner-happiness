package com.inner.satisfaction.backend.position;

import com.inner.satisfaction.backend.base.BaseEntityValidation;
import com.inner.satisfaction.backend.base.MyValidationException;
import org.springframework.stereotype.Component;

@Component
public class PositionValidation implements BaseEntityValidation<Position> {

  @Override
  public void isValidToSave(Position entity) throws MyValidationException {

  }

  @Override
  public void isValidToDelete(Position entity) throws MyValidationException {

  }
}

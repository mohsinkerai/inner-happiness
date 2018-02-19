package com.inner.satisfaction.backend.position.active;

import com.inner.satisfaction.backend.base.BaseEntityValidation;
import com.inner.satisfaction.backend.base.MyValidationException;
import org.springframework.stereotype.Component;

@Component
public class PositionOnActiveLevelValidation implements BaseEntityValidation<PositionOnActiveLevel> {

  @Override
  public void isValidToSave(PositionOnActiveLevel entity) throws MyValidationException {

  }

  @Override
  public void isValidToDelete(PositionOnActiveLevel entity) throws MyValidationException {

  }
}

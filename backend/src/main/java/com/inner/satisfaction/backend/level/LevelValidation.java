package com.inner.satisfaction.backend.level;

import com.inner.satisfaction.backend.base.BaseEntityValidation;
import com.inner.satisfaction.backend.base.MyValidationException;
import org.springframework.stereotype.Component;

@Component
public class LevelValidation implements BaseEntityValidation<Level> {

  @Override
  public void isValidToSave(Level entity) throws MyValidationException {

  }

  @Override
  public void isValidToDelete(Level entity) throws MyValidationException {

  }
}

package com.inner.satisfaction.backend.level.active;

import com.inner.satisfaction.backend.base.BaseEntityValidation;
import com.inner.satisfaction.backend.base.MyValidationException;
import org.springframework.stereotype.Component;

@Component
public class LevelActiveValidation implements BaseEntityValidation<LevelActive> {

  @Override
  public void isValidToSave(LevelActive entity) throws MyValidationException {

  }

  @Override
  public void isValidToDelete(LevelActive entity) throws MyValidationException {

  }
}

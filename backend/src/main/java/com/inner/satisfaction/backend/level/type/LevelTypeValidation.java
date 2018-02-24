package com.inner.satisfaction.backend.level.type;

import com.inner.satisfaction.backend.base.BaseEntityValidation;
import com.inner.satisfaction.backend.base.MyValidationException;
import java.util.Optional;
import org.springframework.stereotype.Component;

@Component
public class LevelTypeValidation implements BaseEntityValidation<LevelType> {

  @Override
  public void isValidToSave(LevelType entity) throws MyValidationException {
    boolean isValidEntity = Optional.ofNullable(entity.getName())
        .filter(s -> s.length() > 0).isPresent();
    if (!isValidEntity) {
      throw new MyValidationException();
    }
  }

  @Override
  public void isValidToDelete(LevelType entity) throws MyValidationException {

  }
}

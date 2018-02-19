package com.inner.satisfaction.backend.cycle.position;

import com.inner.satisfaction.backend.base.BaseEntityValidation;
import com.inner.satisfaction.backend.base.MyValidationException;
import org.springframework.stereotype.Component;

@Component
public class CyclePositionOnActiveLevelValidation implements BaseEntityValidation<CyclePositionOnActiveLevel> {

  @Override
  public void isValidToSave(CyclePositionOnActiveLevel entity) throws MyValidationException {

  }

  @Override
  public void isValidToDelete(CyclePositionOnActiveLevel entity) throws MyValidationException {

  }
}

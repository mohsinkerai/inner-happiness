package com.inner.satisfaction.backend.position.active;

import com.inner.satisfaction.backend.base.BaseEntityValidation;
import com.inner.satisfaction.backend.base.MyValidationException;
import org.springframework.stereotype.Component;

@Component
public class PositionOnInstitutionValidation implements BaseEntityValidation<PositionOnInstitution> {

  @Override
  public void isValidToSave(PositionOnInstitution entity) throws MyValidationException {

  }

  @Override
  public void isValidToDelete(PositionOnInstitution entity) throws MyValidationException {

  }
}

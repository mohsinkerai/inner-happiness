package com.inner.satisfaction.backend.cycle.position;

import com.inner.satisfaction.backend.base.BaseEntityValidation;
import com.inner.satisfaction.backend.base.MyValidationException;
import org.springframework.stereotype.Component;

@Component
public class CyclePositionOnInstitutionValidation implements BaseEntityValidation<CyclePositionOnInstitution> {

  @Override
  public void isValidToSave(CyclePositionOnInstitution entity) throws MyValidationException {

  }

  @Override
  public void isValidToDelete(CyclePositionOnInstitution entity) throws MyValidationException {

  }
}

package com.inner.satisfaction.backend.institution;

import com.inner.satisfaction.backend.base.BaseEntityValidation;
import com.inner.satisfaction.backend.base.MyValidationException;
import org.springframework.stereotype.Component;

@Component
public class InstitutionValidation implements BaseEntityValidation<Institution> {

  @Override
  public void isValidToSave(Institution entity) throws MyValidationException {

  }

  @Override
  public void isValidToDelete(Institution entity) throws MyValidationException {

  }
}

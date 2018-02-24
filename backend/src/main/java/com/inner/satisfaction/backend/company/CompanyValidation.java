package com.inner.satisfaction.backend.company;

import com.inner.satisfaction.backend.base.BaseEntityValidation;
import com.inner.satisfaction.backend.base.MyValidationException;
import org.springframework.stereotype.Component;

@Component
public class CompanyValidation implements BaseEntityValidation<Company> {

  @Override
  public void isValidToSave(Company entity) throws MyValidationException {

  }

  @Override
  public void isValidToDelete(Company entity) throws MyValidationException {

  }
}

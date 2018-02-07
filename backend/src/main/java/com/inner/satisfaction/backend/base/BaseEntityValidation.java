package com.inner.satisfaction.backend.base;

public interface BaseEntityValidation <E extends BaseEntity> {

  void isValidToSave(E entity) throws MyValidationException;
  void isValidToDelete(E entity) throws MyValidationException;
}

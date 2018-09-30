package com.inner.satisfaction.backend.appointment;

import com.inner.satisfaction.backend.base.BaseEntityValidation;
import com.inner.satisfaction.backend.base.MyValidationException;
import org.springframework.stereotype.Component;

@Component
public class AppointmentPositionValidation implements BaseEntityValidation<AppointmentPosition> {

  @Override
  public void isValidToSave(AppointmentPosition entity) throws MyValidationException {

  }

  @Override
  public void isValidToDelete(AppointmentPosition entity) throws MyValidationException {

  }
}

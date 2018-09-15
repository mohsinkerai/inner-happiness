package com.inner.satisfaction.backend.appointment;

import com.inner.satisfaction.backend.base.BaseService;
import org.springframework.stereotype.Service;

@Service
public class AppointmentPositionService extends BaseService<AppointmentPosition>{

  protected AppointmentPositionService(
      AppointmentPositionRepository baseRepository,
      AppointmentPositionValidation appointmentPositionValidation) {
    super(baseRepository, appointmentPositionValidation);
  }
}

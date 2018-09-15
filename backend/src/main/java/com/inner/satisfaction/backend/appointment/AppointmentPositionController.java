package com.inner.satisfaction.backend.appointment;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + "appointment-position")
public class AppointmentPositionController extends BaseController<AppointmentPosition> {

  public AppointmentPositionController(AppointmentPositionService appointmentPositionService) {
    super(appointmentPositionService);
  }
}

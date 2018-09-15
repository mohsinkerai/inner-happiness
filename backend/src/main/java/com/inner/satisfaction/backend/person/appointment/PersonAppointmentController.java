package com.inner.satisfaction.backend.person.appointment;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + PersonAppointmentController.PATH)
public class PersonAppointmentController extends BaseController<PersonAppointment> {

  public static final String PATH = "person/appointment";

  public PersonAppointmentController(PersonAppointmentService personAppointmentService) {
    super(personAppointmentService);
  }
}

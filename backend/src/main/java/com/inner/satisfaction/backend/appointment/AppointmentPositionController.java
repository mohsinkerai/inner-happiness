package com.inner.satisfaction.backend.appointment;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import java.util.List;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + "appointment-position")
public class AppointmentPositionController extends BaseController<AppointmentPosition> {

  private final AppointmentPositionService appointmentPositionService;

  public AppointmentPositionController(AppointmentPositionService appointmentPositionService) {
    super(appointmentPositionService);
    this.appointmentPositionService = appointmentPositionService;
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findAppointmentOfPersonIdAndIsMowlaAppointee", method = RequestMethod.GET)
  public List<AppointmentPositionDto> findAppointmentsOfPersonIdAndIsMowlaAppointee(
    @RequestParam("personId") long personId,
    @RequestParam("isMowlaAppointee") boolean isMowlaAppointee) {
    return appointmentPositionService.findAppointmentsOfPersonIdAndIsMowlaAppointee(personId, isMowlaAppointee);
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findByCycleIdAndInstitutionId", method = RequestMethod.GET)
  public List<ApptPositionDto> findByCycleIdAndInstitutionId(
    @RequestParam("cycleId") long cycleId,
    @RequestParam("institutionId") long institutionId) {
    return appointmentPositionService.findByCycleIdAndInstitutionId(cycleId, institutionId);
  }
}

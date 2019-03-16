package com.inner.satisfaction.backend.appointment;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.appointment.dto.AppointmentPositionDto;
import com.inner.satisfaction.backend.appointment.dto.ApptPositionDto;
import com.inner.satisfaction.backend.appointment.dto.MidtermPositionCreateRequestDto;
import com.inner.satisfaction.backend.authentication.token.AuthenticationToken;
import com.inner.satisfaction.backend.authentication.user.AuthenticationUser;
import com.inner.satisfaction.backend.base.BaseController;
import com.inner.satisfaction.backend.company.Company;
import java.util.List;
import javax.transaction.Transactional;
import org.springframework.data.domain.Page;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + "appointment-position")
public class AppointmentPositionController extends BaseController<AppointmentPosition> {

  private final AppointmentPositionFacade appointmentPositionFacade;

  public AppointmentPositionController(
    AppointmentPositionService appointmentPositionService,
    AppointmentPositionFacade appointmentPositionFacade) {
    super(appointmentPositionService);
    this.appointmentPositionFacade = appointmentPositionFacade;
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findAppointmentOfPersonIdAndIsMowlaAppointee", method = RequestMethod.GET)
  public List<AppointmentPositionDto> findAppointmentsOfPersonIdAndIsMowlaAppointee(
    @RequestParam("personId") long personId,
    @RequestParam("isMowlaAppointee") boolean isMowlaAppointee) {
    return appointmentPositionFacade
      .findAppointmentsOfPersonIdAndIsMowlaAppointee(personId, isMowlaAppointee);
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findByCycleIdAndInstitutionId", method = RequestMethod.GET)
  public List<ApptPositionDto> findByCycleIdAndInstitutionId(
    @RequestParam("cycleId") long cycleId,
    @RequestParam("institutionId") long institutionId) {
    return appointmentPositionFacade.findByCycleIdAndInstitutionId(cycleId, institutionId);
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findByCycleIdAndInstitutionIdAndPositionIdAndSeatNo", method = RequestMethod.GET)
  public List<ApptPositionDto> findByCycleIdAndInstitutionIdAndPositionIdAndSeatId(
    @RequestParam("cycleId") long cycleId,
    @RequestParam("institutionId") long institutionId,
    @RequestParam("positionId") long positionId,
    @RequestParam("seatNo") long seatNo) {
    return appointmentPositionFacade
      .findByCycleIdAndInstitutionIdPositionIdAndSeatNo(cycleId, institutionId, positionId, seatNo);
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findByCycleIdWhereNoOneIsRecommended", method = RequestMethod.GET)
  public List<ApptPositionDto> findByCycleIdWhereNoOneIsRecommended(
    @RequestParam("cycleId") long cycleId) {
    return appointmentPositionFacade.findByCycleIdWhereNoOneIsRecommended(cycleId);
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/midterm", method = RequestMethod.POST)
  public List<AppointmentPosition> introduceMidtermPosition(
    @RequestBody MidtermPositionCreateRequestDto requestDto) {
    return appointmentPositionFacade.createMidtermPosition(requestDto);
  }

  @Override
  public AppointmentPosition save(@RequestBody AppointmentPosition appointmentPosition) {
    return appointmentPositionFacade.save(appointmentPosition);
  }

  @Override
  @Transactional
  public ResponseEntity<AppointmentPosition> findOne(@PathVariable("id") Long entityId) {
    ResponseEntity<AppointmentPosition> appointmentPosition = super.findOne(entityId);
    Company company = appointmentPosition.getBody().getCompany();
    Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
    if(company != null && authentication instanceof AuthenticationToken ) {
      AuthenticationToken authenticationToken = (AuthenticationToken) authentication;
      if(authenticationToken.getCompanyId() != company.getId()) {
        throw new RuntimeException("Invalid Company");
      }
    }
    return appointmentPosition;
  }
}

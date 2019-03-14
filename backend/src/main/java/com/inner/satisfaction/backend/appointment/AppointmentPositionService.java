package com.inner.satisfaction.backend.appointment;

import static com.inner.satisfaction.backend.appointment.AppointmentPositionState.CREATED;

import com.google.common.collect.Lists;
import com.inner.satisfaction.backend.appointment.dto.AppointmentPositionDeletedEventDto;
import com.inner.satisfaction.backend.authentication.token.AuthenticationToken;
import com.inner.satisfaction.backend.base.BaseService;
import java.util.List;
import java.util.Optional;
import org.springframework.context.ApplicationEventPublisher;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Service;

@Service
public class AppointmentPositionService extends BaseService<AppointmentPosition> {

  private final AppointmentPositionRepository appointmentPositionRepository;
  private final ApplicationEventPublisher applicationEventPublisher;

  protected AppointmentPositionService(
    AppointmentPositionRepository baseRepository,
    AppointmentPositionValidation appointmentPositionValidation,
    ApplicationEventPublisher applicationEventPublisher) {
    super(baseRepository, appointmentPositionValidation);
    this.appointmentPositionRepository = baseRepository;
    this.applicationEventPublisher = applicationEventPublisher;
  }

  public List<AppointmentPosition> findByInstitutionIdAndSeatNoAndCycleIdAndPositionId(long cycleId,
    long institutionId, long seatNo, long positionId) {
    return appointmentPositionRepository
      .findByInstitutionIdAndSeatNoAndCycleIdAndPositionId(institutionId, seatNo, cycleId,
        positionId);
  }

  public List<AppointmentPosition> findByCycleId(long cycleId) {
    return appointmentPositionRepository.findByCycleId(cycleId);
  }

  public List<AppointmentPosition> findByCycleIdAndInstitutionId(long cycleId,
    long institutionId) {
    return appointmentPositionRepository.findByCycleIdAndInstitutionId(cycleId, institutionId);
  }

  public List<AppointmentPosition> findByCycleIdAndInstitutionIdAndPositionIdAndSeatNo(
    long cycleId, long institutionId, long positionId,
    long seatId) {
    return appointmentPositionRepository
      .findByCycleIdAndInstitutionIdAndPositionIdAndSeatNo(cycleId, institutionId, positionId,
        seatId);
  }

  public List<AppointmentPosition> findAppointmentsOfPersonInCycle(long personId, long cycleId) {
    return appointmentPositionRepository
      .findByCycleIdAndPersonIdAndRecommendedTrue(cycleId, personId);
  }

  public List<AppointmentPosition> fetchActiveAppointmentsForCycle(Long id) {
    return appointmentPositionRepository.findByCycleIdAndState(id, CREATED);
  }

  public List<AppointmentPosition> fetchAppointmentsForCycleByState(Long id, String state) {
    return appointmentPositionRepository.findByCycleIdAndState(id, state);
  }

  public List<AppointmentPosition> findByCycleIdWhereNoOneIsRecommended(long cycleId) {
    return appointmentPositionRepository.findByCycleIdWhereNoOneIsRecommended(cycleId);
  }

  @Override
  public void delete(AppointmentPosition appointmentPosition) {
    super.delete(appointmentPosition);
    applicationEventPublisher
      .publishEvent(new AppointmentPositionDeletedEventDto(appointmentPosition.getId()));
  }

  @Override
  public List<AppointmentPosition> findAll() {
    return getCurrentUserCompanyId().map(appointmentPositionRepository::findByCompanyId)
      .orElse(Lists.newArrayList());
  }

  @Override
  public Page<AppointmentPosition> findAll(Pageable pageable) {
    return getCurrentUserCompanyId()
      .map(companyId -> appointmentPositionRepository.findByCompanyId(companyId, pageable))
      .orElse(Page.empty());
  }

  @Override
  public AppointmentPosition findOne(Long id) {
    AppointmentPosition one = super.findOne(id);
    Optional<Long> currentUserCompanyId = getCurrentUserCompanyId();
    if(one != null && currentUserCompanyId.isPresent()) {
      return one.getCompany().getId() == currentUserCompanyId.get() ? one : null;
    }
    return one;
  }

  public void updateState(Long id, String appointed) {
    AppointmentPosition one = findOne(id);
    one.setState(appointed);
    save(one);
  }

  private Optional<Long> getCurrentUserCompanyId() {
    Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
    if (authentication instanceof AuthenticationToken) {
      AuthenticationToken authenticationToken = (AuthenticationToken) authentication;
      return Optional.of(((AuthenticationToken) authentication).getCompanyId());
    }
    return Optional.empty();
  }
}

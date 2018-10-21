package com.inner.satisfaction.backend.appointment;

import com.inner.satisfaction.backend.base.BaseService;
import java.util.List;
import org.springframework.stereotype.Service;

@Service
public class AppointmentPositionService extends BaseService<AppointmentPosition> {

  private final AppointmentPositionRepository appointmentPositionRepository;

  protected AppointmentPositionService(
    AppointmentPositionRepository baseRepository,
    AppointmentPositionValidation appointmentPositionValidation) {
    super(baseRepository, appointmentPositionValidation);
    this.appointmentPositionRepository = baseRepository;
  }

  public AppointmentPosition findByInstitutionIdAndSeatNoAndCycleIdAndPositionId(long cycleId, long institutionId, long seatNo, long positionId) {
    return appointmentPositionRepository
      .findByInstitutionIdAndSeatNoAndCycleIdAndPositionId(institutionId, seatNo, positionId,
        cycleId);
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
    return appointmentPositionRepository.findByCycleIdAndInstitutionIdAndPositionIdAndSeatNo(cycleId, institutionId, positionId, seatId);
  }
}

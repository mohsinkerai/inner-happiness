package com.inner.satisfaction.backend.appointment;

import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.cycle.CycleService;
import com.inner.satisfaction.backend.institution.InstitutionService;
import com.inner.satisfaction.backend.person.PersonService;
import com.inner.satisfaction.backend.person.appointment.PersonAppointment;
import com.inner.satisfaction.backend.person.appointment.PersonAppointmentDto;
import com.inner.satisfaction.backend.person.appointment.PersonAppointmentService;
import com.inner.satisfaction.backend.position.PositionService;
import java.util.List;
import java.util.stream.Collectors;
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
}

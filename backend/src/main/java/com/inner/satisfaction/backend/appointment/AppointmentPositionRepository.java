package com.inner.satisfaction.backend.appointment;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.stereotype.Repository;

@Repository
public interface AppointmentPositionRepository extends BaseRepository<AppointmentPosition> {

  List<AppointmentPosition> findByCycleIdAndInstitutionId(long cycleId, long institutionId);

  List<AppointmentPosition> findByCycleId(long cycleId);

  AppointmentPosition findByInstitutionIdAndSeatNoAndCycleIdAndPositionId(long institutionId, long seatNo, long positionId, long cycleId);

  List<AppointmentPosition> findByCycleIdAndInstitutionIdAndPositionIdAndSeatNo(long cycleId, long institutionId, long positionId, long seatNo);
}

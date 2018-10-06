package com.inner.satisfaction.backend.appointment;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.stereotype.Repository;

@Repository
public interface AppointmentPositionRepository extends BaseRepository<AppointmentPosition> {

  List<AppointmentPosition> findByCycleIdAndInstitutionId(long cycleId, long institutionId);
}

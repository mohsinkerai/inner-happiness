package com.inner.satisfaction.backend.position.active;

import com.inner.satisfaction.backend.base.BaseService;
import java.util.List;
import org.springframework.stereotype.Service;

@Service
public class PositionOnInstitutionService extends BaseService<PositionOnInstitution> {

  private final PositionOnInstitutionRepository positionOnInstitutionRepository;

  protected PositionOnInstitutionService(
    PositionOnInstitutionRepository baseRepository,
    PositionOnInstitutionValidation positionOnInstitutionValidation) {
    super(baseRepository, positionOnInstitutionValidation);
    this.positionOnInstitutionRepository = baseRepository;
  }

  public List<PositionOnInstitution> findByInstitutionId(long institutionId) {
    return positionOnInstitutionRepository.findByInstitutionId(institutionId);
  }

  public List<PositionOnInstitution> findByInstitutionIdAndCycleId(long institutionId, long cycleId) {
    return positionOnInstitutionRepository.findByCycleIdAndInstitutionId(cycleId, institutionId);
  }

  public PositionOnInstitution findByInstitutionIdAndCycleIdAndPositionId(long institutionId, long cycleId, long positionId) {
    return positionOnInstitutionRepository.findByCycleIdAndInstitutionIdAndPositionId(cycleId,institutionId,positionId);
  }
}

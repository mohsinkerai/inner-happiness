package com.inner.satisfaction.backend.institution;

import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.level.Level;
import com.inner.satisfaction.backend.level.LevelService;
import java.util.List;
import java.util.Set;
import java.util.stream.Collectors;
import org.springframework.stereotype.Service;

@Service
public class InstitutionService extends BaseService<Institution> {

  private final LevelService levelService;
  private final InstitutionRepository repository;

  protected InstitutionService(
    InstitutionRepository baseRepository,
    InstitutionValidation institutionValidation,
    LevelService levelService) {
    super(baseRepository, institutionValidation);
    this.repository = baseRepository;
    this.levelService = levelService;
  }

  public List<Institution> findByLevelId(long levelId) {
    return repository.findByLevelId(levelId);
  }

  public List<Institution> findByLevelType(Long levelType) {
    Set<Level> levels = levelService.findByLevelTypeId(levelType.intValue());
    return levels
      .stream()
      .flatMap((level) -> repository.findByLevelId(level.getId()).stream())
      .collect(Collectors.toList());
  }
}

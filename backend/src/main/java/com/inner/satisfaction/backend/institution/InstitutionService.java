package com.inner.satisfaction.backend.institution;

import com.google.common.collect.Sets;
import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.error.AmsException;
import com.inner.satisfaction.backend.error.ErrorEnumType;
import com.inner.satisfaction.backend.level.Level;
import com.inner.satisfaction.backend.level.LevelService;
import java.util.List;
import java.util.Set;
import java.util.stream.Collectors;
import org.springframework.stereotype.Service;

@Service
public class InstitutionService extends BaseService<Institution> {

  private static final Set<String> ALLOWED_CATEGORIES = Sets.newHashSet("CAB", "ITREB", "COUNCIL");

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

  @Override
  public Institution save(Institution institution) {
    if (institution != null && institution.getCategory() != null && !ALLOWED_CATEGORIES
      .contains(institution.getCategory())) {
      new AmsException(ErrorEnumType.INVALID_INSTITUTION_CATEGORY_GIVEN);
    }
    return super.save(institution);
  }

  public List<Institution> findByCategory(String category) {
    return repository.findByCategory(category);
  }
}

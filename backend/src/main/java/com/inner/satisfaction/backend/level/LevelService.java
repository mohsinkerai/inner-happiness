package com.inner.satisfaction.backend.level;

import com.inner.satisfaction.backend.base.BaseService;
import java.util.Set;
import org.springframework.stereotype.Service;

@Service
public class LevelService extends BaseService<Level>{

  private final LevelRepository levelRepository;

  protected LevelService(
      LevelRepository levelRepository,
      LevelValidation levelValidation) {
    super(levelRepository, levelValidation);
    this.levelRepository = levelRepository;
  }

  public Set<Level> findByLevelParentId(long levelParentId) {
    return levelRepository.findByLevelParentId(levelParentId);
  }

  public Set<Level> findByLevelTypeId(int levelTypeId) {
    return levelRepository.findByLevelTypeId(levelTypeId);
  }
}

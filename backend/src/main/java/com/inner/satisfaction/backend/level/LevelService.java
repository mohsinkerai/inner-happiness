package com.inner.satisfaction.backend.level;

import com.inner.satisfaction.backend.appconfiguration.ApplicationConfiguration;
import com.inner.satisfaction.backend.appconfiguration.ApplicationConfigurationKeys;
import com.inner.satisfaction.backend.appconfiguration.ApplicationConfigurationService;
import com.inner.satisfaction.backend.base.BaseService;
import java.util.Set;
import javax.transaction.Transactional;
import org.springframework.stereotype.Service;
import org.springframework.util.Assert;

@Service
public class LevelService extends BaseService<Level> {

  private final ApplicationConfigurationService appConfigurationService;
  private final LevelRepository levelRepository;

  protected LevelService(
    LevelRepository levelRepository,
    LevelValidation levelValidation,
    ApplicationConfigurationService appConfigurationService) {
    super(levelRepository, levelValidation);
    this.levelRepository = levelRepository;
    this.appConfigurationService = appConfigurationService;
  }

  public Set<Level> findByLevelParentId(long levelParentId) {
    return levelRepository.findByLevelParentId(levelParentId);
  }

  public Set<Level> findByLevelTypeId(int levelTypeId) {
    return levelRepository.findByLevelTypeId(levelTypeId);
  }

  public void close(long levelId) {
    ApplicationConfiguration currentCycle = appConfigurationService
      .findByKey(ApplicationConfigurationKeys.CURRENT_CYCLE_ID.name());
    Assert
      .isNull(currentCycle, "Current Cycle is open, please close if before making schema changes");

    Level level = findOne(levelId);
    if (level == null) {
      throw new RuntimeException("Level " + levelId + " is not available");
    }
    level.setClosed(true);
    save(level);
  }

  @Override
  public Level save(Level level) {
    ApplicationConfiguration currentCycle = appConfigurationService
      .findByKey(ApplicationConfigurationKeys.CURRENT_CYCLE_ID.name());
    Assert
      .isNull(currentCycle, "Current Cycle is open, please close if before making schema changes");
    return super.save(level);
  }

  @Transactional
  public void updateParent(long levelId, long newParentId) {
    ApplicationConfiguration currentCycle = appConfigurationService
      .findByKey(ApplicationConfigurationKeys.CURRENT_CYCLE_ID.name());
    Assert
      .isNull(currentCycle, "Current Cycle is open, please close if before making schema changes");

    Level one = findOne(levelId);
    if (one == null) {
      throw new RuntimeException("Level " + levelId + " is not available, katlo");
    }
    Level oneCopy = new Level(one);
    one.setClosed(true);
    oneCopy.setLevelParentId(newParentId);
    oneCopy.setClosed(false);

    save(one);
    oneCopy = save(oneCopy);

    for (Level children : findByLevelParentId(levelId)) {
      updateParent(children.getId(), oneCopy.getId());
    }
  }
}

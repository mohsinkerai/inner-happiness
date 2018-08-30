package com.inner.satisfaction.backend.level;

import com.inner.satisfaction.backend.base.BaseService;
import java.util.Set;
import javax.transaction.Transactional;
import org.springframework.stereotype.Service;

@Service
public class LevelService extends BaseService<Level> {

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

  public void close(long levelId) {
    Level level = findOne(levelId);
    if (level == null) {
      throw new RuntimeException("Level " + levelId + " is not available");
    }
    level.setClosed(true);
    save(level);
  }

  @Transactional
  public void updateParent(long levelId, long newParentId) {
    Level one = findOne(levelId);
    if(one == null) {
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

package com.inner.satisfaction.backend.level;

import com.inner.satisfaction.backend.base.BaseRepository;
import com.inner.satisfaction.backend.base.BaseService;
import org.springframework.stereotype.Service;

@Service
public class LevelService extends BaseService<Level>{

  protected LevelService(
      LevelRepository baseRepository) {
    super(baseRepository);
  }
}

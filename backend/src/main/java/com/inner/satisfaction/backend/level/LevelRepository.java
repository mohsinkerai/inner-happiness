package com.inner.satisfaction.backend.level;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import java.util.Set;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
public interface LevelRepository extends BaseRepository<Level> {

  Set<Level> findByLevelParentId(Long levelParentId);

  Set<Level> findByLevelTypeId(long levelTypeId);
}

package com.inner.satisfaction.backend.institution;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.stereotype.Repository;

@Repository
public interface InstitutionRepository extends BaseRepository<Institution> {

  List<Institution> findByLevelId(long levelId);

  List<Institution> findByCategory(String category);
}

package com.inner.satisfaction.backend.institution;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
public interface InstitutionRepository extends BaseRepository<Institution> {

  List<Institution> findByLevelId(long levelId);

  List<Institution> findByCategory(String category);

  @Query(value = "SELECT i.* from institution i inner join level l on l.id = i.level_id where i.category = :category and l.level_parent_id  = :parentLevelId", nativeQuery = true)
  List<Institution> findByCategoryAndParentLevelId(String category, Long parentLevelId);

  @Query(value = "SELECT i.* from institution i inner join level l on l.id = i.level_id where i.category = :category and l.level_type_id  = :levelTypeId", nativeQuery = true)
  List<Institution> findByCategoryAndLevelTypeId(String category, Long levelTypeId);
}

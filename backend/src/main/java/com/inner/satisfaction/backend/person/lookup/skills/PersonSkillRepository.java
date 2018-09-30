package com.inner.satisfaction.backend.person.lookup.skills;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
public interface PersonSkillRepository extends BaseRepository<PersonSkill> {

  @Modifying
  @Query("DELETE FROM PersonSkill ps where ps.personId = :personId")
  void removeByPersonId(long personId);

  List<PersonSkill> findByPersonId(long personId);
}

package com.inner.satisfaction.backend.person.lookup.fieldofexpertise;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
public interface PersonFieldOfExpertiseRepository extends BaseRepository<PersonFieldOfExpertise> {

  @Modifying
  @Query("DELETE FROM PersonFieldOfExpertise ps where ps.personId = :personId")
  void removeByPersonId(long personId);

  List<PersonFieldOfExpertise> findByPersonId(long personId);
}

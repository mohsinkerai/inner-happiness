package com.inner.satisfaction.backend.person.relation;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
public interface PersonRelationPersonRepository extends BaseRepository<PersonRelationPerson> {

  List<PersonRelationPerson> findByFirstPersonId(long firstPersonId);

  PersonRelationPerson findByFirstPersonIdAndSecondPersonIdAndRelation(long firstPersonId,
    long secondPersonId, long relation);

  List<PersonRelationPerson> findByFirstPersonIdAndSecondPersonId(long firstPersonId,
    long secondPersonId);

  @Modifying
  @Query("DELETE FROM PersonRelationPerson prp where prp.firstPersonId = :personId or prp.secondPersonId = :personId")
  void removeByPersonId(long personId);
}

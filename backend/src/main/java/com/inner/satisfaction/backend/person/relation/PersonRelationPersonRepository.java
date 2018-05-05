package com.inner.satisfaction.backend.person.relation;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.stereotype.Repository;

@Repository
public interface PersonRelationPersonRepository extends BaseRepository<PersonRelationPerson> {

  List<PersonRelationPerson> findByFirstPersonId(long firstPersonId);
}

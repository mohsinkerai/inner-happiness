package com.inner.satisfaction.backend.person;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
public interface PersonRepository extends BaseRepository<Person> {

  Person findByCnic(String cnic);

  // TODO: Full Text search index on CNIC, FirstName and LastName
  List<Person> findByCnicIgnoreCaseContainingOrFirstNameIgnoreCaseContainingOrFamilyNameIgnoreCaseContaining(String cnic, String firstName, String familyName);
}

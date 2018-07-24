package com.inner.satisfaction.backend.person;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Repository;

@Repository
public interface PersonRepository extends BaseRepository<Person> {

  Person findByCnic(String cnic);

  // TODO: Full Text search index on CNIC, FirstName and LastName
  Page<Person> findByCnicIgnoreCaseContainingOrFirstNameIgnoreCaseContainingOrFamilyNameIgnoreCaseContaining(
    String cnic, String firstName, String familyName, Pageable pageable);
}

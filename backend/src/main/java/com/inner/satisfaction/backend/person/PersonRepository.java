package com.inner.satisfaction.backend.person;

import com.inner.satisfaction.backend.base.BasePagingAndSortingRepository;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Repository;

@Repository
public interface PersonRepository extends BasePagingAndSortingRepository<Person> {

  Person findByCnic(String cnic);

  // TODO: Full Text search index on CNIC, FirstName and LastName
  Page<Person> findByCnicIgnoreCaseContainingOrFirstNameIgnoreCaseContainingOrFamilyNameIgnoreCaseContaining(
    String cnic, String firstName, String familyName, Pageable pageable);
}

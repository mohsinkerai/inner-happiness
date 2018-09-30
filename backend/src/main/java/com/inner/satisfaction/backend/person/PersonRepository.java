package com.inner.satisfaction.backend.person;

import com.inner.satisfaction.backend.base.BasePagingAndSortingRepository;
import java.util.List;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Repository;

@Repository
public interface PersonRepository extends BasePagingAndSortingRepository<Person> {

  Person findByCnic(String cnic);

  // TODO: Full Text search index on CNIC, FirstName and LastName
  Page<Person> findByCnicIgnoreCaseContainingAndFirstNameIgnoreCaseContainingAndFamilyNameIgnoreCaseContainingOrIdEquals(
    String cnic, String firstName, String familyName, Long id, Pageable pageable);

  List<Person> findByIdOrCnicIgnoreCaseContaining(Long id, String cnic);

  Page<Person> findByCnicIgnoreCaseContainingOrFirstNameIgnoreCaseContainingOrFamilyNameIgnoreCaseContainingOrIdEqualsOrJamatiTitleEqualsOrGenDegreeContainingOrGenInstitutionContainingOrGenMajorAreaOfStudyContaining(
    String cnic, String firstName, String lastName, Long id, Long jamatiTitle, String genDegree,
    String genInstitution, String genMajorAreaOfStudy, Pageable pageable);
}

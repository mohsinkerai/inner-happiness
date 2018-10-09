package com.inner.satisfaction.backend.person;

import com.inner.satisfaction.backend.base.BasePagingAndSortingRepository;
import java.util.List;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
public interface PersonRepository extends BasePagingAndSortingRepository<Person> {

  String searchQuery = "SELECT p.* FROM person p "
    + " WHERE (:name is null OR :name = '' OR MATCH(p.full_name) AGAINST (CONCAT(\"'\", :name, \"'\" ) IN BOOLEAN MODE))"
    + " AND (:cnic is null OR :cnic = '' OR p.cnic = :cnic)"
    + " AND (:id is null OR :id = 0 OR p.id = :id)"
    + " AND (:jt is null OR :jt = 0 OR p.jamati_title = :jt)"
    + " AND (:eduInstitution is null OR :eduInstitution = 0 OR MATCH(p.gen_institution) AGAINST (CONCAT(\"'\", '+', :eduInstitution, \"'\" ) IN BOOLEAN MODE))"
    + " AND (:eduDegree is null OR :eduDegree = 0 OR MATCH(p.gen_degree) AGAINST (CONCAT(\"'\", '+', :eduDegree, \"'\" ) IN BOOLEAN MODE))"
    + " AND (:aos is null OR :aos = 0 OR MATCH(p.gen_major_area_of_study) AGAINST (CONCAT(\"'\", '+', :aos, \"'\" ) IN BOOLEAN MODE))";
//    + " AND (:eduInstitution is null OR :eduInstitution = 0 OR MATCH(p.gen_institution) AGAINST (CONCAT('\\\'', '+',:eduInstitution,'\\\'') IN BOOLEAN MODE))";

  Person findByCnic(String cnic);

  // TODO: Full Text search index on CNIC, FirstName and LastName
  Page<Person> findByCnicIgnoreCaseContainingAndFirstNameIgnoreCaseContainingAndFamilyNameIgnoreCaseContainingOrIdEquals(
    String cnic, String firstName, String familyName, Long id, Pageable pageable);

  List<Person> findByIdOrCnicIgnoreCaseContaining(Long id, String cnic);

  //  @Query(value = "SELECT p.* FROM person p WHERE MATCH(first_name) AGAINST (\":firstName\" IN BOOLEAN MODE)", nativeQuery = true)
  Page<Person> findByCnicIgnoreCaseContainingOrFirstNameIgnoreCaseContainingOrFamilyNameIgnoreCaseContainingOrIdEqualsOrJamatiTitleEqualsOrGenDegreeContainingOrGenInstitutionContainingOrGenMajorAreaOfStudyContaining(
    String cnic, String firstName, String lastName, Long id, Long jamatiTitle, String genDegree,
    String genInstitution, String genMajorAreaOfStudy, Pageable pageable);

  // Only Educational Institution
  @Query(value = PersonRepository.searchQuery, nativeQuery = true)
  Page<Person> findByFullNameAndIdAndCnicAndEducationInstitutionAndEducationDegreeAndAreaOfStudyAndJamatiTitle(String name, Long id, Long eduInstitution, Long eduDegree, Long aos, Long jt, String cnic, Pageable pageable);
}

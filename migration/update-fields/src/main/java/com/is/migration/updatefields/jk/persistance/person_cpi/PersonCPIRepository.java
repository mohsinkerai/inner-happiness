package com.is.migration.updatefields.jk.persistance.person_cpi;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface PersonCPIRepository extends JpaRepository<PersonCPI, Long> {

  List<PersonCPI> findByPersonIdEqualsAndAndCpiIdEquals(Long personId, Long cpiId);
}

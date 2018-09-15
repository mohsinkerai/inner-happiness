package com.is.migration.updatefields.jk.persistance.person;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface PersonRepository extends JpaRepository<Person, Long> {

  List<Person> findByOldId(Long oldId);
}

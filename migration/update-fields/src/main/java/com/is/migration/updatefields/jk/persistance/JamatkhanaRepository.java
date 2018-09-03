package com.is.migration.updatefields.jk.persistance;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface JamatkhanaRepository extends JpaRepository<JamatkhanaEntity, Long> {

  List<JamatkhanaEntity> findByOldCodeEqualsAndIsClosedFalse(String oldCode);
}

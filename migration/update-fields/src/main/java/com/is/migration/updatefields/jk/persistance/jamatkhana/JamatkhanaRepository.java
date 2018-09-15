package com.is.migration.updatefields.jk.persistance.jamatkhana;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface JamatkhanaRepository extends JpaRepository<JamatkhanaEntity, Long> {

  List<JamatkhanaEntity> findByOldCodeEqualsAndIsClosedFalse(String oldCode);

  List<JamatkhanaEntity> findByOldId(Long oldId);
}

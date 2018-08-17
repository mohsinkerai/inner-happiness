package com.inner.satisfaction.backend.position.active;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.stereotype.Repository;

@Repository
public interface PositionOnInstitutionRepository extends BaseRepository<PositionOnInstitution> {

  List<PositionOnInstitution> findByInstitutionId(long institutionId);
}

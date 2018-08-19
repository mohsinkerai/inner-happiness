package com.inner.satisfaction.backend.person.cpi;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.stereotype.Repository;

@Repository
public interface PersonCPIRepository extends BaseRepository<PersonCPI> {

  List<PersonCPI> findByCpiId(long cpiId);

  PersonCPI findByCpiIdAndIsAppointedTrue(long cpiId);
}

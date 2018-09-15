package com.inner.satisfaction.backend.person.lookup.professionalmembership;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
public interface PersonProfessionalMembershipRepository extends BaseRepository<PersonProfessionalMembership> {

  @Modifying
  @Query("DELETE FROM PersonProfessionalMembership ps where ps.personId = :personId")
  void removeByPersonId(long personId);

  List<PersonProfessionalMembership> findByPersonId(long personId);
}

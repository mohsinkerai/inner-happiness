package com.inner.satisfaction.backend.person.relation;

import com.inner.satisfaction.backend.base.BaseService;
import java.util.List;
import org.springframework.stereotype.Service;

@Service
public class PersonRelationPersonService extends BaseService<PersonRelationPerson> {

  private final PersonRelationPersonRepository personRelationPersonRepository;

  protected PersonRelationPersonService(
    PersonRelationPersonRepository personRelationPersonRepository,
    PersonRelationPersonValidation personRelationPersonValidation) {
    super(personRelationPersonRepository, personRelationPersonValidation);
    this.personRelationPersonRepository = personRelationPersonRepository;
  }

  public List<PersonRelationPerson> findByFirstPersonId(long id) {
    return personRelationPersonRepository.findByFirstPersonId(id);
  }

  public PersonRelationPerson findByPersonPersonRelation(long personOneId, long personTwoId,
    long relationId) {
    return personRelationPersonRepository
      .findByFirstPersonIdAndSecondPersonIdAndRelation(personOneId, personTwoId, relationId);
  }

  public void remove(long personOneId, long personTwoId) {
    personRelationPersonRepository
      .findByFirstPersonIdAndSecondPersonId(personOneId, personTwoId).stream()
      .forEach(personRelationPersonRepository::delete);
  }
}

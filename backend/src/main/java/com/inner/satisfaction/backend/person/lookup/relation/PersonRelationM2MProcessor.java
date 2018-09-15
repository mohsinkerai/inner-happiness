package com.inner.satisfaction.backend.person.lookup.relation;

import com.google.common.collect.Lists;
import com.inner.satisfaction.backend.lookups.relation.Relation;
import com.inner.satisfaction.backend.lookups.relation.RelationService;
import com.inner.satisfaction.backend.person.Person;
import com.inner.satisfaction.backend.person.PersonService;
import com.inner.satisfaction.backend.person.lookup.base.BaseM2MProcessingService;
import com.inner.satisfaction.backend.person.lookup.dto.ReducedPersonDto;
import com.inner.satisfaction.backend.utils.DtoEntityConverter;
import java.util.List;
import java.util.Optional;
import javax.transaction.Transactional;
import org.springframework.stereotype.Component;

@Component
public class PersonRelationM2MProcessor extends
  BaseM2MProcessingService<PersonRelationPerson, ReducedPersonDto> {

  private final RelationService relationService;
  private final PersonService personService;
  private final PersonRelationPersonService prpService;
  private final DtoEntityConverter<ReducedPersonDto, Person> entityConverter;

  public PersonRelationM2MProcessor(
    RelationService relationService,
    PersonService personService,
    PersonRelationPersonService prpService,
    DtoEntityConverter<ReducedPersonDto, Person> entityConverter) {
    this.relationService = relationService;
    this.personService = personService;
    this.prpService = prpService;
    this.entityConverter = entityConverter;
  }

  @Override
  protected List<PersonRelationPerson> findPersonEntities(long personId) {
    return prpService.findByFirstPersonId(personId);
  }

  @Override
  protected Person populateEntityInPerson(Person person, List<PersonRelationPerson> e) {
    List<ReducedPersonDto> reducedPersonDtos = Lists.newArrayList();
    for (PersonRelationPerson prp : e) {
      long secondPersonId = prp.getSecondPersonId();
      Person secondPerson = personService.findOne(secondPersonId);
      ReducedPersonDto dto = entityConverter.convertTo(secondPerson);
      dto.setRelation(prp.getRelation());
      reducedPersonDtos.add(dto);
    }
    person.setFamilyRelations(reducedPersonDtos);
    return person;
  }

  @Override
  protected long getEntityId(ReducedPersonDto dto) {
    Optional<Long> id = Optional.ofNullable(dto.getId());
    if (!id.isPresent()) {
      String cnic = dto.getCnic();
      Optional<Person> byCnic = Optional.ofNullable(personService.findByCnic(cnic));
      if (!byCnic.isPresent()) {
        Person person = entityConverter.convertFrom(dto);
        person = personService.save(person);
        id = Optional.of(person.getId());
      } else {
        id = Optional.of(byCnic.get().getId());
      }
    }
    return id.get();
  }

  @Override
  protected ReducedPersonDto setPersonId(ReducedPersonDto dto, long id) {
    dto.setPersonId(id);
    return dto;
  }

  @Override
  protected ReducedPersonDto setEntityId(ReducedPersonDto dto, long id) {
    dto.setId(id);
    return dto;
  }

  @Override
  protected PersonRelationPerson convert(ReducedPersonDto dto) {
    return PersonRelationPerson.builder()
      .firstPersonId(dto.personId)
      .secondPersonId(dto.getId())
      .relation(dto.getRelation())
      .build();
  }

  @Override
  protected List<ReducedPersonDto> convert(Person person) {
    return Optional.ofNullable(person.getFamilyRelations()).orElse(Lists.newArrayList());
  }

  @Override
  protected void removeAllEntityByPersonId(long personId) {
    prpService.removeAllRelationForPerson(personId);
  }

  @Override
  @Transactional
  protected PersonRelationPerson saveEntity(PersonRelationPerson personRelationPerson) {
    long relationId = personRelationPerson.getRelation();
    Relation relation = relationService.findOne(relationId);
    if (relation == null) {
      throw new RuntimeException("Invalid Relation");
    }
    Long reverseRelationId = relation.getReverseRelationId();

    PersonRelationPerson prp = PersonRelationPerson.builder()
      .firstPersonId(personRelationPerson.getSecondPersonId())
      .relation(reverseRelationId)
      .secondPersonId(personRelationPerson.getFirstPersonId())
      .build();

    prpService.save(personRelationPerson);
    prpService.save(prp);
    return personRelationPerson;
  }
}

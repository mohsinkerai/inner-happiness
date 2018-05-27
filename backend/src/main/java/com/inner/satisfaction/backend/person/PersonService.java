package com.inner.satisfaction.backend.person;

import com.google.common.collect.Lists;
import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.person.dto.ReducedPersonDto;
import com.inner.satisfaction.backend.person.relation.PersonRelationPerson;
import com.inner.satisfaction.backend.person.relation.PersonRelationPersonService;
import com.inner.satisfaction.backend.utils.DtoEntityConverter;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.Stream;
import javax.transaction.Transactional;
import org.springframework.stereotype.Service;

@Service
public class PersonService extends BaseService<Person> {

  private final DtoEntityConverter<ReducedPersonDto, Person> dtoConverter;
  private final PersonRelationPersonService personRelationPersonService;
  private final PersonRepository personRepository;

  protected PersonService(
    PersonRepository personRepository,
    PersonValidation personValidation,
    DtoEntityConverter<ReducedPersonDto, Person> dtoConverter,
    PersonRelationPersonService personRelationPersonService) {
    super(personRepository, personValidation);
    this.personRepository = personRepository;
    this.dtoConverter = dtoConverter;
    this.personRelationPersonService = personRelationPersonService;
  }

  public Person findByCnic(String cnic) {
    return personRepository.findByCnic(cnic);
  }

  public List<Person> findByCnicOrFirstNameOrLastName(String cnic, String firstName,
    String lastName) {
    return personRepository
      .findByCnicIgnoreCaseContainingOrFirstNameIgnoreCaseContainingOrFamilyNameIgnoreCaseContaining(
        cnic, firstName, lastName);
  }

  /**
   * 1. Convert all new to persons
   * 2. Create relations (don't save)
   * 3. Find All Relations of personIdOne.
   * 4. Remove Extra Relations (that've ended) and Create New where needed.
   */
  @Override
  @Transactional
  public Person save(Person person) {
    List<ReducedPersonDto> familyRelations = person.getFamilyRelations();
    person = super.save(person);

    List<ReducedPersonDto> reducedPersonsWithId = familyRelations
      .stream()
      .map(this::attachPersonId)
      .collect(Collectors.toList());

    List<ReducedPersonDto> personRelations = findAllRelations(person.getId());
    managePRP(reducedPersonsWithId, personRelations, person.getId());

    person.setFamilyRelations(familyRelations);
    return person;
  }

  private void managePRP(List<ReducedPersonDto> reducedPersonsWithId,
    List<ReducedPersonDto> personRelations, long personId) {
    for(ReducedPersonDto reducedPersonDto : personRelations){
      personRelationPersonService.remove(personId, reducedPersonDto.getId());
    }
    for(ReducedPersonDto reducedPersonDto : reducedPersonsWithId){
      personRelationPersonService.save(
        PersonRelationPerson.builder()
        .firstPersonId(personId)
        .secondPersonId(reducedPersonDto.getId())
        .relation(reducedPersonDto.getRelation())
        .build()
      );
    }
  }

  @Override
  public Person findOne(Long id) {
    Person one = super.findOne(id);
    if (one == null) {
      return null;
    }
    List<ReducedPersonDto> reducedPersons = findAllRelations(id);
    one.setFamilyRelations(reducedPersons);
    return one;
  }

  private List<ReducedPersonDto> findAllRelations(long id) {
    return personRelationPersonService
      .findByFirstPersonId(id)
      .stream()
      .map(this::findOneReducedPerson)
      .collect(Collectors.toList());
  }

  private ReducedPersonDto findOneReducedPerson(PersonRelationPerson prp) {
    Person second = super.findOne(prp.getSecondPersonId());
    ReducedPersonDto reducedPersonDto = dtoConverter.convertTo(second);
    reducedPersonDto.setRelation(prp.getRelation());
    return reducedPersonDto;
  }

  private ReducedPersonDto attachPersonId(ReducedPersonDto reducedPersonDto) {
    Person person = findByCnic(reducedPersonDto.getCnic());
    if(person != null) {
      reducedPersonDto.setId(person.getId());
      return reducedPersonDto;
    }
    person = dtoConverter.convertFrom(reducedPersonDto);
    person = personRepository.save(person);
    reducedPersonDto.setId(person.getId());
    return reducedPersonDto;
  }
}

package com.inner.satisfaction.backend.person;

import com.google.common.collect.ImmutableList;
import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.person.dto.ReducedPersonDto;
import com.inner.satisfaction.backend.person.relation.PersonRelationPerson;
import com.inner.satisfaction.backend.person.relation.PersonRelationPersonService;
import com.inner.satisfaction.backend.utils.DtoEntityConverter;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import javax.transaction.Transactional;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

@Service
public class PersonService extends BaseService<Person> {

  private final DtoEntityConverter<ReducedPersonDto, Person> dtoConverter;
  private final PersonRelationPersonService personRelationPersonService;
  private final PersonRepository personRepository;
  private final String path;

  protected PersonService(
    PersonRepository personRepository,
    PersonValidation personValidation,
    DtoEntityConverter<ReducedPersonDto, Person> dtoConverter,
    PersonRelationPersonService personRelationPersonService,
    @Value("${person.images.path}")
      String path) {
    super(personRepository, personValidation);
    this.personRepository = personRepository;
    this.dtoConverter = dtoConverter;
    this.personRelationPersonService = personRelationPersonService;
    this.path = path;
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

  @Override
  @Transactional
  public Person save(Person person) {
    List<ReducedPersonDto> reducedPersons = Optional.ofNullable(person.getFamilyRelations())
      .orElse(ImmutableList.of());
    Person savedPerson = super.save(person);
    List<ReducedPersonDto> updatedReducedPersons = reducedPersons.stream()
      .map(rp -> updateReducedPerson(rp, savedPerson.getId())).collect(
        Collectors.toList());
    savedPerson.setFamilyRelations(updatedReducedPersons);
    return savedPerson;
  }

  @Override
  public Person findOne(Long id) {
    Person one = super.findOne(id);
    if (one == null) {
      return null;
    }
    List<ReducedPersonDto> reducedPersons = personRelationPersonService
      .findByFirstPersonId(id)
      .stream()
      .map(prp -> findOneReducedPerson(prp, id))
      .collect(Collectors.toList());
    one.setFamilyRelations(reducedPersons);
    return one;
  }

  private ReducedPersonDto findOneReducedPerson(PersonRelationPerson prp, Long id) {
    Person second = super.findOne(prp.getSecondPersonId());
    ReducedPersonDto reducedPersonDto = dtoConverter.convertTo(second);
    reducedPersonDto.setRelation(prp.getRelation());
    return reducedPersonDto;
  }

  private ReducedPersonDto updateReducedPerson(ReducedPersonDto reducedPerson, long savedPersonId) {
    if (reducedPerson.getId() == null) {
      Person relativePerson = dtoConverter.convertFrom(reducedPerson);
      ReducedPersonDto savedReducedPerson = dtoConverter
        .convertTo(personRepository.save(relativePerson));
      savedReducedPerson.setRelation(reducedPerson.getRelation());
      reducedPerson = savedReducedPerson;
    }

    PersonRelationPerson prp = PersonRelationPerson.builder()
      .firstPersonId(savedPersonId)
      .secondPersonId(reducedPerson.getId())
      .relation(reducedPerson.getRelation())
      .build();
    personRelationPersonService.save(prp);
    return reducedPerson;
  }
}

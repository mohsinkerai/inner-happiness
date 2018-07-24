package com.inner.satisfaction.backend.person;

import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.person.base.BaseM2MProcessingService;
import com.inner.satisfaction.backend.person.dto.ReducedPersonDto;
import com.inner.satisfaction.backend.person.relation.PersonRelationPerson;
import com.inner.satisfaction.backend.person.relation.PersonRelationPersonService;
import com.inner.satisfaction.backend.utils.DtoEntityConverter;
import java.util.List;
import java.util.stream.Collectors;
import javax.persistence.EntityNotFoundException;
import javax.transaction.Transactional;
import lombok.extern.slf4j.Slf4j;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Service;

@Slf4j
@Service
public class PersonService extends BaseService<Person> {

  private final DtoEntityConverter<ReducedPersonDto, Person> dtoConverter;
  private final PersonRelationPersonService personRelationPersonService;
  private final PersonRepository personRepository;
  private final List<BaseM2MProcessingService> baseProcessingservices;

  protected PersonService(
    PersonRepository personRepository,
    PersonValidation personValidation,
    DtoEntityConverter<ReducedPersonDto, Person> dtoConverter,
    PersonRelationPersonService personRelationPersonService,
    List<BaseM2MProcessingService> baseProcessingservices) {
    super(personRepository, personValidation);
    this.personRepository = personRepository;
    this.dtoConverter = dtoConverter;
    this.personRelationPersonService = personRelationPersonService;
    this.baseProcessingservices = baseProcessingservices;
  }

  public Person findByCnic(String cnic) {
    return personRepository.findByCnic(cnic);
  }

  public Page<Person> findByCnicOrFirstNameOrLastName(String cnic, String firstName,
    String lastName, Pageable pageable) {
    return personRepository
      .findByCnicIgnoreCaseContainingOrFirstNameIgnoreCaseContainingOrFamilyNameIgnoreCaseContaining(
        cnic, firstName, lastName, pageable);
  }

  /**
   * 1. Convert all new to persons 2. Create relations (don't save) 3. Find All Relations of
   * personIdOne. 4. Remove Extra Relations (that've ended) and Create New where needed.
   */
  @Override
  @Transactional
  public Person save(Person person) {

    List<ReducedPersonDto> familyRelations = person.getFamilyRelations();
    person = super.save(person);
    final Person savedPerson = person;

    for (BaseM2MProcessingService bps : baseProcessingservices) {
      bps.processList(person, person.getId());
    }

//    baseProcessingservices.stream()
//      .forEach(bps -> bps.processList(savedPerson, savedPerson.getId()));

    if (familyRelations != null) {
      List<ReducedPersonDto> reducedPersonsWithId = familyRelations
        .stream()
        .filter(reducedPersonDto -> reducedPersonDto != null)
        .map(this::attachPersonId)
        .collect(Collectors.toList());

      List<ReducedPersonDto> personRelations = findAllRelations(person.getId());
      managePRP(reducedPersonsWithId, personRelations, person.getId());
      person.setFamilyRelations(familyRelations);
    } else {
      deleteAllRelation(person.getId());
    }
    return person;
  }

  private void deleteAllRelation(Long personId) {
    List<PersonRelationPerson> prp = personRelationPersonService
      .findByFirstPersonId(personId);
    prp.stream()
      .forEach(personRelationPerson -> personRelationPersonService.delete(personRelationPerson));
  }

  private void managePRP(List<ReducedPersonDto> reducedPersonsWithId,
    List<ReducedPersonDto> personRelations, long personId) {
    for (ReducedPersonDto reducedPersonDto : personRelations) {
      personRelationPersonService.remove(personId, reducedPersonDto.getId());
    }
    for (ReducedPersonDto reducedPersonDto : reducedPersonsWithId) {
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

    Person person = one;
    for (BaseM2MProcessingService bps : baseProcessingservices) {
      person = bps.populatePerson(person);
    }

    return person;
  }

  private List<ReducedPersonDto> findAllRelations(long id) {
    return personRelationPersonService
      .findByFirstPersonId(id)
      .stream()
      .map(this::findOneReducedPerson)
      .filter(reducedPersonDto -> reducedPersonDto != null)
      .collect(Collectors.toList());
  }

  private ReducedPersonDto findOneReducedPerson(PersonRelationPerson prp) {
    try {
      Person second = super.findOne(prp.getSecondPersonId());
      if (second != null) {
        ReducedPersonDto reducedPersonDto = dtoConverter.convertTo(second);
        reducedPersonDto.setRelation(prp.getRelation());
        return reducedPersonDto;
      }
    } catch (EntityNotFoundException ex) {
      log.info("Exception aya", ex);
      return null;
    }
    return null;
  }

  private ReducedPersonDto attachPersonId(ReducedPersonDto reducedPersonDto) {
    Person person = findByCnic(reducedPersonDto.getCnic());
    if (person != null) {
      reducedPersonDto.setId(person.getId());
      return reducedPersonDto;
    }
    person = dtoConverter.convertFrom(reducedPersonDto);
    person = personRepository.save(person);
    reducedPersonDto.setId(person.getId());
    return reducedPersonDto;
  }
}

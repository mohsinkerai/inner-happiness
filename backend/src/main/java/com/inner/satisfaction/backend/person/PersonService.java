package com.inner.satisfaction.backend.person;

import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.person.lookup.base.BaseM2MProcessingService;
import java.util.List;
import java.util.Optional;
import java.util.Set;
import javax.transaction.Transactional;
import lombok.extern.slf4j.Slf4j;
import org.apache.commons.lang3.RandomStringUtils;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Service;
import org.springframework.util.Assert;
import org.springframework.util.StringUtils;

@Slf4j
@Service
public class PersonService extends BaseService<Person> {

  private final PersonRepository personRepository;
  private final Set<BaseM2MProcessingService> baseProcessingservices;

  protected PersonService(
    PersonRepository personRepository,
    PersonValidation personValidation,
    Set<BaseM2MProcessingService> baseProcessingservices) {
    super(personRepository, personValidation);
    this.personRepository = personRepository;
    this.baseProcessingservices = baseProcessingservices;
  }


  public Person findByCnic(String cnic) {
    return personRepository.findByCnic(cnic);
  }

  public Person findByFormNo(String formNo) {
    return findOne(Long.valueOf(formNo));
  }

  public Page<Person> findByCnicAndFirstNameAndLastNameAndFormNo(
    String cnic,
    String firstName,
    String lastName,
    String formNo,
    Pageable pageable) {
    return personRepository
      .findByCnicIgnoreCaseContainingAndFirstNameIgnoreCaseContainingAndFamilyNameIgnoreCaseContainingOrIdEquals(
        cnic, firstName, lastName, Long.valueOf(formNo), pageable);
  }

  public Page<Person> findByFistNameOrLastNameOrFormNoOrCnicOrJamatiTitleOrDegreeOrMajorAreaOfStudyOrAcademicInstitution(
    String cnic,
    String firstName,
    String lastName,
    Long formNo,
    Long degree,
    Long majorAreaOfStudy,
    Long academicInstitution,
    Long jamatiTitle,
    Pageable pageable
  ) {
    String areaOfStudyString = String.valueOf(majorAreaOfStudy);
    String academicInstitutionString = String.valueOf(academicInstitution);
    String degreeString = String.valueOf(degree);

    if (StringUtils.isEmpty(firstName)) {
      firstName = RandomStringUtils.random(5);
    }
    if (StringUtils.isEmpty(lastName)) {
      lastName = RandomStringUtils.random(5);
    }
    if (StringUtils.isEmpty(cnic)) {
      cnic = RandomStringUtils.random(5);
    }
    if (StringUtils.isEmpty(cnic)) {
      cnic = RandomStringUtils.random(5);
    }
    if (areaOfStudyString.equals("0")) {
      areaOfStudyString = RandomStringUtils.random(5);
    }
    if (academicInstitutionString.equals("0")) {
      academicInstitutionString = RandomStringUtils.random(5);
    }
    if (degreeString.equals("0")) {
      degreeString = RandomStringUtils.random(5);
    }
    return personRepository
      .findByCnicIgnoreCaseContainingOrFirstNameIgnoreCaseContainingOrFamilyNameIgnoreCaseContainingOrIdEqualsOrJamatiTitleEqualsOrGenDegreeContainingOrGenInstitutionContainingOrGenMajorAreaOfStudyContaining(
        cnic, firstName, lastName, formNo, jamatiTitle, degreeString,
        academicInstitutionString, areaOfStudyString, pageable);
  }

  public List<Person> findByIdOrCnic(
    String cnic,
    Long id) {
    return personRepository
      .findByIdOrCnicIgnoreCaseContaining(id, cnic);
  }

  /**
   * 1. Convert all new to persons 2. Create relations (don't save) 3. Find All Relations of
   * personIdOne. 4. Remove Extra Relations (that've ended) and Create New where needed.
   */
  @Override
  @Transactional
  public Person save(Person person) {
    validate(person);
    Person personCopy = super.save(person);
    person.setId(personCopy.getId());

    for (BaseM2MProcessingService bps : baseProcessingservices) {
      bps.processList(person, person.getId());
    }

    return person;
  }

  public Person findOneWithDetails(Long id) {
    Optional<Person> one = Optional.ofNullable(findOne(id));
    if (!one.isPresent()) {
      return null;
    } else {
      Person person = one.get();
      for (BaseM2MProcessingService bps : baseProcessingservices) {
        person = bps.populatePerson(person);
      }
      return person;
    }
  }

  public Page<Person> findAll(Pageable pageable) {
    return personRepository.findAll(pageable);
  }

  public void validate(Person entity) {
    Assert.notNull(entity.getJamatkhana(), "Person jamatkhana should not be null");
    Assert.notNull(entity.getLocalCouncil(), "Person local council should not be null");
    Assert.notNull(entity.getRegionalCouncil(), "Person regional council should not be null");
  }
}

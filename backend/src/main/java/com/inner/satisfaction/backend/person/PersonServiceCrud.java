package com.inner.satisfaction.backend.person;

import com.inner.satisfaction.backend.base.BaseService;
import lombok.extern.slf4j.Slf4j;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Service;

/**
 * Copy of Person Service for Crud Operations Only. It is done just to resolve circular dependency.
 */
@Slf4j
@Service
public class PersonServiceCrud extends BaseService<Person> {

  private final PersonRepository personRepository;

  protected PersonServiceCrud(
    PersonRepository personRepository,
    PersonValidation personValidation) {
    super(personRepository, personValidation);
    this.personRepository = personRepository;
  }

  public Person findByCnic(String cnic) {
    return personRepository.findByCnic(cnic);
  }

  public Page<Person> findAll(Pageable pageable) {
    return personRepository.findAll(pageable);
  }
}

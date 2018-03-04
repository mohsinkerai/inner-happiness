package com.inner.satisfaction.backend.person;

import com.inner.satisfaction.backend.base.BaseService;
import org.springframework.stereotype.Service;

@Service
public class PersonService extends BaseService<Person>{

  private final PersonRepository personRepository;

  protected PersonService(
      PersonRepository personRepository,
      PersonValidation personValidation) {
    super(personRepository, personValidation);
    this.personRepository = personRepository;
  }

  public Person findByCnic(String cnic) {
    return personRepository.findByCnic(cnic);
  }
}

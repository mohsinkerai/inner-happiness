package com.inner.satisfaction.backend.person;

import com.inner.satisfaction.backend.base.BaseService;
import org.springframework.stereotype.Service;

@Service
public class PersonService extends BaseService<Person>{

  protected PersonService(
      PersonRepository baseRepository,
      PersonValidation personValidation) {
    super(baseRepository, personValidation);
  }
}

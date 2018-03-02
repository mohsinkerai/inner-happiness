package com.inner.satisfaction.backend;

import com.inner.satisfaction.backend.person.Person;
import com.inner.satisfaction.backend.person.PersonRepository;
import java.util.List;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class PappuController {

  @Autowired
  PersonRepository personRepository;

  @RequestMapping(value = "/pappu", produces = MediaType.APPLICATION_JSON_UTF8_VALUE)
  public List<Person> pappu() {
    return personRepository.findAll();
  }
}

package com.inner.satisfaction.backend.person;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + PersonController.PATH)
public class PersonController extends BaseController<Person> {

  public static final String PATH = "person";

  public PersonController(PersonService personService) {
    super(personService);
  }
}

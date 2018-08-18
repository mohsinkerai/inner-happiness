package com.inner.satisfaction.backend.person.cpi;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + PersonCPIController.PATH)
public class PersonCPIController extends BaseController<PersonCPI> {

  public static final String PATH = "person/cpi";

  public PersonCPIController(PersonCPIService personCPIService) {
    super(personCPIService);
  }
}

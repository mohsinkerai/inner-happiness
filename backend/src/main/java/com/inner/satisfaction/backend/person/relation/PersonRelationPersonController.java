package com.inner.satisfaction.backend.person.relation;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
@RequestMapping(PREFIX + PersonRelationPersonController.PATH)
public class PersonRelationPersonController extends BaseController<PersonRelationPerson> {

  public static final String PATH = "person/relation";

  public PersonRelationPersonController(PersonRelationPersonService personRelationPersonService) {
    super(personRelationPersonService);
  }
}

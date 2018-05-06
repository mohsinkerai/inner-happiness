package com.inner.satisfaction.backend.person;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import java.util.List;
import java.util.Optional;
import javax.persistence.EntityNotFoundException;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + PersonController.PATH)
public class PersonController extends BaseController<Person> {

  public static final String PATH = "person";
  private final PersonService personService;

  public PersonController(PersonService personService) {
    super(personService);
    this.personService = personService;
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/cnic", method = RequestMethod.GET)
  public Person findByCnic(@RequestParam("cnic") String cnic) {
    return Optional.ofNullable(personService.findByCnic(cnic))
      .orElseThrow(() -> new EntityNotFoundException("Person with Cnic " + cnic + " Not found"));
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findByCnicOrFirstNameOrLastName", method = RequestMethod.GET)
  public List<Person> findByCnicOrFirstNameOrLastName(
    @RequestParam("cnic") String cnic,
    @RequestParam("firstName") String firstName,
    @RequestParam("lastName") String lastName) {
    return personService.findByCnicOrFirstNameOrLastName(cnic, firstName, lastName);
  }

  @ExceptionHandler(EntityNotFoundException.class)
  public ResponseEntity<Void> handleEntityNotFound() {
    return ResponseEntity.status(404).build();
  }
}

package com.inner.satisfaction.backend.person;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import java.util.List;
import java.util.Optional;
import javax.persistence.EntityNotFoundException;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
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

  @Override
  public List<Person> findAll() {
    throw new RuntimeException("Please use search method for listing");
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/all/paginated", method = RequestMethod.GET)
  public Page<Person> findAllPaginated(
    @RequestParam(required = false, defaultValue = "1", value = "page") int page,
    @RequestParam(required = false, defaultValue = "20", value = "size") int size) {
    PageRequest pageRequest = PageRequest.of(page - 1, size);
    return personService.findAll(pageRequest);
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = {"/search/cnic", "/search/findByCnic"}, method = RequestMethod.GET)
  public Person findByCnic(@RequestParam("cnic") String cnic) {
    return Optional.ofNullable(personService.findByCnic(cnic))
      .orElseThrow(() -> new EntityNotFoundException("Person with Cnic " + cnic + " Not found"));
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findByFormNo", method = RequestMethod.GET)
  public Person findByFormNo(@RequestParam("formNo") String formNo) {
    return Optional.ofNullable(personService.findByFormNo(formNo))
      .orElseThrow(
        () -> new EntityNotFoundException("Person with FormNo " + formNo + " Not found"));
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findByIdOrCnic", method = RequestMethod.GET)
  public List<Person> findByFormNo(@RequestParam("id") Long id, @RequestParam("cnic") String cnic) {
    return Optional.ofNullable(personService.findByIdOrCnic(cnic, id))
      .orElseThrow(
        () -> new EntityNotFoundException("Person with FormNo " + cnic + " " + id + " Not found"));
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findByCnicAndFirstNameAndLastNameAndFormNo", method = RequestMethod.GET)
  public Page<Person> findByCnicOrFirstNameOrLastName(
    @RequestParam(required = false, value = "cnic", defaultValue = "") String cnic,
    @RequestParam(required = false, value = "firstName", defaultValue = "") String firstName,
    @RequestParam(required = false, value = "lastName", defaultValue = "") String lastName,
    @RequestParam(required = false, value = "formNo", defaultValue = "0") String formNo,
    @RequestParam(required = false, defaultValue = "1", value = "page") int page,
    @RequestParam(required = false, defaultValue = "20", value = "size") int size) {
    PageRequest pageRequest = PageRequest.of(page - 1, size);
    return personService
      .findByCnicAndFirstNameAndLastNameAndFormNo(cnic, firstName, lastName, formNo, pageRequest);
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findByCnicOrFNameOrLNameOrIdOrDegreeOrAcadInstOrJamatiTitleOrMaos", method = RequestMethod.GET)
  public Page<Person> findByCnicOrFNameOrLNameOrIdOrDegreeOrAcadInstOrJamatiTitleOrAos(
    @RequestParam(required = false, value = "cnic", defaultValue = "") String cnic,
    @RequestParam(required = false, value = "firstName", defaultValue = "") String firstName,
    @RequestParam(required = false, value = "lastName", defaultValue = "") String lastName,
    @RequestParam(required = false, value = "id", defaultValue = "0") Long id, // formNo
    @RequestParam(required = false, value = "degree", defaultValue = "0") Long degree, // Degree
    // Academic Institution
    @RequestParam(required = false, value = "inst", defaultValue = "0") Long institution,
    // Jamati Title
    @RequestParam(required = false, value = "jamatiTitle", defaultValue = "0") Long jamatiTitle,
    // Major Area of Study
    @RequestParam(required = false, value = "maos", defaultValue = "0") Long maos,
    @RequestParam(required = false, defaultValue = "1", value = "page") int page,
    @RequestParam(required = false, defaultValue = "20", value = "size") int size) {
    PageRequest pageRequest = PageRequest.of(page - 1, size);
    return personService
      .findByFistNameOrLastNameOrFormNoOrCnicOrJamatiTitleOrDegreeOrMajorAreaOfStudyOrAcademicInstitution(
        cnic, firstName, lastName, id, degree, maos, institution, jamatiTitle, pageRequest);
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findByCnicAndNameAndIdAndDegreeAndAcadInstAndJamatiTitleAndMaos", method = RequestMethod.GET)
  public Page<Person> findByCnicAndNameAndIdAndDegreeAndAcadInstAndJamatiTitleAndMaos(
    @RequestParam(required = false, value = "cnic", defaultValue = "") String cnic,
    @RequestParam(required = false, value = "name", defaultValue = "") String name,
    @RequestParam(required = false, value = "id", defaultValue = "0") Long id, // formNo
    @RequestParam(required = false, value = "degree", defaultValue = "0") Long degree, // Degree
    // Academic Institution
    @RequestParam(required = false, value = "inst", defaultValue = "0") Long institution,
    // Jamati Title
    @RequestParam(required = false, value = "jamatiTitle", defaultValue = "0") Long jamatiTitle,
    // Major Area of Study
    @RequestParam(required = false, value = "maos", defaultValue = "0") Long maos,
    @RequestParam(required = false, defaultValue = "1", value = "page") int page,
    @RequestParam(required = false, defaultValue = "20", value = "size") int size) {
    PageRequest pageRequest = PageRequest.of(page - 1, size);
    return personService
      .findByFullNameAndIdAndCnicAndEducationInstitutionAndEducationDegreeAndAreaOfStudyAndJamatiTitle(
        name, cnic, id, institution, degree, maos, jamatiTitle, pageRequest);
  }

  @Override
  @GetMapping(ONE)
  public ResponseEntity<Person> findOne(@PathVariable("id") Long entityId) {
    Person entity = personService.findOneWithDetails(entityId);
    if (entity == null) {
      return ResponseEntity.status(404).body(null);
    }
    return ResponseEntity.ok(entity);
  }

  @ExceptionHandler(EntityNotFoundException.class)
  public ResponseEntity<Void> handleEntityNotFound() {
    return ResponseEntity.status(404).build();
  }
}

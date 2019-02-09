package com.inner.satisfaction.backend.person.appointment;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import java.util.List;
import javax.validation.Valid;
import org.springframework.util.Assert;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + PersonAppointmentController.PATH)
public class PersonAppointmentController extends BaseController<PersonAppointment> {

  public static final String PATH = "person/appointment";

  private final PersonAppointmentFacade personAppointmentFacade;

  public PersonAppointmentController(PersonAppointmentService personAppointmentService,
    PersonAppointmentFacade personAppointmentFacade) {
    super(personAppointmentService);
    this.personAppointmentFacade = personAppointmentFacade;
  }

  @Override
  public PersonAppointment save(
    @RequestBody PersonAppointment personAppointment) {
    Assert.notNull(personAppointment.getPersonId(), "Person Id should not be null");
    Assert.notNull(personAppointment.getAppointmentPositionId(), "Appointment Position Id should not be null");
    return personAppointmentFacade.save(personAppointment);
  }

  @Override
  public PersonAppointment putSave(
    @PathVariable("id") Long id,
    @RequestBody PersonAppointment personAppointment) {
    personAppointment.setId(id);
    Assert.notNull(personAppointment.getPersonId(), "Person Id should not be null");
    Assert.notNull(personAppointment.getAppointmentPositionId(), "Appointment Position Id should not be null");
    return personAppointmentFacade.save(personAppointment);
  }

  @PostMapping("recommend")
  public void recommend(@Valid @RequestBody PersonRecommendationDto personRecommendationDto) {
    personAppointmentFacade.recommendPersonInAppointment(personRecommendationDto);
  }

  public List<PersonAppointment> findRecommendationAndNominationByPersonIdAndCycleId(
    @RequestParam("personId") long personId,
    @RequestParam("cycleId") long cycleId
  ) {
    return personAppointmentFacade.findRecommendationAndNominationByPersonIdAndCycleId(personId, cycleId);
  }

  @Override
  public void delete(Long entityId) {
    personAppointmentFacade.delete(entityId);
  }
}
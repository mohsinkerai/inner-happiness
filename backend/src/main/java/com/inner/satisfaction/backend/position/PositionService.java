package com.inner.satisfaction.backend.position;

import com.inner.satisfaction.backend.appconfiguration.ApplicationConfiguration;
import com.inner.satisfaction.backend.appconfiguration.ApplicationConfigurationService;
import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.cycle.CycleService;
import com.inner.satisfaction.backend.cycle.position.CyclePositionOnInstitutionService;
import com.inner.satisfaction.backend.institution.InstitutionService;
import com.inner.satisfaction.backend.person.Person;
import com.inner.satisfaction.backend.person.PersonService;
import com.inner.satisfaction.backend.person.appointment.PersonAppointmentService;
import java.util.Optional;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;
import org.springframework.util.Assert;

@Slf4j
@Service
public class PositionService extends BaseService<Position> {

  private final PositionRepository positionRepository;
  private final InstitutionService institutionService;
  private final PersonService personService;
  private final CycleService cycleService;
  private final PersonAppointmentService personAppointmentService;
  private final CyclePositionOnInstitutionService cyclePositionOnInstitutionService;
  private final ApplicationConfigurationService applicationConfigurationService;

  protected PositionService(
    PositionRepository baseRepository,
    PositionValidation positionValidation,
    InstitutionService institutionService,
    PersonService personService,
    CycleService cycleService,
    PersonAppointmentService personAppointmentService,
    CyclePositionOnInstitutionService cyclePositionOnInstitutionService,
    ApplicationConfigurationService applicationConfigurationService) {

    super(baseRepository, positionValidation);
    this.institutionService = institutionService;
    this.personService = personService;
    this.cycleService = cycleService;
    this.personAppointmentService = personAppointmentService;
    this.positionRepository = baseRepository;
    this.cyclePositionOnInstitutionService = cyclePositionOnInstitutionService;
    this.applicationConfigurationService = applicationConfigurationService;
  }

  /**
   * Returns All the Inc
   */
  public PositionRecommendationResponse findByInstitutionId(long institutionId) {
//    Institution institution = institutionService.findOne(institutionId);
//    Assert.notNull(institution, "Invalid Institution Id Given, doesn't exist in db");
//
//    List<PositionOnInstitution> positionOnInstitutions = positionOnInstitutionService
//      .findByInstitutionId(institutionId);
//
//    List<PositionDetailsDto> positionDetails = Lists.newArrayList();
//
//    Integer currentCycleId = getKeyInteger(ApplicationConfigurationKeys.CURRENT_CYCLE_ID.name());
//    Integer previousCycleId = getKeyInteger(ApplicationConfigurationKeys.PREVIOUS_CYCLE_ID.name());
//
//    Cycle cycle = cycleService.findOne((long) currentCycleId);
//
//    for (PositionOnInstitution poi : positionOnInstitutions) {
//      Position position = findOne(poi.getPositionId());
//      CyclePositionOnInstitution cpi = cyclePositionOnInstitutionService
//        .findByCycleIdAndPositionOnInstitutionId(poi.getId(),
//          currentCycleId);
//      CyclePositionOnInstitution lastCpi = cyclePositionOnInstitutionService
//        .findByCycleIdAndPositionOnInstitutionId(poi.getId(),
//          previousCycleId);
//
////      Assert.notNull(appointment, "Either Cycle is not configured correctly or some other error resulting on CPI null");
////      Assert.notNull(lastCpi, "Either Previous Cycle is not configured correctly or some other error resulting on Previous CPI null");
//
//      if (lastCpi != null) {
//        PositionDetailsDto positionDetailsDto = new PositionDetailsDto();
//        positionDetailsDto.setPosition(position);
//        List<PositionDetailsPersonDto> pdpd = Lists.newArrayList();
//
//        // Person Incumbent
//        PersonAppointment incumbentPersonAppointment = personAppointmentService
//          .findByCpiIdAndIsAppointedTrue(lastCpi.getId());
//        Person incumbentPerson = getPerson(incumbentPersonAppointment.getPersonId());
//        PositionDetailsPersonDto incumbent = positionDetailsDto.new PositionDetailsPersonDto(
//          incumbentPerson, incumbentPersonAppointment);
//        positionDetailsDto.setIncumbent(incumbent);
//
//        if (cpi != null) {
//          List<PersonAppointment> cpis = personAppointmentService.findByCpiId(cpi.getId());
//          for (PersonAppointment cpi1 : cpis) {
//            Person one = getPerson(cpi1.getPersonId());
//            pdpd.add(positionDetailsDto.new PositionDetailsPersonDto(one, cpi1));
//          }
//          positionDetailsDto.setCpiId(cpi.getId());
//          positionDetailsDto.setPersonsNominated(pdpd);
//          positionDetailsDto.setPoi(poi);
//          positionDetails.add(positionDetailsDto);
//        }
//      }
//    }
//
//    return new PositionRecommendationResponse(positionDetails, institution, cycle);
    return null;
  }

  private Person getPerson(long personId) {
    Person one = personService.findOne(personId);
    Assert
      .notNull(one,
        "Person id is available in PersonAppointment but person profile is not available");
    return one;
  }

  private String getKey(String key) {
    return Optional.ofNullable(applicationConfigurationService
      .findByKey(key))
      .map(ApplicationConfiguration::getValue).orElseThrow(() ->
        new RuntimeException("Unable to process, as system doesn't contains key " + key));
  }

  private Integer getKeyInteger(String key) {
    String value = Optional.ofNullable(applicationConfigurationService
      .findByKey(key))
      .map(ApplicationConfiguration::getValue)
      .orElseThrow(
        () -> new RuntimeException("Unable to process, as system doesn't contains key " + key));
    try {
      return Integer.valueOf(value);
    } catch (NumberFormatException ex) {
      log.info("Invalid key value in app configuration for key {}, should be number", key);
      throw new RuntimeException("Invalid Integer key value in app configuration for key " + key);
    }
  }
}

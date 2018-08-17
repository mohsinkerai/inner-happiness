package com.inner.satisfaction.backend.position;

import com.google.common.collect.Lists;
import com.inner.satisfaction.backend.appconfiguration.ApplicationConfiguration;
import com.inner.satisfaction.backend.appconfiguration.ApplicationConfigurationKeys;
import com.inner.satisfaction.backend.appconfiguration.ApplicationConfigurationService;
import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.cycle.position.CyclePositionOnInstitution;
import com.inner.satisfaction.backend.cycle.position.CyclePositionOnInstitutionService;
import com.inner.satisfaction.backend.person.Person;
import com.inner.satisfaction.backend.person.PersonService;
import com.inner.satisfaction.backend.person.cpi.PersonCPI;
import com.inner.satisfaction.backend.person.cpi.PersonCPIService;
import com.inner.satisfaction.backend.position.PositionDetailsDto.PositionDetailsPersonDto;
import com.inner.satisfaction.backend.position.active.PositionOnInstitution;
import com.inner.satisfaction.backend.position.active.PositionOnInstitutionService;
import java.util.List;
import java.util.Optional;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;
import org.springframework.util.Assert;

@Slf4j
@Service
public class PositionService extends BaseService<Position> {

  private final PositionRepository positionRepository;
  private final PersonService personService;
  private final PersonCPIService personCPIService;
  private final PositionOnInstitutionService positionOnInstitutionService;
  private final CyclePositionOnInstitutionService cyclePositionOnInstitutionService;
  private final ApplicationConfigurationService applicationConfigurationService;

  protected PositionService(
    PositionRepository baseRepository,
    PositionValidation positionValidation,
    PersonService personService,
    PersonCPIService personCPIService,
    PositionOnInstitutionService positionOnInstitutionService,
    CyclePositionOnInstitutionService cyclePositionOnInstitutionService,
    ApplicationConfigurationService applicationConfigurationService) {
    super(baseRepository, positionValidation);
    this.personService = personService;
    this.personCPIService = personCPIService;
    this.positionOnInstitutionService = positionOnInstitutionService;
    this.positionRepository = baseRepository;
    this.cyclePositionOnInstitutionService = cyclePositionOnInstitutionService;
    this.applicationConfigurationService = applicationConfigurationService;
  }

  public List<PositionDetailsDto> findByInstitutionId(long institutionId) {
    List<PositionOnInstitution> positionOnInstitutions = positionOnInstitutionService
      .findByInstitutionId(institutionId);

    List<PositionDetailsDto> positionDetails = Lists.newArrayList();

    Integer currentCycleId = getKeyInteger(ApplicationConfigurationKeys.CURRENT_CYCLE_ID.name());
    Integer previousCycleId = getKeyInteger(ApplicationConfigurationKeys.PREVIOUS_CYCLE_ID.name());

    for (PositionOnInstitution poi : positionOnInstitutions) {
      CyclePositionOnInstitution cpi = cyclePositionOnInstitutionService
        .findByCycleIdAndPositionOnInstitutionId(poi.getId(),
          currentCycleId);
      CyclePositionOnInstitution lastCpi = cyclePositionOnInstitutionService
        .findByCycleIdAndPositionOnInstitutionId(poi.getId(),
          previousCycleId);

      Assert.notNull(cpi, "Either Cycle is not configured correctly or some other error resulting on CPI null");
      Assert.notNull(lastCpi, "Either Previous Cycle is not configured correctly or some other error resulting on Previous CPI null");

      PositionDetailsDto positionDetailsDto = new PositionDetailsDto();
      List<PositionDetailsPersonDto> pdpd = Lists.newArrayList();

      List<PersonCPI> cpis = personCPIService.findByCpiId(cpi.getId());

      for (PersonCPI cpi1 : cpis) {
        Person one = getPerson(cpi1.getPersonId());
        pdpd.add(positionDetailsDto.new PositionDetailsPersonDto(one, cpi1));
      }

      PersonCPI incumbentPersonCpi = personCPIService
        .findByCpiIdAndIsAppointedTrue(lastCpi.getId());
      Person incumbentPerson = getPerson(incumbentPersonCpi.getPersonId());
      PositionDetailsPersonDto incumbent = positionDetailsDto.new PositionDetailsPersonDto(
        incumbentPerson, incumbentPersonCpi);
      // Incumbent Logic Here

      positionDetailsDto.setCpiId(cpi.getId());
      positionDetailsDto.setPersonsNominated(pdpd);
      positionDetailsDto.setIncumbent(incumbent);
      positionDetails.add(positionDetailsDto);
    }

    return positionDetails;
  }

  private Person getPerson(long personId) {
    Person one = personService.findOne(personId);
    Assert
      .notNull(one, "Person id is available in PersonCPI but person profile is not available");
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

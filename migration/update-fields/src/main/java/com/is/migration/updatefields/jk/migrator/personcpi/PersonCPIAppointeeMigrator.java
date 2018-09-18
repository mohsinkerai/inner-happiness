package com.is.migration.updatefields.jk.migrator.personcpi;

import com.is.migration.updatefields.jk.persistance.jamatkhana.JamatkhanaEntity;
import com.is.migration.updatefields.jk.persistance.jamatkhana.JamatkhanaRepository;
import com.is.migration.updatefields.jk.persistance.person.Person;
import com.is.migration.updatefields.jk.persistance.person.PersonRepository;
import com.is.migration.updatefields.jk.persistance.person_cpi.PersonCPI;
import com.is.migration.updatefields.jk.persistance.person_cpi.PersonCPIRepository;
import com.is.migration.updatefields.jk.persistance.poi.Poi;
import com.is.migration.updatefields.jk.persistance.poi.PoiRepository;
import com.is.migration.updatefields.jk.persistance.position.Position;
import com.is.migration.updatefields.jk.persistance.position.PositionRepository;
import com.opencsv.CSVReader;
import java.io.IOException;
import java.io.Reader;
import java.nio.charset.Charset;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.io.ClassPathResource;
import org.springframework.stereotype.Component;
import org.springframework.util.CollectionUtils;

@Slf4j
@Component
public class PersonCPIAppointeeMigrator {

  @Autowired
  PersonCPIRepository personCPIRepository;
  @Autowired
  PersonRepository personRepository;
  @Autowired
  PoiRepository poiRepository;
  @Autowired
  PositionRepository positionRepository;
  @Autowired
  JamatkhanaRepository jamatkhanaRepository;

  public void migrate() throws IOException {
    List<PersonCPIAppointeeDto> personCPIDtos = getPersonCPIDtos();

    for (PersonCPIAppointeeDto dto : personCPIDtos) {
//    for (int i = 200; i < 210; i++) {
//      PersonCPIDto dto = personCPIDtos.get(i);

      try {
        if(dto.getPersonId() == null) {
          continue;
        }
        Position posititon = getPosititon(dto.getPositionId(), dto.getSeatId());
        if (posititon== null) {
          log.info("Position is Null");
        }
        Person person = getPerson(dto.getPersonId());
        if (person == null) {
          log.info("Person is Null");
        }
        JamatkhanaEntity level = getLevel(dto.getInstitutionId());
        if (level == null) {
          log.info("Level is Null");
        }

        if(posititon == null || person == null || level == null) {
          log.info("Either Person {}, Level {} or Position {} is null {}", person, level, posititon, dto);
          continue;
        }
        Poi poi = getPoi(dto.getCycleId(), posititon.getId(), level.getId());
        if (poi == null) {
          log.info("CPoi is Null {}", dto);
        }
        PersonCPI personCpi = getPersonCpi(person.getId(), poi.getId());
        if(personCpi == null) {
          log.info("Person CPI is not available, {} {}", person, poi);
          continue;
        }

        personCpi.setIsAppointed(Boolean.TRUE);
        personCPIRepository.save(personCpi);
//        log.info("PersonCPI {}", personCPI);
      } catch (Exception ex) {
        log.error("Exception aayaaa re", ex);
      }
    }
  }

  private PersonCPI getPersonCpi(Long personId, Long cpiId) {
    List<PersonCPI> personCpi = personCPIRepository
        .findByPersonIdEqualsAndAndCpiIdEquals(personId, cpiId);
    if (CollectionUtils.isEmpty(personCpi)) {
      return null;
    }
    return personCpi.get(0);
  }

  private Poi getPoi(Long cycleId, Long positionId, Long institutionId) {
    List<Poi> person = poiRepository
        .findByCycleIdEqualsAndPositionIdEqualsAndInstitutionIdEquals(cycleId, positionId,
            institutionId);
    if (CollectionUtils.isEmpty(person)) {
      return null;
    }
    return person.get(0);
  }

  private JamatkhanaEntity getLevel(Long oldId) {
    List<JamatkhanaEntity> level = jamatkhanaRepository.findByOldId(oldId);
    if (CollectionUtils.isEmpty(level)) {
      return null;
    }
    return level.get(0);
  }

  private Person getPerson(Long oldId) {
    List<Person> person = personRepository.findByOldId(oldId);
    if (CollectionUtils.isEmpty(person)) {
      return null;
    }
    return person.get(0);
  }

  private Position getPosititon(Long oldId, Long seatId) {
    List<Position> position = positionRepository.findByOldIdEqualsAndSeatIdEquals(oldId, seatId);
    if (CollectionUtils.isEmpty(position)) {
      return null;
    }
    return position.get(0);
  }

  private List<PersonCPIAppointeeDto> getPersonCPIDtos() throws IOException {
    ArrayList<PersonCPIAppointeeDto> jks = new ArrayList<>();
    try (
        Reader reader = Files
            .newBufferedReader(Paths.get(new ClassPathResource("Appointee.csv").getURI()),
                Charset.forName("UTF-8"));
        CSVReader csvReader = new CSVReader(reader);
    ) {
      String[] nextRecord;
      while ((nextRecord = csvReader.readNext()) != null) {
        try {
          jks.add(PersonCPIAppointeeDto.builder()
              .cycleId(Long.valueOf(nextRecord[0]))
              .institutionId(Long.valueOf(nextRecord[1]))
              .positionId(Long.valueOf(nextRecord[2]))
              .seatId(Long.valueOf(nextRecord[3]))
              .personId(Long.valueOf(nextRecord[4]))
              .build());
        } catch (Exception ex) {
          log.error("Unable to Parse {}", nextRecord);
        }

      }
    }
    return jks;
  }
}

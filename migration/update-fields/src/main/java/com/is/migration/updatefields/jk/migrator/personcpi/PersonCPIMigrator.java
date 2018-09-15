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
public class PersonCPIMigrator {

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
    List<PersonCPIDto> personCPIDtos = getPersonCPIDtos();

    for (PersonCPIDto dto : personCPIDtos) {
//    for (int i = 200; i < 210; i++) {
//      PersonCPIDto dto = personCPIDtos.get(i);

      try {
        if (dto.getIncumbentId().equals(dto.getPersonId())) {
          continue;
        }
        Position posititon = getPosititon(dto.getPositionId(), dto.getSeatId());
        if (posititon == null) {
          List<Position> byOldId = positionRepository.findByOldId(dto.getPositionId());
          if(!CollectionUtils.isEmpty(byOldId)) {
            posititon = Position.builder().seatId(dto.getSeatId())
                .oldId(dto.getPositionId())
                .name(byOldId.get(0).getName() + ' ' + dto.getSeatId())
                .build();
            posititon = positionRepository.save(posititon);
          }
        }
        Person person = getPerson(dto.getPersonId());
        if (person == null) {
          log.info("Person is Null");
        }
        JamatkhanaEntity level = getLevel(dto.getInstitutionId());
        if (level == null) {
          log.info("Level is Null");
        }
//        log.info("PersonID {}, poiId {}, cycle {}, posititon {}, institution {}", person, poi, dto.getCycleId(), posititon.getId(), dto.getInstitutionId());

        if (posititon == null || person == null || level == null) {
          log.info("Either Person {}, Level {} or Position {} is null {}", person, level, posititon,
              dto);
          continue;
        }
        Poi poi = getPoi(dto.getCycleId(), posititon.getId(), level.getId());
        if (poi == null) {
          log.info("CPoi is Null {}", dto);
          continue;
        }

        PersonCPI personCPI = new PersonCPI();
        personCPI.setPersonId(person.getId());
        personCPI.setCpiId(poi.getId());
        personCPI.setPriority(dto.getPreference());
        personCPI.setIsAppointed(false);
        personCPI.setRecommended(dto.getRecommended() > 0);

        personCPIRepository.save(personCPI);
//        log.info("PersonCPI {}", personCPI);
      } catch (Exception ex) {
        log.error("Exception aayaaa re", ex);
      }
    }
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

  private Position getPosititon(Long oldId, long seatId) {
    List<Position> position = positionRepository.findByOldIdEqualsAndSeatIdEquals(oldId, seatId);
    if (CollectionUtils.isEmpty(position)) {
      return null;
    } else if (position.size() > 1) {
      log.info("Size of position should be 1, id is {} {}", oldId, seatId);
    }
    return position.get(0);
  }

  private List<PersonCPIDto> getPersonCPIDtos() throws IOException {
    ArrayList<PersonCPIDto> jks = new ArrayList<>();
    try (
        Reader reader = Files
            .newBufferedReader(Paths.get(new ClassPathResource("Recommendation.csv").getURI()),
                Charset.forName("UTF-8"));
        CSVReader csvReader = new CSVReader(reader);
    ) {
      String[] nextRecord;
      while ((nextRecord = csvReader.readNext()) != null) {
        try {
          jks.add(PersonCPIDto.builder()
              .cycleId(Long.valueOf(nextRecord[0]))
              .institutionId(Long.valueOf(nextRecord[1]))
              .positionId(Long.valueOf(nextRecord[2]))
              .seatId(Long.valueOf(nextRecord[3]))
              .personId(Long.valueOf(nextRecord[4]))
              .incumbentId(Long.valueOf(nextRecord[5]))
              .recommended(Long.valueOf(nextRecord[6]))
              .preference(Long.valueOf(nextRecord[7]))
              .build());
        } catch (Exception ex) {
          log.error("Unable to Parse {}", nextRecord);
        }

      }
    }
    return jks;
  }
}

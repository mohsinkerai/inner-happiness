package com.is.migration.updatefields.jk.migrator.position;

import com.is.migration.updatefields.jk.persistance.jamatkhana.JamatkhanaEntity;
import com.is.migration.updatefields.jk.persistance.jamatkhana.JamatkhanaRepository;
import com.is.migration.updatefields.jk.persistance.person.PersonRepository;
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
public class PositionMigrator {

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
    List<PositionDto> personCPIDtos = getPersonCPIDtos();

    for (PositionDto dto : personCPIDtos) {
//    for (int i = 0; i < 10; i++) {
//      PositionDto dto = personCPIDtos.get(i);
      try {
        Position position = Position.builder()
            .name(dto.getName())
            .oldId(dto.getOldId())
            .seatId(1l)
            .build();
        positionRepository.save(position);
      } catch (Exception ex) {
        log.error("Exception aayaaa re", ex);
      }
    }

  }

//  private void createPoi(PositionDto dto, Long id) {
//    CPoi poi = new CPoi();
//    poi.setCycleId(dto.getCycleId());
//    poi.setCycleId(dto.getInstitutionId());
//    poi.setCycleId(dto.getPositionId());
//
//    poiRepository.save()
//  }

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

  private Position getPosititon(Long oldId) {
    List<Position> position = positionRepository.findByOldId(oldId);
    if (CollectionUtils.isEmpty(position)) {
      return null;
    }
    return position.get(0);
  }

  private List<PositionDto> getPersonCPIDtos() throws IOException {
    ArrayList<PositionDto> jks = new ArrayList<>();
    try (
        Reader reader = Files
            .newBufferedReader(Paths.get(new ClassPathResource("Position.csv").getURI()),
                Charset.forName("UTF-8"));
        CSVReader csvReader = new CSVReader(reader);
    ) {
      String[] nextRecord;
      while ((nextRecord = csvReader.readNext()) != null) {
        try {
          jks.add(PositionDto.builder()
              .oldId(Long.valueOf(nextRecord[0].trim()))
              .name(nextRecord[1].trim())
              .build());
        } catch (Exception ex) {
          log.error("Unable to Parse {}", nextRecord);
        }

      }
    }
    return jks;
  }
}

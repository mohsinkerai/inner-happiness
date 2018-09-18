package com.is.migration.updatefields.jk.migrator.poi;

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
public class PoiMigrator {

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
    List<PoiDto> personCPIDtos = getCPIDtos();

    for(PoiDto dto : personCPIDtos) {
//    for (int i = 0; i < 1; i++) {
//      PoiDto dto = personCPIDtos.get(i);

      try {
        List<Position> posititons = getPosititons(dto.getPositionId());
        int size = posititons.size();

        if (dto.getMin() > size) {
          Position position = posititons.get(0);
          for (int j = size + 1; j <= dto.getMin(); j++) {
            Position newPosition = Position.builder()
                .name(position.getName() + " " + j)
                .oldId(position.getOldId())
                .seatId((long)j)
                .build();
            positionRepository.save(newPosition);
          }
        }
        posititons = getPosititons(dto.getPositionId());
        JamatkhanaEntity level = getLevel(dto.getInstitutionId());
        if (level == null) {
          log.info("level {} is not available, continuing",dto.getInstitutionId());
          continue;
        }

        for (Position p : posititons) {
          Poi poi = Poi.builder()
              .cycleId(dto.getCycleId())
              .institutionId(level.getId())
              .positionId(p.getId())
              .min(dto.getMin())
              .max(dto.getMax())
              .desired(dto.getDesired())
              .build();
          poiRepository.save(poi);
        }
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

  private List<Position> getPosititons(Long oldId) {
    List<Position> position = positionRepository.findByOldId(oldId);
    return position;
  }

  private List<PoiDto> getCPIDtos() throws IOException {
    ArrayList<PoiDto> jks = new ArrayList<>();
    try (
        Reader reader = Files
            .newBufferedReader(Paths.get(new ClassPathResource("InstitutionPosition.csv").getURI()),
                Charset.forName("UTF-8"));
        CSVReader csvReader = new CSVReader(reader);
    ) {
      String[] nextRecord;
      while ((nextRecord = csvReader.readNext()) != null) {
        try {
          jks.add(PoiDto.builder()
              .cycleId(Long.valueOf(nextRecord[0].trim()))
              .institutionId(Long.valueOf(nextRecord[1].trim()))
              .positionId(Long.valueOf(nextRecord[2].trim()))
              // Min is the seat
              .min(Long.valueOf(nextRecord[3].trim()))
              .max(Long.valueOf(nextRecord[4].trim()))
              .desired(Long.valueOf(nextRecord[6].trim()))
              .build());
        } catch (Exception ex) {
          log.error("Unable to Parse {}", nextRecord);
        }

      }
    }
    return jks;
  }
}

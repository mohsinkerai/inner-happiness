package com.is.migration.updatefields.jk;

import com.is.migration.updatefields.jk.persistance.JamatkhanaEntity;
import com.is.migration.updatefields.jk.persistance.JamatkhanaRepository;
import com.opencsv.CSVReader;
import java.io.IOException;
import java.io.Reader;
import java.nio.charset.Charset;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.io.ClassPathResource;
import org.springframework.stereotype.Component;
import org.springframework.util.CollectionUtils;

@Slf4j
@Component
public class JamatkhanaMigrator {

  @Autowired
  JamatkhanaRepository jamatkhanaRepository;

  private List<Jamatkhana> verifiedJks = new ArrayList<>();
  private List<Jamatkhana> doesntExistInDb = new ArrayList<>();
  private List<Jamatkhana> otherConsistencyIssues = new ArrayList<>();

  public void migrate() throws IOException {
    List<Jamatkhana> jks = getJks();
    verifyJk(jks);
  }

  private void verifyJk(List<Jamatkhana> jks) {
    for (int i = 0; i < 20; i++) {
      Jamatkhana jamatkhana = jks.get(i);
      List<JamatkhanaEntity> entityList = jamatkhanaRepository
          .findByOldCodeEqualsAndIsClosedFalse(jamatkhana.getEoCode());
      if (entityList.size() == 1) {
        boolean isVerified = verifyJkDetails(jamatkhana, entityList.get(0));
        if (isVerified) {
          verifiedJks.add(jamatkhana);
        } else {
          otherConsistencyIssues.add(jamatkhana);
        }
      } else if (CollectionUtils.isEmpty(entityList)) {
        doesntExistInDb.add(jamatkhana);
      } else {
        otherConsistencyIssues.add(jamatkhana);
      }

      log.info("Total Entities Got {}, List = {}, Excel = {}", entityList.size(), entityList,
          jamatkhana);
    }

    List<String> verified = verifiedJks.stream().map(Jamatkhana::getEoCode)
        .collect(Collectors.toList());
    List<String> doesntExist = doesntExistInDb.stream().map(Jamatkhana::getEoCode)
        .collect(Collectors.toList());
    List<String> otherConsistency = otherConsistencyIssues.stream().map(Jamatkhana::getEoCode)
        .collect(Collectors.toList());
    log.info("Verified JK are {}, {}, {}", verified, doesntExist, otherConsistency);
  }

  private boolean verifyJkDetails(Jamatkhana jamatkhana, JamatkhanaEntity jamatkhanaEntity) {
    boolean isNameEqual = jamatkhana.getEoName().toLowerCase().equals(jamatkhanaEntity.getFullName().toLowerCase());
    Optional<JamatkhanaEntity> localCouncil = jamatkhanaRepository
        .findById(jamatkhanaEntity.getLevelParentId());
    if (!isNameEqual || !localCouncil.isPresent()) {
      // If name is not equal, update parent
      return false;
    }
    isNameEqual = ("LC " + jamatkhana.getLcName()).equals(localCouncil.get().getName());
    Optional<JamatkhanaEntity> regionalCouncil = jamatkhanaRepository
        .findById(localCouncil.get().getLevelParentId());
    if (!isNameEqual || !regionalCouncil.isPresent()) {
      // If name is not equal, update parent, else return
      return false;
    }
    isNameEqual = (jamatkhana.getRcName()).equals(regionalCouncil.get().getName());
    if (!isNameEqual) {
      // If name is not equal, update parnet
      return false;
    }
    return true;
  }

  private Optional<JamatkhanaEntity> getById(Long id) {
    return jamatkhanaRepository.findById(id);
  }

  private List<Jamatkhana> getJks() throws IOException {
    ArrayList<Jamatkhana> jks = new ArrayList<>();
    try (
        Reader reader = Files.newBufferedReader(Paths.get(new ClassPathResource("jk.csv").getURI()),
            Charset.forName("UTF-8"));
        CSVReader csvReader = new CSVReader(reader);
    ) {
      String[] nextRecord;
      while ((nextRecord = csvReader.readNext()) != null) {
        jks.add(Jamatkhana.builder()
            .lcName(nextRecord[0])
            .rcName(nextRecord[1])
            .ncCode(nextRecord[2])
            .ncName(nextRecord[3])
            .eoCode(nextRecord[4])
            .eoName(nextRecord[5]).build());

      }
    }
    return jks;
  }
}

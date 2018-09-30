package com.is.migration.updatefields;

import com.is.migration.updatefields.jk.migrator.cpoi.CpoiMigrator;
import com.is.migration.updatefields.jk.migrator.jamatkhana.JamatkhanaMigrator;
import com.is.migration.updatefields.jk.migrator.personcpi.PersonCPIAppointeeMigrator;
import com.is.migration.updatefields.jk.migrator.personcpi.PersonCPIMigrator;
import com.is.migration.updatefields.jk.migrator.poi.PoiMigrator;
import com.is.migration.updatefields.jk.migrator.position.PositionMigrator;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

@Slf4j
@Component
public class MigrationRunner implements CommandLineRunner {

  @Autowired
  JamatkhanaMigrator jamatkhanaMigrator;

  @Autowired
  PersonCPIMigrator personCPIMigrator;

  @Autowired
  PersonCPIAppointeeMigrator personCPIAppMigrator;

  @Autowired
  PoiMigrator poiMigrator;

  @Autowired
  CpoiMigrator cpoiMigrator;

  @Autowired
  PositionMigrator positionMigrator;

  @Override
  public void run(String... args) throws Exception {
    log.info("Hello World");

    // 1. Migrate Positions
//    positionMigrator.migrate();

    // 2. Institution, Cycle and Level from Ali-Ahad

    // 3. Position On Institution
    // Cycle Maps As it Is, Institution needs to be checked, position too
//    poiMigrator.migrate();
    cpoiMigrator.migrate();

    // 4. Migrate All Nominations
//    personCPIMigrator.migrate();

    // 5. Migrate All Appointee
//    personCPIAppMigrator.migrate();
//    jamatkhanaMigrator.migrate();
//    poiMigrator.migrate();;
  }
}

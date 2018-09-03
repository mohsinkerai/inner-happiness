package com.is.migration.updatefields;

import com.is.migration.updatefields.jk.JamatkhanaMigrator;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

@Slf4j
@Component
public class MigrationRunner implements CommandLineRunner {

  @Autowired
  JamatkhanaMigrator jamatkhanaMigrator;

  @Override
  public void run(String... args) throws Exception {
    log.info("Hello World");

    jamatkhanaMigrator.migrate();
  }
}

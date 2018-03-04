package com.inner.satisfaction.backend;

import com.inner.satisfaction.backend.level.LevelService;
import com.inner.satisfaction.backend.person.PersonRepository;
import com.inner.satisfaction.backend.person.relation.PersonRelationPersonRepository;
import com.inner.satisfaction.backend.person.relation.PersonRelationPersonService;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

@Slf4j
@Component
public class MyCommandLineRunner implements CommandLineRunner{

  @Autowired
  private LevelService levelService;

  @Autowired
  private PersonRelationPersonService personRelationPersonService;

  @Autowired
  private PersonRepository personRepository;

  @Override
  public void run(String... strings) throws Exception {
    log.info("Total Levels are {}",levelService.findAll());
    log.info("Person Relation Person {}", personRelationPersonService.findAll());
    log.info("Person {}", personRepository.findByCnic("42101-1111111-1"));
  }
}

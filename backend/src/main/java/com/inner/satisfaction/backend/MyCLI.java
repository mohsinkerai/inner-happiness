package com.inner.satisfaction.backend;

import com.inner.satisfaction.backend.person.Person;
import com.inner.satisfaction.backend.person.PersonRepository;
import java.util.List;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Component;

@Component
@Slf4j
public class MyCLI implements CommandLineRunner {

  @Autowired
  PersonRepository personRepository;

  @Override
  public void run(String... args) throws Exception {
    Page<Person> byFirstName = personRepository.findByFullNameAndIdAndCnicAndEducationInstitutionAndEducationDegreeAndAreaOfStudyAndJamatiTitle("+hafiz", null, 466l, null, null, null, null, Pageable.unpaged());

    log.info("Returned RESULT {}", byFirstName.getContent());
  }
}

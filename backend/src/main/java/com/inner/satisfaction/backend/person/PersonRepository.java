package com.inner.satisfaction.backend.person;

import com.inner.satisfaction.backend.base.BaseRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface PersonRepository extends BaseRepository<Person> {

  Person findByCnic(String cnic);
}

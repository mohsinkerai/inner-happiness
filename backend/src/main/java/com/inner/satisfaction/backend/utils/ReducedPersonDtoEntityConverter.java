package com.inner.satisfaction.backend.utils;

import com.inner.satisfaction.backend.person.Person;
import com.inner.satisfaction.backend.person.dto.ReducedPersonDto;
import org.springframework.stereotype.Component;

@Component
public class ReducedPersonDtoEntityConverter implements
  DtoEntityConverter<ReducedPersonDto,Person> {

  @Override
  public ReducedPersonDto convertTo(Person entity) {
    return ReducedPersonDto.builder()
      .id(entity.getId())
      .cnic(entity.getCnic())
      .dateOfBirth(entity.getDateOfBirth())
      .familyName(entity.getFamilyName())
      .fathersName(entity.getFathersName())
      .firstName(entity.getFirstName())
      .jamatiTitle(entity.getJamatiTitle())
      .salutation(entity.getSalutation())
      .build();
  }

  @Override
  public Person convertFrom(ReducedPersonDto dto) {
    Person person = Person.builder()
      .cnic(dto.getCnic())
      .dateOfBirth(dto.getDateOfBirth())
      .familyName(dto.getFamilyName())
      .fathersName(dto.getFathersName())
      .firstName(dto.getFirstName())
      .jamatiTitle(dto.getJamatiTitle())
      .salutation(dto.getSalutation())
      .build();

    person.setId(dto.getId());
    return person;
  }
}

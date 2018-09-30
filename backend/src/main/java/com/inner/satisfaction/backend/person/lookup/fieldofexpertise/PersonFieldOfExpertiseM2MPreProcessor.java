package com.inner.satisfaction.backend.person.lookup.fieldofexpertise;

import com.google.common.collect.Lists;
import com.inner.satisfaction.backend.lookups.fieldofexperties.FieldOfExpertise;
import com.inner.satisfaction.backend.lookups.fieldofexperties.FieldOfExpertiseService;
import com.inner.satisfaction.backend.person.Person;
import com.inner.satisfaction.backend.person.lookup.base.BaseM2MProcessingService;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import org.springframework.stereotype.Component;

@Component
public class PersonFieldOfExpertiseM2MPreProcessor extends
  BaseM2MProcessingService<PersonFieldOfExpertise, FieldOfExpertiseDto> {

  private final FieldOfExpertiseService fieldOfExpertiseService;
  private final PersonFieldOfExpertiseService personFieldOfExpertiseService;

  public PersonFieldOfExpertiseM2MPreProcessor(
    FieldOfExpertiseService fieldOfExpertiseService,
    PersonFieldOfExpertiseService personFieldOfExpertiseService) {
    this.fieldOfExpertiseService = fieldOfExpertiseService;
    this.personFieldOfExpertiseService = personFieldOfExpertiseService;
  }

  @Override
  protected List<PersonFieldOfExpertise> findPersonEntities(long personId) {
    return personFieldOfExpertiseService.findByPersonId(personId);
  }

  @Override
  protected Person populateEntityInPerson(Person person, List<PersonFieldOfExpertise> e) {
    List<String> skills = e.stream()
      .map(PersonFieldOfExpertise::getFieldOfExpertiseId)
      .map(String::valueOf)
      .collect(Collectors.toList());
    person.setFieldOfExpertise(skills);
    return person;
  }

  @Override
  protected long getEntityId(FieldOfExpertiseDto dto) {
    String skill = dto.getFieldOfExpertise();
    try {
      return Long.parseLong(skill);
    } catch (NumberFormatException nfe) {
      FieldOfExpertise fieldOfExpertise = new FieldOfExpertise();
      fieldOfExpertise.setName(skill);
      fieldOfExpertise = fieldOfExpertiseService.save(fieldOfExpertise);
      return fieldOfExpertise.getId();
    }
  }

  @Override
  protected FieldOfExpertiseDto setPersonId(FieldOfExpertiseDto dto, long id) {
    dto.setPersonId(id);
    return dto;
  }

  @Override
  protected FieldOfExpertiseDto setEntityId(FieldOfExpertiseDto dto, long id) {
    dto.setFieldOfExpertiseId(id);
    return dto;
  }

  @Override
  protected PersonFieldOfExpertise convert(FieldOfExpertiseDto dto) {
    return PersonFieldOfExpertise.builder()
      .personId(dto.getPersonId())
      .fieldOfExpertiseId(dto.getFieldOfExpertiseId())
      .build();
  }

  @Override
  protected List<FieldOfExpertiseDto> convert(Person person) {
    Optional<List<String>> skills = Optional.ofNullable(person.getFieldOfExpertise());
    return skills.orElse(Lists.newArrayList())
      .stream()
      .map(this::convert)
      .collect(Collectors.toList());
  }

  private FieldOfExpertiseDto convert(String val) {
    return FieldOfExpertiseDto.builder()
      .fieldOfExpertise(val)
      .build();
  }

  @Override
  protected void removeAllEntityByPersonId(long personId) {
    personFieldOfExpertiseService.removeByPersonId(personId);
  }

  @Override
  protected PersonFieldOfExpertise saveEntity(PersonFieldOfExpertise personFieldOfExpertise) {
    return personFieldOfExpertiseService.save(personFieldOfExpertise);
  }
}

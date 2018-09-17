package com.inner.satisfaction.backend.person.lookup.fieldofexpertise;

import com.google.common.collect.Lists;
import com.inner.satisfaction.backend.lookups.fieldofexperties.FieldOfExpertise;
import com.inner.satisfaction.backend.lookups.fieldofexperties.FieldOfExpertiseService;
import com.inner.satisfaction.backend.person.Person;
import com.inner.satisfaction.backend.person.lookup.base.BaseM2MProcessingService;
import com.inner.satisfaction.backend.person.lookup.dto.PersonSkillsDto;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import org.springframework.stereotype.Component;

@Component
public class PersonFieldOfExpertiseM2MPreProcessor extends
  BaseM2MProcessingService<PersonFieldOfExpertise, PersonSkillsDto> {

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
    person.setSkills(skills);
    return person;
  }

  @Override
  protected long getEntityId(PersonSkillsDto dto) {
    String skill = dto.getSkill();
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
  protected PersonSkillsDto setPersonId(PersonSkillsDto dto, long id) {
    dto.setPersonId(id);
    return dto;
  }

  @Override
  protected PersonSkillsDto setEntityId(PersonSkillsDto dto, long id) {
    dto.setSkillId(id);
    return dto;
  }

  @Override
  protected PersonFieldOfExpertise convert(PersonSkillsDto dto) {
    return PersonFieldOfExpertise.builder()
      .personId(dto.getPersonId())
      .fieldOfExpertiseId(dto.getSkillId())
      .build();
  }

  @Override
  protected List<PersonSkillsDto> convert(Person person) {
    Optional<List<String>> skills = Optional.ofNullable(person.getSkills());
    return skills.orElse(Lists.newArrayList())
      .stream()
      .map(this::convert)
      .collect(Collectors.toList());
  }

  private PersonSkillsDto convert(String val) {
    return PersonSkillsDto.builder()
      .skill(val)
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

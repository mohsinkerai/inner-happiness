package com.inner.satisfaction.backend.person.skills;

import com.inner.satisfaction.backend.lookups.skill.Skill;
import com.inner.satisfaction.backend.lookups.skill.SkillService;
import com.inner.satisfaction.backend.person.Person;
import com.inner.satisfaction.backend.person.base.BaseM2MProcessingService;
import com.inner.satisfaction.backend.person.dto.PersonSkillsDto;
import java.util.List;
import java.util.stream.Collectors;
import org.springframework.stereotype.Component;

@Component
public class PersonSkillM2MPreProcessor extends
  BaseM2MProcessingService<PersonSkill, PersonSkillsDto> {

  private final SkillService skillService;
  private final PersonSkillService personSkillService;

  public PersonSkillM2MPreProcessor(
    SkillService skillService,
    PersonSkillService personSkillService) {
    this.skillService = skillService;
    this.personSkillService = personSkillService;
  }

  @Override
  protected List<PersonSkill> findPersonEntities(long personId) {
    return personSkillService.findByPersonId(personId);
  }

  @Override
  protected Person populateEntityInPerson(Person person, List<PersonSkill> e) {
    List<String> skills = e.stream()
      .map(PersonSkill::getSkillId)
      .map(skillService::findOne)
      .map(Skill::getName)
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
      Skill skillEntity = new Skill();
      skillEntity.setName(skill);
      skillEntity = skillService.save(skillEntity);
      return skillEntity.getId();
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
  protected PersonSkill convert(PersonSkillsDto dto) {
    return PersonSkill.builder()
      .personId(dto.getPersonId())
      .skillId(dto.getSkillId())
      .build();
  }

  @Override
  protected List<PersonSkillsDto> convert(Person person) {
    return person.getSkills()
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
    personSkillService.removeByPersonId(personId);
  }

  @Override
  protected PersonSkill saveEntity(PersonSkill personSkill) {
    return personSkillService.save(personSkill);
  }
}

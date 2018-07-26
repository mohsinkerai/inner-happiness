package com.inner.satisfaction.backend.person.professionalmembership;

import com.inner.satisfaction.backend.lookups.professionalmembership.ProfessionalMembership;
import com.inner.satisfaction.backend.lookups.professionalmembership.ProfessionalMembershipService;
import com.inner.satisfaction.backend.person.Person;
import com.inner.satisfaction.backend.person.base.BaseM2MProcessingService;
import com.inner.satisfaction.backend.person.dto.PersonPMDto;
import java.util.Collections;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import org.springframework.stereotype.Component;

@Component
public class PersonProfessionalMembershipM2MPreProcessor extends
  BaseM2MProcessingService<PersonProfessionalMembership, PersonPMDto> {

  private final ProfessionalMembershipService professionalMembershipService;
  private final PersonProfessionalMembershipService personProfessionalMembershipService;

  public PersonProfessionalMembershipM2MPreProcessor(
    ProfessionalMembershipService professionalMembershipService,
    PersonProfessionalMembershipService personProfessionalMembershipService) {
    this.professionalMembershipService = professionalMembershipService;
    this.personProfessionalMembershipService = personProfessionalMembershipService;
  }

  @Override
  protected List<PersonProfessionalMembership> findPersonEntities(long personId) {
    return personProfessionalMembershipService.findByPersonId(personId);
  }

  @Override
  protected Person populateEntityInPerson(Person person, List<PersonProfessionalMembership> e) {
    List<String> professionalMembership = e.stream()
      .map(PersonProfessionalMembership::getProfessionalMembershipId)
      .map(professionalMembershipService::findOne)
      .map(ProfessionalMembership::getName)
      .collect(Collectors.toList());
    person.setProfessionalMemberships(professionalMembership);
    return person;
  }

  @Override
  protected long getEntityId(PersonPMDto dto) {
    String pm = dto.getProfessionalMembership();
    try {
      return Long.parseLong(pm);
    } catch (NumberFormatException nfe) {
      ProfessionalMembership professionalMembership = new ProfessionalMembership();
      professionalMembership.setName(pm);
      professionalMembership = professionalMembershipService.save(professionalMembership);
      return professionalMembership.getId();
    }
  }

  @Override
  protected PersonPMDto setPersonId(PersonPMDto dto, long id) {
    dto.setPersonId(id);
    return dto;
  }

  @Override
  protected PersonPMDto setEntityId(PersonPMDto dto, long id) {
    dto.setProfessionalMembershipId(id);
    return dto;
  }

  @Override
  protected PersonProfessionalMembership convert(PersonPMDto dto) {
    return PersonProfessionalMembership.builder()
      .personId(dto.getPersonId())
      .professionalMembershipId(dto.getProfessionalMembershipId())
      .build();
  }

  @Override
  protected List<PersonPMDto> convert(Person person) {
    Optional<List<String>> professionalMemberships = Optional
      .ofNullable(person.getProfessionalMemberships());
    if (professionalMemberships.isPresent()) {
      return professionalMemberships.get()
        .stream()
        .map(this::convert)
        .collect(Collectors.toList());
    } else {
      return Collections.emptyList();
    }
  }

  private PersonPMDto convert(String val) {
    return PersonPMDto.builder()
      .professionalMembership(val)
      .build();
  }

  @Override
  protected void removeAllEntityByPersonId(long personId) {
    personProfessionalMembershipService.removeByPersonId(personId);
  }

  @Override
  protected PersonProfessionalMembership saveEntity(
    PersonProfessionalMembership personProfessionalMembership) {
    return personProfessionalMembershipService.save(personProfessionalMembership);
  }
}

package com.inner.satisfaction.backend.utils;

import com.inner.satisfaction.backend.person.Person;
import com.inner.satisfaction.backend.person.PersonDto;
import com.inner.satisfaction.backend.person.akdntraining.PersonAkdnTrainingService;
import com.inner.satisfaction.backend.person.education.PersonEducationService;
import com.inner.satisfaction.backend.person.skills.PersonSkillsService;
import java.util.stream.Collectors;
import org.springframework.stereotype.Component;

@Component
public class PersonDtoEntityConverter implements DtoEntityConverter<PersonDto, Person>{

  private final PersonSkillsService personSkillsService;
  private final PersonAkdnTrainingService personAkdnTrainingService;
  private final PersonEducationService personEducationService;

  public PersonDtoEntityConverter(
    PersonSkillsService personSkillsService,
    PersonAkdnTrainingService personAkdnTrainingService,
    PersonEducationService personEducationService) {
    this.personSkillsService = personSkillsService;
    this.personAkdnTrainingService = personAkdnTrainingService;
    this.personEducationService = personEducationService;
  }

  @Override
  public PersonDto convertTo(Person entity) {
    return null;
//    return PersonDto.builder()
//      .akdnTraining(entity.getAkdnTraining().stream().map(personAkdnTrainingService::findOne).collect(Collectors.toList()))
//      .areaOfOrigin(entity.getAreaOfOrigin())
//      .city(entity.getCity())
//      .cnic(entity.getCnic())
//      .dateOfBirth(entity.getDateOfBirth())
//      .educationDetails(entity.getEducationDetails())
//      .email(entity.getEmail())
//      .familyName(entity.getFamilyName())
//      .fatherName(entity.getFatherName())
//      .fieldOfExpertise(entity.getFieldOfExpertise())
//      .firstName(entity.getFirstName())
//      .gender(entity.getGender())
//      .highestLevelOfStudy(entity.getHighestLevelOfStudy())
//      .highestLevelOfStudyOthers(entity.getHighestLevelOfStudyOthers())
//      .id(entity.getId())
//      .imagePath(entity.getImagePath())
//      .jamatiTitle(entity.getJamatiTitle())
//      .jamatkhanaId(entity.getJamatkhanaId())
//      .localCouncilId(entity.getLocalCouncilId())
//      .maritalStatus(entity.getMaritalStatus())
//      .mobilePhone(entity.getMobilePhone())
//      .passportNumber(entity.getPassportNumber())
//      .professionalTraining(entity.getProfessionalTraining())
//      .regionalCouncilId(entity.getRegionalCouncilId())
//      .religiousEducation(entity.getReligiousEducation())
//      .relocation(entity.getRelocation())
//      .relocationDateTime(entity.getRelocationDateTime())
//      .residenceTelephone(entity.getResidenceTelephone())
//      .residentialAddress(entity.getResidentialAddress())
//      .salutation(entity.getSalutation())
//      .skills(entity.getSkills())
//      .build();
  }

  @Override
  public Person convertFrom(PersonDto dto) {
    return null;
  }
}

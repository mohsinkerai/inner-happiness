package com.inner.satisfaction.backend.person;

import com.fasterxml.jackson.annotation.JsonAutoDetect;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.inner.satisfaction.backend.base.BaseEntity;
import com.inner.satisfaction.backend.config.JpaConverterJson;
import com.inner.satisfaction.backend.person.dto.PersonAkdnTrainingDto;
import com.inner.satisfaction.backend.person.dto.PersonEducationDto;
import com.inner.satisfaction.backend.person.dto.PersonProfessionalTrainingDto;
import com.inner.satisfaction.backend.person.dto.PersonSkillsDto;
import java.sql.Timestamp;
import java.util.List;
import javax.persistence.Convert;
import javax.persistence.Entity;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Entity
@Builder
@NoArgsConstructor
@AllArgsConstructor
@JsonAutoDetect
@JsonIgnoreProperties(ignoreUnknown = true)
public class Person extends BaseEntity {

  private String cnic;
  private String passportNumber;
  private String imagePath;

  private long salutation; // id
  private String firstName;
  private String fatherName;
  private String familyName;

  private long jamatiTitle; // id
  // 0 male, 1 female
  private int gender;
  private Timestamp dateOfBirth;
  private String residentialAddress;
  private long city; // id
  private String residenceTelephone;
  private String mobilePhone;
  private String email;

  private long maritalStatus; //id
  private long areaOfOrigin; // id

  // LevelID To be Exact
  private long regionalCouncil;
  private long localCouncil;
  private long jamatkhana;
  private String relocation;
  private Timestamp relocationDateTime;

  private String highestLevelOfStudy;
  private String highestLevelOfStudyOthers;

  // 2.Education - Graduation & Post Graduation
  @Convert(converter = JpaConverterJson.class)
  private List<PersonEducationDto> educationDetails;

  // 2.Education - AKDN Training
  @Convert(converter = JpaConverterJson.class)
  private List<PersonAkdnTrainingDto> akdnTrainings;

  // 2.Education - Professional Training & Acheivements
  @Convert(converter = JpaConverterJson.class)
  private List<PersonProfessionalTrainingDto> professionalTrainings;

  // 2.Education - Professional Training & Acheivements
  @Convert(converter = JpaConverterJson.class)
  private List<String> skills;

  // 2.Education - Professional Training & Acheivements
  @Convert(converter = JpaConverterJson.class)
  private List<String> professionalMemberships;


}

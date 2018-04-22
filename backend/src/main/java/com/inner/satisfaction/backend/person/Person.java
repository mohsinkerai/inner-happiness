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

  private String salutation;
  private String firstName;
  private String fatherName;
  private String familyName;

  private String jamatiTitle;
  // 0 male, 1 female
  private int gender;
  private Timestamp dateOfBirth;
  private String residentialAddress;
  private String city;
  private String residenceTelephone;
  private String mobilePhone;
  private String email;

  private String maritalStatus;
  private String areaOfOrigin;

  // LevelID To be Exact
  private long regionalCouncilId;
  private long localCouncilId;
  private long jamatkhanaId;
  private String relocation;
  private Timestamp relocationDateTime;

  private String highestLevelOfStudy;
  private String highestLevelOfStudyOthers;

  @Convert(converter = JpaConverterJson.class)
  private List<PersonEducationDto> educationDetails;
  @Convert(converter = JpaConverterJson.class)
  private List<PersonAkdnTrainingDto> akdnTraining;
  @Convert(converter = JpaConverterJson.class)
  private List<PersonProfessionalTrainingDto> professionalTraining;
  @Convert(converter = JpaConverterJson.class)
  private List<String> skills;
//  private List<Long> professionalMembership;
//  private List<Long> personLanguage;
//  private List<Long> voluntaryCommunityService;

//  private String fieldOfExpertise;
//  private String religiousEducation;
}

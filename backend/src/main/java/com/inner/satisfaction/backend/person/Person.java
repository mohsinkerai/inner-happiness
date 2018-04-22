package com.inner.satisfaction.backend.person;

import com.fasterxml.jackson.annotation.JsonAutoDetect;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.inner.satisfaction.backend.base.BaseEntity;
import com.inner.satisfaction.backend.config.JpaConverterJson;
import com.inner.satisfaction.backend.person.akdntraining.PersonAkdnTraining;
import com.inner.satisfaction.backend.person.education.PersonEducation;
import com.inner.satisfaction.backend.person.professionaltraining.PersonProfessionalTraining;
import com.inner.satisfaction.backend.person.skills.PersonSkills;
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

  private long salutation;
  private String firstName;
  private String fatherName;
  private String familyName;

  // 1 male, 2 female
  private String jamatiTitle;
  private int gender;
  private Timestamp dateOfBirth;
  private String residentialAddress;
  private String city;
  private String residenceTelephone;
  private String mobilePhone;
  private String email;

  private long maritalStatus;
  private long areaOfOrigin;

  // LevelID To be Exact
  private long regionalCouncilId;
  private long localCouncilId;
  private long jamatkhanaId;
  private String relocation;
  private Timestamp relocationDateTime;

  private String highestLevelOfStudy;
  private String highestLevelOfStudyOthers;

  @Convert(converter = JpaConverterJson.class)
  private List<PersonEducation> educationDetails;
//  private List<PersonAkdnTraining> akdnTraining;
//  private List<PersonProfessionalTraining> professionalTraining;
//  private List<PersonSkills> skills;
//  private List<Long> professionalMembership;
//  private List<Long> personLanguage;
//  private List<Long> voluntaryCommunityService;

  private String fieldOfExpertise;
  private String religiousEducation;
}

package com.inner.satisfaction.backend.person;

import com.inner.satisfaction.backend.base.BaseDto;
import com.inner.satisfaction.backend.person.akdntraining.PersonAkdnTraining;
import com.inner.satisfaction.backend.person.education.PersonEducation;
import com.inner.satisfaction.backend.person.professionaltraining.PersonProfessionalTraining;
import com.inner.satisfaction.backend.person.skills.PersonSkills;
import java.sql.Timestamp;
import java.util.List;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@AllArgsConstructor
@NoArgsConstructor
public class PersonDto extends BaseDto{

  private long id;
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

  private List<PersonEducation> educationDetails;
  private List<PersonAkdnTraining> akdnTraining;
  private List<PersonProfessionalTraining> professionalTraining;
  private List<PersonSkills> skills;
//  private List<Long> professionalMembership;
//  private List<Long> personLanguage;
//  private List<Long> voluntaryCommunityService;

  private String fieldOfExpertise;
  private String religiousEducation;
}

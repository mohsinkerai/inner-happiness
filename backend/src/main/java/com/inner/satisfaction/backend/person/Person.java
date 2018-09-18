package com.inner.satisfaction.backend.person;

import com.fasterxml.jackson.annotation.JsonAutoDetect;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.inner.satisfaction.backend.base.BaseEntity;
import com.inner.satisfaction.backend.config.JpaConverterJson;
import com.inner.satisfaction.backend.person.lookup.dto.EmploymentHistoryDto;
import com.inner.satisfaction.backend.person.lookup.dto.LanguageDto;
import com.inner.satisfaction.backend.person.lookup.dto.PersonAkdnTrainingDto;
import com.inner.satisfaction.backend.person.lookup.dto.PersonEducationDto;
import com.inner.satisfaction.backend.person.lookup.dto.PersonProfessionalTrainingDto;
import com.inner.satisfaction.backend.person.lookup.dto.ReducedPersonDto;
import com.inner.satisfaction.backend.person.lookup.dto.VoluntaryCommunityServiceDto;
import com.inner.satisfaction.backend.person.lookup.dto.VoluntaryPublicServiceDto;
import java.time.LocalDate;
import java.util.List;
import javax.persistence.Convert;
import javax.persistence.Entity;
import javax.persistence.Transient;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Entity
@Builder
@JsonAutoDetect
@NoArgsConstructor
@AllArgsConstructor
@JsonIgnoreProperties(ignoreUnknown = true)
public class Person extends BaseEntity {

  private String cnic;
  private String oldCnic;
  private String passportNumber;
  private String image;

  private Long salutation; // id, Lookup
  private String firstName;
  private String fathersName;
  private String familyName;

  private Long jamatiTitle; // id Lookup

  // 0 male, 1 female
  private Integer gender;

  // Should Represent UTC
  private LocalDate dateOfBirth;
  private Long maritalStatus; //id Lookup
  private String residentialAddress;
  private Long city; // id Lookup
  private String residenceTelephone;
  private String mobilePhone;
  private String emailAddress;

  private Long areaOfOrigin; // id Lookup

  // LevelID To be Exact
  private Long regionalCouncil; // id Lookup
  private Long localCouncil; // id Lookup
  private Long jamatkhana; // id Lookup
  private Boolean planToRelocate;
  private String relocateLocation;
  // Should Represent UTC
  private LocalDate relocationDateTime;

  private Long highestLevelOfStudy; // id Lookup
  private String highestLevelOfStudyOther;

  // 2.Education - Graduation & Post Graduation
  @Convert(converter = JpaConverterJson.class)
  private List<PersonEducationDto> educations;

  // 2.Education - AKDN Training
  @Convert(converter = JpaConverterJson.class)
  private List<PersonAkdnTrainingDto> akdnTrainings;

  // 2.Education - Professional Training & Acheivements
  @Convert(converter = JpaConverterJson.class)
  private List<PersonProfessionalTrainingDto> professionalTrainings;

  // 2.Education - Professional Training & Acheivements
//  @Convert(converter = JpaConverterJson.class)
  @Transient
  private List<String> skills;

  // 2.Education - Professional Training & Acheivements
//  @Convert(converter = JpaConverterJson.class)
  @Transient
  private List<String> professionalMemberships;

  @Transient
  private List<String> fieldOfExpertise;

  // 2.Education - Professional Training & Acheivements
  @Convert(converter = JpaConverterJson.class)
  private List<LanguageDto> languageProficiencies;

  // 3.1. Voluntary Community Service
  @Convert(converter = JpaConverterJson.class)
  private List<VoluntaryCommunityServiceDto> voluntaryCommunityServices;

  // 3.2. Voluntary Public Service
  @Convert(converter = JpaConverterJson.class)
  private List<VoluntaryPublicServiceDto> voluntaryPublicServices;

  private String willingnessToDevoteTimeInFuture;

  @Convert(converter = JpaConverterJson.class)
  private List<Integer> fieldOfInterest; // ID's

  private Long religiousEducation; // Id Lookup

  private Long hoursPerWeek;

  // 6. Occupation
  private Long occupationType; // Id Lookup
  private String occupationOthers;

  // EmploymentHistory
  @Convert(converter = JpaConverterJson.class)
  private List<EmploymentHistoryDto> employments;

  @Transient
  private List<ReducedPersonDto> familyRelations;
}

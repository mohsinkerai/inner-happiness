package com.inner.satisfaction.backend.person.education;

import com.fasterxml.jackson.annotation.JsonAutoDetect;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.inner.satisfaction.backend.base.BaseDto;
import javax.annotation.security.DenyAll;
import javax.persistence.Entity;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@JsonAutoDetect
@AllArgsConstructor
@NoArgsConstructor
@JsonIgnoreProperties(ignoreUnknown = true)
public class PersonEducation extends BaseDto{

  private long educationalInstitutionId;
  private long countryOfStudyId;
  private long nameOfDegree;
  private int fromYear;
  private int toYear;
  private String majorAreaOfStudy;
  private long personId;
}

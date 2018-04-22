package com.inner.satisfaction.backend.person.skills;

import com.fasterxml.jackson.annotation.JsonAutoDetect;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import javax.persistence.Entity;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Entity
@Builder
@JsonAutoDetect
@AllArgsConstructor
@NoArgsConstructor
@JsonIgnoreProperties(ignoreUnknown = true)
public class PersonSkills {

  private long educationalInstitutionId;
  private long countryOfStudyId;
  private long nameOfDegree;
  private int fromYear;
  private int toYear;
  private String majorAreaOfStudy;
  private long personId;
}

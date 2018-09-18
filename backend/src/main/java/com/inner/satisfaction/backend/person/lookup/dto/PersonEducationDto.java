package com.inner.satisfaction.backend.person.lookup.dto;

import com.fasterxml.jackson.annotation.JsonAutoDetect;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.inner.satisfaction.backend.base.BaseDto;
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
public class PersonEducationDto extends BaseDto {

  private String educationId; // Ignore for now
  private Long institution;// Id Lookup
  private Long countryOfStudy;// Id Lookup
  private Long nameOfDegree;// Id lookup
  private Integer fromYear;
  private Integer toYear;
  private Long majorAreaOfStudy; // Id lookup
  private int priority;
}

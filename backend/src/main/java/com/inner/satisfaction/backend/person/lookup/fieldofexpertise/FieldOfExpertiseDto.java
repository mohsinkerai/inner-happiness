package com.inner.satisfaction.backend.person.lookup.fieldofexpertise;

import com.fasterxml.jackson.annotation.JsonAutoDetect;
import com.fasterxml.jackson.annotation.JsonIgnore;
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
public class FieldOfExpertiseDto extends BaseDto {

  @JsonIgnore
  private long fieldOfExpertiseId;
  @JsonIgnore
  private long personId;

  private String fieldOfExpertise;
}

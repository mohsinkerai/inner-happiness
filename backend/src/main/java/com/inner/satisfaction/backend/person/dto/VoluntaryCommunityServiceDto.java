package com.inner.satisfaction.backend.person.dto;

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
public class VoluntaryCommunityServiceDto extends BaseDto{

  private long institution;// Id
  private long position;// Id
  private int fromYear;
  private int toYear;
}

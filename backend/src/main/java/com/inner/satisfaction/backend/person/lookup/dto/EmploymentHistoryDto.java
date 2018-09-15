package com.inner.satisfaction.backend.person.lookup.dto;

import com.fasterxml.jackson.annotation.JsonAutoDetect;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.databind.annotation.JsonSerialize;
import com.fasterxml.jackson.datatype.jsr310.ser.LocalDateSerializer;
import com.inner.satisfaction.backend.base.BaseDto;
import java.time.LocalDate;
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
public class EmploymentHistoryDto extends BaseDto {

  private String employmentId;
  private String nameOfOrganization;
  private String designation;
  private String location;
  private String employmentEmailAddress;
  private String employmentTelephone;
  private long businessType; // ID
  private long businessNature; // ID

  @JsonSerialize(using = LocalDateSerializer.class)
  private LocalDate employmentStartDate;
  @JsonSerialize(using = LocalDateSerializer.class)
  private LocalDate employmentEndDate;
}

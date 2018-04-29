package com.inner.satisfaction.backend.person.dto;

import com.fasterxml.jackson.annotation.JsonAutoDetect;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.inner.satisfaction.backend.base.BaseDto;
import java.sql.Timestamp;
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

  private String nameOfOrganization;
  private String designation;
  private String location;
  private String employmentEmailAddress;
  private String employmentTelephone;
  private long businessType; // ID
  private long businessNature; // ID
  private Timestamp employmentStartDate;
  private Timestamp employmentEndDate;
}

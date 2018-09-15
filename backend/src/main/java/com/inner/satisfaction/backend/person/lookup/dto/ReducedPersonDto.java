package com.inner.satisfaction.backend.person.lookup.dto;

import com.fasterxml.jackson.annotation.JsonAutoDetect;
import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
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
public class ReducedPersonDto extends BaseDto {

  public Long id;
  public String cnic;
  public LocalDate dateOfBirth;
  public long salutation;
  public String firstName;
  public String fathersName;
  public String familyName;
  public Long jamatiTitle;
  public long relation;
  @JsonIgnore
  public long personId;
}

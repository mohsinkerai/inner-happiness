package com.inner.satisfaction.backend.person.appointment;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.inner.satisfaction.backend.person.Person;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Builder
@NoArgsConstructor
@AllArgsConstructor
@Data
@JsonIgnoreProperties(ignoreUnknown = true)
public class PersonAppointmentDto {

  private Person person;
  private boolean isAppointed;
  private boolean isRecommended;
  private int priority;
  private String remarks;
}

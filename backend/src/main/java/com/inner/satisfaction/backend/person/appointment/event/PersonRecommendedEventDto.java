package com.inner.satisfaction.backend.person.appointment.event;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@AllArgsConstructor
@NoArgsConstructor
@Data
@Builder
@JsonIgnoreProperties(ignoreUnknown = true)
public class PersonRecommendedEventDto {

  private long personAppointmentId;
  private long appointmentPositionId;
  private long personId;
  private int previousRecommendationCount;
}

package com.inner.satisfaction.backend.person.appointment.dto;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.inner.satisfaction.backend.cycle.Cycle;
import com.inner.satisfaction.backend.institution.Institution;
import com.inner.satisfaction.backend.person.Person;
import com.inner.satisfaction.backend.position.Position;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Builder(toBuilder = true)
@NoArgsConstructor
@AllArgsConstructor
@Data
@JsonIgnoreProperties(ignoreUnknown = true)
public class PersonAppointmentExtendedDto {

  private Person person;

  private Long personId;
  private Long appointmentPositionId;
  private Long personAppointmentId;

  private boolean isAppointed;
  private boolean isRecommended;
  private int priority;
  private String remarks;

  private long cycleId;
  private long institutionId;
  private long positionId;
  private long seatId;

  private Position position;
  private Institution institution;
  private Cycle cycle;
}

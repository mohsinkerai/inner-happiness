package com.inner.satisfaction.backend.appointment;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.inner.satisfaction.backend.base.BaseDto;
import com.inner.satisfaction.backend.cycle.Cycle;
import com.inner.satisfaction.backend.institution.Institution;
import com.inner.satisfaction.backend.person.appointment.PersonAppointmentDto;
import com.inner.satisfaction.backend.position.Position;
import java.util.List;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Builder
@NoArgsConstructor
@AllArgsConstructor
@Data
@JsonIgnoreProperties(ignoreUnknown = true)
public class ApptPositionDto extends BaseDto {

  private long appointmentPositionId;
  private long seatId;
  private int nominationsRequired;
  private boolean isMowlaAppointee;

  private Cycle cycle;
  private Institution institution;
  private Position position;
  private List<PersonAppointmentDto> personAppointmentList;
}

package com.inner.satisfaction.backend.appointment;

import com.fasterxml.jackson.annotation.JsonAutoDetect;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.inner.satisfaction.backend.base.BaseDto;
import com.inner.satisfaction.backend.cycle.Cycle;
import com.inner.satisfaction.backend.institution.Institution;
import com.inner.satisfaction.backend.position.Position;
import javax.persistence.Entity;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@AllArgsConstructor
@NoArgsConstructor
@JsonAutoDetect
@JsonIgnoreProperties(ignoreUnknown = true)
public class AppointmentPositionDto extends BaseDto {

  private Long id;
  private Position position;
  private Institution institution;
  private long seatNo;
  private Cycle cycleId;
  private int nominationsRequired;
  private boolean isMowlaAppointee;
  private boolean isActive;
}

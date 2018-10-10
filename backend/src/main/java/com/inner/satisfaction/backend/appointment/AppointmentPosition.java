package com.inner.satisfaction.backend.appointment;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Entity
@Builder
@NoArgsConstructor
@AllArgsConstructor
@JsonIgnoreProperties(ignoreUnknown = true)
public class AppointmentPosition extends BaseEntity{

  private long positionId;
  private long institutionId;
  private long seatNo;
  private long cycleId;
  private int nominationsRequired;
  private boolean isMowlaAppointee;
  private boolean isActive;
  private Integer rank;
}

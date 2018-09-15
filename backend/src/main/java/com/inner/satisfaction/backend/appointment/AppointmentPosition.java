package com.inner.satisfaction.backend.appointment;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class AppointmentPosition extends BaseEntity{

  private long positionId;
  private long institutionId;
  private long seatNo;
  private long cycleId;
  private int nominationsRequired;
  private boolean isMowlaAppointee;
  private boolean isActive;
}

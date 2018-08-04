package com.inner.satisfaction.backend.position.active;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
/**
 * Represents All Relation that are available on Particular Active Level
 */
public class PositionOnInstitution extends BaseEntity {

  private Long positionId;
  private Long institutionId;

  private int minCount;
  private int desired;
  private int maxCount;
  private int nominations;

  private boolean isMowlaAppointee;
  private boolean isActive;
}

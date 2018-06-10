package com.inner.satisfaction.backend.cycle.position;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class CyclePositionOnInstitution extends BaseEntity {

  private int minCount;
  private int desired;
  private int maxCount;
  private int nominations;

  private Long cycleId;
  private Long positionOnInstitutionId;
}

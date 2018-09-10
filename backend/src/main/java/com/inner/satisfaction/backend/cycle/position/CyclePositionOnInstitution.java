package com.inner.satisfaction.backend.cycle.position;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class CyclePositionOnInstitution extends BaseEntity {

  private Integer minCount;
  private Integer desired;
  private Integer maxCount;
  private Integer nominations;

  private Long cycleId;
  private Long positionOnInstitutionId;
}

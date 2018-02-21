package com.inner.satisfaction.backend.position.active;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
/**
 * Represents All Person that are available on Particular Active Level
 */
public class PositionOnActiveLevel extends BaseEntity {

  private Long positionId;
  private Long levelActiveId;

  private boolean isActive;
}

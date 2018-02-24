package com.inner.satisfaction.backend.level.active;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class LevelActive extends BaseEntity{

  private Long levelId;
  private Long levelActiveId;
}

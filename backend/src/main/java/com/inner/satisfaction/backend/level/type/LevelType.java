package com.inner.satisfaction.backend.level.type;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class LevelType extends BaseEntity{

  private String name;
  private Long parentLevelTypeId;
}

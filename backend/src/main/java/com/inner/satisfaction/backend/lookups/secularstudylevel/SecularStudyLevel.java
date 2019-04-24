package com.inner.satisfaction.backend.lookups.secularstudylevel;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class SecularStudyLevel extends BaseEntity{

  private String name;
  private String shortCode;
}

package com.inner.satisfaction.backend.lookups.highestLevelStudy;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class HighestLevelOfStudy extends BaseEntity{

  private String name;
}

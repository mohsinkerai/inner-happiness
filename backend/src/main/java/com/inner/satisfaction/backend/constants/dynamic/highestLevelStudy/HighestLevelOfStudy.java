package com.inner.satisfaction.backend.constants.dynamic.highestLevelStudy;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class HighestLevelOfStudy extends BaseEntity{

  private String name;
}

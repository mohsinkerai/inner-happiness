package com.inner.satisfaction.backend.lookups.fieldofinterest;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class FieldOfInterest extends BaseEntity{

  private String name;
  private String shortCode;
}

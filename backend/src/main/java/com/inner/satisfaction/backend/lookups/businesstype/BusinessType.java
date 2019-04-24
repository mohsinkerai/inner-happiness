package com.inner.satisfaction.backend.lookups.businesstype;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class BusinessType extends BaseEntity{

  private String name;
  private String shortCode;
}

package com.inner.satisfaction.backend.lookups.businessnature;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class BusinessNature extends BaseEntity{

  private String name;
}

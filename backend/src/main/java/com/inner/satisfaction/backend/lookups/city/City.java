package com.inner.satisfaction.backend.lookups.city;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class City extends BaseEntity{

  private String name;
  private Long countryId;
  private String shortCode;
}

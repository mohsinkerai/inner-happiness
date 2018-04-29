package com.inner.satisfaction.backend.lookups.country;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class Country extends BaseEntity{

  private String name;
}

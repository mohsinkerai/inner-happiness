package com.inner.satisfaction.backend.lookups.salutation;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class Salutation extends BaseEntity{

  private String name;
}

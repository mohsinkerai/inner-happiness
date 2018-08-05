package com.inner.satisfaction.backend.person.cpi;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity(name = "person_cpi")
public class PersonCPI extends BaseEntity {

  private long personId;
  private long cpiId;
  private boolean isAppointed;
  private int priority;
}

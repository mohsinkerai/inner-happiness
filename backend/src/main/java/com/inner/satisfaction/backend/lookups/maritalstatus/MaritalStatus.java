package com.inner.satisfaction.backend.lookups.maritalstatus;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class MaritalStatus extends BaseEntity{

  private String name;
}

package com.inner.satisfaction.backend.lookups.institution;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class Institution extends BaseEntity{

  private String name;
}

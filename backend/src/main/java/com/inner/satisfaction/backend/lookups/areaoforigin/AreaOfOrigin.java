package com.inner.satisfaction.backend.lookups.areaoforigin;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class AreaOfOrigin extends BaseEntity{

  private String name;
}

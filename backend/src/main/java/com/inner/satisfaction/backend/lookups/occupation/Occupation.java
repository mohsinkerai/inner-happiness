package com.inner.satisfaction.backend.lookups.occupation;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class Occupation extends BaseEntity{

  private String name;
}

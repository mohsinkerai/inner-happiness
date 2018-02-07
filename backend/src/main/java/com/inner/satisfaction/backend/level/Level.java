package com.inner.satisfaction.backend.level;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class Level extends BaseEntity{

  private String name;
  private String fullName;
  private String shortCode;
  private String address;

  private int levelTypeId;
}

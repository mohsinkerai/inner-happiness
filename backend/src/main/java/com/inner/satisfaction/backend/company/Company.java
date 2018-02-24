package com.inner.satisfaction.backend.company;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class Company extends BaseEntity{

  private String name;
  private String fullName;
  private String shortCode;
  private String address;

  private int levelTypeId;
}

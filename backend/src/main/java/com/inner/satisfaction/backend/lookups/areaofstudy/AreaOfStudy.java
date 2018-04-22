package com.inner.satisfaction.backend.lookups.areaofstudy;

import com.inner.satisfaction.backend.base.BaseEntity;
import lombok.Data;

import javax.persistence.Entity;

@Data
@Entity
public class AreaOfStudy extends BaseEntity{

  private String name;
}

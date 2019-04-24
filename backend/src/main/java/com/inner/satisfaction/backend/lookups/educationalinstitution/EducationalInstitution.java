package com.inner.satisfaction.backend.lookups.educationalinstitution;

import com.inner.satisfaction.backend.base.BaseEntity;
import lombok.Data;

import javax.persistence.Entity;

@Data
@Entity
public class EducationalInstitution extends BaseEntity{

  private String name;
  private String shortCode;
}

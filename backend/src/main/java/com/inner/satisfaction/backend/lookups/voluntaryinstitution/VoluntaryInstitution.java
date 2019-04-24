package com.inner.satisfaction.backend.lookups.voluntaryinstitution;

import com.inner.satisfaction.backend.base.BaseEntity;
import lombok.Data;

import javax.persistence.Entity;

@Data
@Entity
public class VoluntaryInstitution extends BaseEntity{

  private String name;
  private String shortCode;
}

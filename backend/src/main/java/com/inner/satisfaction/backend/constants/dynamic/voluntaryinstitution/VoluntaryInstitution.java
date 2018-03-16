package com.inner.satisfaction.backend.constants.dynamic.voluntaryinstitution;

import com.inner.satisfaction.backend.base.BaseEntity;
import lombok.Data;

import javax.persistence.Entity;

@Data
@Entity
public class VoluntaryInstitution extends BaseEntity{

  private String name;
}

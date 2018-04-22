package com.inner.satisfaction.backend.lookups.religiousqualification;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class ReligiousQualification extends BaseEntity{

  private String name;
}

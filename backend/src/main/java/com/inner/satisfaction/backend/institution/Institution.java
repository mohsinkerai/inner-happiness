package com.inner.satisfaction.backend.institution;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class Institution extends BaseEntity{

  private String name;
  private String fullName;
  private long levelId;
  private String reportingName;

  private String category;
}

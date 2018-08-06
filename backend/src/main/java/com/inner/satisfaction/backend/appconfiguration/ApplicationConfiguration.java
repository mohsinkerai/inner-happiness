package com.inner.satisfaction.backend.appconfiguration;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class ApplicationConfiguration extends BaseEntity{

  private String key;
  private String value;
}

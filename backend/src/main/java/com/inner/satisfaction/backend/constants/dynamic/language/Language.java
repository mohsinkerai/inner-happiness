package com.inner.satisfaction.backend.constants.dynamic.language;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class Language extends BaseEntity{

  private String name;
}

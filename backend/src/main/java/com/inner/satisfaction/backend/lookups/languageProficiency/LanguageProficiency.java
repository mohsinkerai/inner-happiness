package com.inner.satisfaction.backend.lookups.languageProficiency;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class LanguageProficiency extends BaseEntity{

  private String name;
}

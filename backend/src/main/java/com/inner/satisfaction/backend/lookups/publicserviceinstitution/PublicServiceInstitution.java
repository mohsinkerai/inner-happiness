package com.inner.satisfaction.backend.lookups.publicserviceinstitution;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
// Public Service PublicServiceInstitution
public class PublicServiceInstitution extends BaseEntity{

  private String name;
}

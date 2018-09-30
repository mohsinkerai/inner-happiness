package com.inner.satisfaction.backend.lookups.professionalmembership;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class ProfessionalMembership extends BaseEntity{

  private String name;
}

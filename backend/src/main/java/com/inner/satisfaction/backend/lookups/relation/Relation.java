package com.inner.satisfaction.backend.lookups.relation;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity(name = "relation")
public class Relation extends BaseEntity{

  private String name;
  private Long reverseRelationId;
}

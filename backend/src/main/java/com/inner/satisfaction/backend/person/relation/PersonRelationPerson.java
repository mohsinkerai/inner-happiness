package com.inner.satisfaction.backend.person.relation;

import com.inner.satisfaction.backend.base.BaseEntity;
import com.inner.satisfaction.backend.person.Relation;
import java.sql.Timestamp;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.EnumType;
import javax.persistence.Enumerated;
import lombok.Data;

@Data
@Entity
public class PersonRelationPerson extends BaseEntity{

  private Integer firstPersonId;
  private Integer secondPersonId;
  @Enumerated
  @Column(name = "relation_id", columnDefinition = "int")
  private Relation relation;
}

package com.inner.satisfaction.backend.person.lookup.relation;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Column;
import javax.persistence.Entity;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Entity
public class PersonRelationPerson extends BaseEntity{

  private long firstPersonId;
  private long secondPersonId;
  @Column(name = "relation_id", columnDefinition = "bigint")
  private long relation;
}

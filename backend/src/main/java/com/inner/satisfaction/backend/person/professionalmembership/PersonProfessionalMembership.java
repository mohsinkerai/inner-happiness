package com.inner.satisfaction.backend.person.professionalmembership;

import com.inner.satisfaction.backend.base.BaseEntity;
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
public class PersonProfessionalMembership extends BaseEntity {

  private long personId;
  private long professionalMembershipId;
}

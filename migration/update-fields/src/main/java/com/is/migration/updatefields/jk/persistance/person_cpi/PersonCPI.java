package com.is.migration.updatefields.jk.persistance.person_cpi;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import lombok.Data;

@Data
@Entity(name="person_cpi")
public class PersonCPI {

  @Id
  @GeneratedValue(strategy = GenerationType.IDENTITY)
  private Long id;
  private Long personId;
  private Long cpiId;
  private boolean isRecommended;
  private Long priority;
  private Boolean isAppointed;
}

package com.inner.satisfaction.backend.lookups.jamatititle;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class JamatiTitle extends BaseEntity{

  private String name;
  /**
   * applicable on which geneder, male or female
   */
  private String gender;
}

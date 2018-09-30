package com.inner.satisfaction.backend.cycle;

import com.inner.satisfaction.backend.base.BaseEntity;
import java.sql.Timestamp;
import javax.persistence.Entity;
import lombok.Data;

/**
 * Represents a cycle of appointment i.e 2015-2019
 */
@Data
@Entity
public class Cycle extends BaseEntity {

  private String name;
  // Expects a year
  private Timestamp startDate;
  private Timestamp endDate;
}

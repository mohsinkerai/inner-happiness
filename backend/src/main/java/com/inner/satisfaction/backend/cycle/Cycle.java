package com.inner.satisfaction.backend.cycle;

import com.inner.satisfaction.backend.base.BaseEntity;
import java.sql.Timestamp;
import javax.persistence.Entity;
import lombok.Data;

/**
 * Represents a cycle of appointment i.e 2015-2019
 *
 * There exist two types of cycles, normal cylce or midterm cycle.
 *
 * Midterm cycle is rare and are conditioanlly created.
 */
@Data
@Entity
public class Cycle extends BaseEntity {

  private String name;

  // Expects a year
  private Timestamp startDate;
  private Timestamp endDate;

  private boolean isMidtermCycle;

  private Long nominatedCount;
  private Long recommendedCount;

  // If Normal Cycle
  private Long previousCycle;

  // If mid-term cycle, this would be populated
  private Long parentCycle;

  // Cycle State OPENED, APPOINTED, MIDTERM, CLOSE
  // OPENED -> APPOINTED -> ClOSE
  // OPENED -> APPOINTED -> MIDTERM -> APPOINTED -> ClOSE
  private String state;
}

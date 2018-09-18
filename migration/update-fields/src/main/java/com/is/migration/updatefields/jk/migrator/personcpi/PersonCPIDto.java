package com.is.migration.updatefields.jk.migrator.personcpi;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Builder
@Data
@AllArgsConstructor
@NoArgsConstructor
public class PersonCPIDto {

  private Long cycleId;
  private Long institutionId;
  private Long positionId;
  private Long personId;
  private Long incumbentId;
  private Long recommended;
  private long preference;
  private long seatId;
}

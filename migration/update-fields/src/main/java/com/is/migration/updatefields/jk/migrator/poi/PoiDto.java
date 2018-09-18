package com.is.migration.updatefields.jk.migrator.poi;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Builder
@Data
@AllArgsConstructor
@NoArgsConstructor
public class PoiDto {

  private Long cycleId;
  private Long institutionId;
  private Long positionId;
  private Long desired;
  private Long min;
  private Long max;
}

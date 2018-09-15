package com.is.migration.updatefields.jk.migrator.position;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Builder
@Data
@AllArgsConstructor
@NoArgsConstructor
public class PositionDto {

  private String name;
  private Long oldId;
}

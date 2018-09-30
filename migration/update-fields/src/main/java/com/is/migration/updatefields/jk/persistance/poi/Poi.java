package com.is.migration.updatefields.jk.persistance.poi;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Entity(name="position_on_institution")
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class Poi {

  @Id
  @GeneratedValue(strategy = GenerationType.IDENTITY)
  private Long id;
  private Long cycleId;
  private Long positionId;
  private Long institutionId;
  @Column(name = "min_count")
  private Long min;
  @Column(name = "max_count")
  private Long max;
  private Long desired;
}

package com.is.migration.updatefields.jk.persistance.cpoi;

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
@Entity(name="cycle_position_on_institution")
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class CPoi {

  @Id
  @GeneratedValue(strategy = GenerationType.IDENTITY)
  private Long id;
  private Long cycleId;
  @Column(name = "position_on_institution_id")
  private Long poiId;
  @Column(name = "min_count")
  private Long min;
  @Column(name = "max_count")
  private Long max;
  private Long desired;
}

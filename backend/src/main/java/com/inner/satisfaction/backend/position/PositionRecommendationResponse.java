package com.inner.satisfaction.backend.position;

import com.inner.satisfaction.backend.cycle.Cycle;
import com.inner.satisfaction.backend.institution.Institution;
import java.util.List;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@AllArgsConstructor
@NoArgsConstructor
public class PositionRecommendationResponse {

  /**
   * It will contain all the positiotns in that institution along with its incumbtee, and all the persons nominated
   * If no one is nominated, list would be empty.
   *
   * Even if there is no entry of position in that specified cycle, it would be empty
   */
  private List<PositionDetailsDto> positionDetailsDto;
  private Institution institution;
  private Cycle currentCycle;
}

package com.inner.satisfaction.backend.cycle;

import java.util.Optional;
import org.springframework.stereotype.Component;

@Component
public class CycleFacade {

  private final CycleService cycleService;

  public CycleFacade(CycleService cycleService) {
    this.cycleService = cycleService;
  }

  public CycleSummaryDto getCycleSummary(long cycleId) {
    Cycle one = cycleService.findOne(cycleId);
    return CycleSummaryDto.builder()
      .nominatedPerson(Optional.ofNullable(one.getNominatedCount()).orElse(0l))
      .recommendedPerson(Optional.ofNullable(one.getRecommendedCount()).orElse(0l))
      .build();
  }
}

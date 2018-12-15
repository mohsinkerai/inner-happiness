package com.inner.satisfaction.backend.cycle;

import static com.inner.satisfaction.backend.cycle.CycleState.APPOINTED;
import static com.inner.satisfaction.backend.cycle.CycleState.MIDTERM;
import static com.inner.satisfaction.backend.cycle.CycleState.OPENED;

import java.util.Optional;
import org.springframework.stereotype.Component;
import org.springframework.util.Assert;

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

  public void openMidtermAppointment(long cycleId) {
    Cycle cycle = cycleService.findOne(cycleId);
    if (cycle.getState() == null) {
      cycle.setState(CycleState.OPENED);
    }
    if (cycle.getState() == APPOINTED) {
      cycle.setState(MIDTERM);
    } else {
      throw new RuntimeException("Lala, Midterm nahi khul sakta, state invalid error");
    }
    cycleService.save(cycle);
  }

  public void appointInCycle(long cycleId) {
    Cycle cycle = cycleService.findOne(cycleId);
    Assert.notNull(cycle, "Invalid Cycle Id Provided");
    if (cycle.getState() == null) {
      cycle.setState(CycleState.OPENED);
      cycleService.save(cycle);
    }
    if (cycle.getState() == OPENED || cycle.getState() == MIDTERM) {

      // Mark PersonAppointed as Appointed
      // Mark Mark AppointmentPosition as Appointed

      // Change State of Cycle
      cycle.setState(CycleState.APPOINTED);
    } else {
      throw new RuntimeException("Lala, Midterm nahi khul sakta, state invalid error");
    }
    cycleService.save(cycle);
  }
}

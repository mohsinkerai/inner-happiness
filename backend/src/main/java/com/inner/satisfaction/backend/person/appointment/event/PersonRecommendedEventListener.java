package com.inner.satisfaction.backend.person.appointment.event;

import com.inner.satisfaction.backend.appointment.AppointmentPosition;
import com.inner.satisfaction.backend.appointment.AppointmentPositionService;
import com.inner.satisfaction.backend.cycle.Cycle;
import com.inner.satisfaction.backend.cycle.CycleService;
import org.springframework.stereotype.Component;
import org.springframework.transaction.event.TransactionPhase;
import org.springframework.transaction.event.TransactionalEventListener;

/**
 * Updates count per cycle wise.
 */
@Component
public class PersonRecommendedEventListener {

  private final CycleService cycleService;
  private final AppointmentPositionService appointmentPositionService;

  public PersonRecommendedEventListener(
    CycleService cycleService,
    AppointmentPositionService appointmentPositionService) {
    this.cycleService = cycleService;
    this.appointmentPositionService = appointmentPositionService;
  }

  @TransactionalEventListener(phase = TransactionPhase.BEFORE_COMMIT)
  public void listen(PersonRecommendedEventDto personRecommendedEventDto) {
    if (personRecommendedEventDto.getPreviousRecommendationCount() == 1) {
      return;
    }

    long appointmentPositionId = personRecommendedEventDto.getAppointmentPositionId();

    // We are sure that it will always exists.
    AppointmentPosition appointmentPosition = appointmentPositionService
      .findOne(appointmentPositionId);
    long cycleId = appointmentPosition.getCycleId();
    Cycle cycle = cycleService.findOne(cycleId);

    cycle.setRecommendedCount(
      cycle.getRecommendedCount() + 1 - personRecommendedEventDto.getPreviousRecommendationCount());
    cycleService.save(cycle);
  }
}
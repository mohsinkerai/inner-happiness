package com.inner.satisfaction.backend.person.appointment.event;

import com.inner.satisfaction.backend.appointment.dto.AppointmentPositionDeletedEventDto;
import com.inner.satisfaction.backend.person.appointment.PersonAppointmentService;
import org.springframework.context.event.EventListener;
import org.springframework.stereotype.Component;
import org.springframework.transaction.event.TransactionPhase;
import org.springframework.transaction.event.TransactionalEventListener;

@Component
public class AppointmentPositionDeletedEventListener {

  private final PersonAppointmentService personAppointmentService;

  public AppointmentPositionDeletedEventListener(
    PersonAppointmentService personAppointmentService) {
    this.personAppointmentService = personAppointmentService;
  }

  @TransactionalEventListener(phase = TransactionPhase.BEFORE_COMMIT)
  public void deleteAppointmentPosition(AppointmentPositionDeletedEventDto dto) {
    personAppointmentService
      .deleteAllPersonAppointmentForAppointmentPosition(dto.getAppointmentPositionId());
  }
}

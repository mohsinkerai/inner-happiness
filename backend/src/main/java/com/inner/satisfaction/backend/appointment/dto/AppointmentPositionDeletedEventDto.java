package com.inner.satisfaction.backend.appointment.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@AllArgsConstructor
@NoArgsConstructor
@Data
@Builder(toBuilder = true)
public class AppointmentPositionDeletedEventDto {

  private long appointmentPositionId;
}

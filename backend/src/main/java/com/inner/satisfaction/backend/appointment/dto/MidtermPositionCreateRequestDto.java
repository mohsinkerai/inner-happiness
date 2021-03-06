package com.inner.satisfaction.backend.appointment.dto;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import java.sql.Timestamp;
import java.util.List;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Builder
@Data
@NoArgsConstructor
@AllArgsConstructor
@JsonIgnoreProperties(ignoreUnknown = true)
public class MidtermPositionCreateRequestDto {

  private long cycleId;
  private List<Long> appointmentPositionIds;
  private Timestamp midtermPositionStartdate;
}

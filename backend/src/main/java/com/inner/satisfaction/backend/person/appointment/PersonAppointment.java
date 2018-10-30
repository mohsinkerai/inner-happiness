package com.inner.satisfaction.backend.person.appointment;

import com.fasterxml.jackson.annotation.JsonProperty;
import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Column;
import javax.persistence.Entity;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Entity(name = "person_appointment")
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class PersonAppointment extends BaseEntity {

  private Long personId;
  private Long appointmentPositionId;
  private Boolean isAppointed;
  private Boolean isRecommended;
  private Integer priority;
  private String remarks;

  @Column(updatable = false)
  private int reappointmentCount;

  public boolean getAppointed() {
    return isAppointed;
  }

  public boolean getRecommended() {
    return isRecommended;
  }
}

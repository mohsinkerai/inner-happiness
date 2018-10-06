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

  private long personId;
  private long appointmentPositionId;

  @Column(name = "is_appointed")
  @JsonProperty("isAppointed")
  private boolean isAppointed;

  @Column(name = "is_recommended")
  @JsonProperty("isRecommended")
  private boolean isRecommended;

  private int priority;
  private String remarks;

  public boolean getAppointed() {
    return isAppointed;
  }

  public boolean getRecommended() {
    return isRecommended;
  }
}

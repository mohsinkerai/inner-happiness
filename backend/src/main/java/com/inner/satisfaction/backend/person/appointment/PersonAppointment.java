package com.inner.satisfaction.backend.person.appointment;

import com.inner.satisfaction.backend.base.BaseEntity;
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
  private boolean isAppointed;
  private boolean isRecommended;
  private int priority;
  private String remarks;
}

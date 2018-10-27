package com.inner.satisfaction.backend.person.appointment.validation.message;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Entity
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class ValidationMessage extends BaseEntity {

  private String code;
  private String message;
  private Long personId;
  private Long cycleId;
  private Long appointmentPositionId;

  private boolean isRequired;
  private boolean isActive;
  private boolean isResolved;
}

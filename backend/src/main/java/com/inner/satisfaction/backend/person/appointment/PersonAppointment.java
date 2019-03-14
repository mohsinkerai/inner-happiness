package com.inner.satisfaction.backend.person.appointment;

import com.inner.satisfaction.backend.base.BaseEntity;
import com.inner.satisfaction.backend.company.Company;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
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
  private Integer reappointmentCount;

  /**
   * I know its redundant but I have to .. Forgive me please... :-)
   */
  @ManyToOne
  @JoinColumn(name = "company_id")
  private Company company;

  public boolean getAppointed() {
    return isAppointed;
  }

  public boolean getRecommended() {
    return isRecommended;
  }
}

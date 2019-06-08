package com.inner.satisfaction.backend.appointment;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.inner.satisfaction.backend.base.BaseEntity;
import com.inner.satisfaction.backend.company.Company;
import java.sql.Timestamp;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Entity
@Builder
@NoArgsConstructor
@AllArgsConstructor
@JsonIgnoreProperties(ignoreUnknown = true)
public class AppointmentPosition extends BaseEntity{

  private long positionId;
  private long institutionId;
  private long seatNo;
  private long cycleId;
  private int nominationsRequired;
  private boolean isMowlaAppointee;
  private boolean isActive;
  private Integer rank;

  @Column(name="from_time")
  private Timestamp from;
  @Column(name="to_time")
  private Timestamp to;

  // There exist 3 State
  // CREATED, APPOINTED, RETIRED
  // It is one way Created -> Appointed -> Retired
  private String state;
  private String positionType;
  private Long exOfficioInstitutionId;
  private boolean portfolioMember;

  @ManyToOne
  @JoinColumn(name = "company_id")
  private Company company;
}

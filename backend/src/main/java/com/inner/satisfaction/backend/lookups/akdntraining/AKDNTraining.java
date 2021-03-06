package com.inner.satisfaction.backend.lookups.akdntraining;

import com.inner.satisfaction.backend.base.BaseEntity;
import lombok.Data;

import javax.persistence.Entity;

@Data
@Entity(name = "akdn_training")
public class AKDNTraining extends BaseEntity{

  private String name;
  private String shortCode;
}

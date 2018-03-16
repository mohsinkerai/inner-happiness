package com.inner.satisfaction.backend.constants.dynamic.akdntraining;

import com.inner.satisfaction.backend.base.BaseEntity;
import lombok.Data;

import javax.persistence.Entity;

@Data
@Entity
public class AKDNTraining extends BaseEntity{

  private String name;
}

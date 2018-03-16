package com.inner.satisfaction.backend.detailConstants.model;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Entity
@Data
public class Salutation extends BaseEntity {

  private String salutation;
}

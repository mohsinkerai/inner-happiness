package com.inner.satisfaction.backend.lookups.fieldofexperties;

import com.inner.satisfaction.backend.base.BaseEntity;
import lombok.Data;

import javax.persistence.Entity;

@Data
@Entity
/**
 * This should be called skill (Refactoring needed)
 */
public class FieldOfExpertise extends BaseEntity{

  private String name;
  private String shortCode;
}

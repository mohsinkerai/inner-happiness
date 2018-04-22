package com.inner.satisfaction.backend.constants.dynamic.fieldofexperties;

import com.inner.satisfaction.backend.base.BaseEntity;
import lombok.Data;

import javax.persistence.Entity;

@Data
@Entity
/**
 * This should be called skills (Refactoring needed)
 */
public class FieldOfExpertise extends BaseEntity{

  private String name;
}

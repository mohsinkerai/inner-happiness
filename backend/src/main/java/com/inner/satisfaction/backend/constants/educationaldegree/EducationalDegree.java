package com.inner.satisfaction.backend.constants.educationaldegree;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class EducationalDegree extends BaseEntity{

  private String name;
}

package com.inner.satisfaction.backend.position;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class Position extends BaseEntity{

  private String name;
}

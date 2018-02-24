package com.inner.satisfaction.backend.base;

import java.io.Serializable;
import javax.persistence.Id;
import javax.persistence.MappedSuperclass;
import lombok.Data;
import lombok.Generated;

@Data
@MappedSuperclass
public abstract class BaseEntity implements Serializable{

  @Id
  @Generated
  protected Long id;
}

package com.inner.satisfaction.backend.person;

import com.inner.satisfaction.backend.base.BaseEntity;
import java.sql.Timestamp;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class Person extends BaseEntity{

  private String name;
  private String email;
  private Timestamp dateOfBirth;
}

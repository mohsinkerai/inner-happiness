package com.is.migration.updatefields.jk.persistance.jamatkhana;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import lombok.Data;

@Data
@Entity(name="level")
public class JamatkhanaEntity {

  @Id
  @GeneratedValue(strategy = GenerationType.IDENTITY)
  private Long id;
  private Long levelTypeId;
  private String name;
  private String fullName;
  private String codeEo;
  private String codeNc;
  private Long levelParentId;
  private Long oldId;
  private String oldCode;
  private boolean isClosed;
}

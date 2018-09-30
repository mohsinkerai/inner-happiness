package com.inner.satisfaction.backend.level;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Entity
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class Level extends BaseEntity {

  private String name;
  private String fullName;
  private String codeEo;
  private String codeNc;
  private boolean isClosed;

  private int levelTypeId;

  private Long levelParentId;

  public Level(Level level) {
    this.name = level.name;
    this.fullName = level.fullName;
    this.codeEo = level.codeEo;
    this.codeNc = level.codeNc;
    this.isClosed = level.isClosed;
    this.levelTypeId = level.levelTypeId;
    this.levelParentId = level.levelParentId;
  }
}

package com.inner.satisfaction.backend.lookups.skill;

import com.inner.satisfaction.backend.base.BaseEntity;
import javax.persistence.Entity;
import lombok.Data;

@Data
@Entity
public class Skill extends BaseEntity{

  private String name;
}
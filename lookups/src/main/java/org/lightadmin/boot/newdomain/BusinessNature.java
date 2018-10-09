package org.lightadmin.boot.newdomain;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

@Entity
public class BusinessNature {

  private static final long serialVersionUID = 1L;

  @Id
  @GeneratedValue
  private Long id;

  @Column
  private String name;

  public BusinessNature() {
  }

  public BusinessNature(String name) {
    this.name = name;
  }

  @Override
  public String toString() {
    return String.format(
        "BusinessNature[id=%d, name='%s'']",
        id, name);
  }

  public Long getId() {
    return id;
  }

  public String getName() {
    return name;
  }
}
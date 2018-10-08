package org.lightadmin.boot.newdomain;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

@Entity
public class BusinessType {

  private static final long serialVersionUID = 1L;

  @Id
  @GeneratedValue
  private Long id;

  @Column
  private String name;

  public BusinessType() {
  }

  public BusinessType(String name) {
    this.name = name;
  }

  @Override
  public String toString() {
    return String.format(
        "BusinessType[id=%d, name='%s'']",
        id, name);
  }

  public Long getId() {
    return id;
  }

  public String getName() {
    return name;
  }
}
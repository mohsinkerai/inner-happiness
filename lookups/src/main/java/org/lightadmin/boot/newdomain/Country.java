package org.lightadmin.boot.newdomain;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

@Entity
public class Country {

  private static final long serialVersionUID = 1L;

  @Id
  @GeneratedValue
  private Long id;

  @Column
  private String name;

  public Country() {
  }

  public Country(String name) {
    this.name = name;
  }

  @Override
  public String toString() {
    return String.format(
        "Country[id=%d, name='%s'']",
        id, name);
  }

  public Long getId() {
    return id;
  }

  public String getName() {
    return name;
  }
}
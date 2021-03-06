package org.lightadmin.boot.newdomain;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

@Entity
public class City {

  private static final long serialVersionUID = 1L;

  @Id
  @GeneratedValue
  private Long id;

  @Column
  private String name;

  @Column
  private Long countryId;

  public City() {
  }

  public City(String name, Long countryId) {
    this.name = name;
    this.countryId = countryId;
  }

  @Override
  public String toString() {
    return String.format(
        "CityAdministration[id=%d, name='%s'']",
        id, name);
  }

  public Long getId() {
    return id;
  }

  public String getName() {
    return name;
  }

  public Long getCountryId() {
    return countryId;
  }
}
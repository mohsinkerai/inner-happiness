package org.lightadmin.boot.newdomain;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

@Entity
public class JamatiTitle {

  private static final long serialVersionUID = 1L;

  @Id
  @GeneratedValue
  private Long id;

  @Column
  private String name;

  @Column
  private String gender;

  public JamatiTitle() {
  }

  public JamatiTitle(String name, String gender) {
    this.name = name;
    this.gender = gender;
  }

  @Override
  public String toString() {
    return String.format(
        "JamatiTitle[id=%d, name='%s'']",
        id, name);
  }

  public Long getId() {
    return id;
  }

  public String getName() {
    return name;
  }

  public String getGender() {
    return gender;
  }
}
package org.lightadmin.boot.newdomain;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

@Entity
public class Relation {

  private static final long serialVersionUID = 1L;

  @Id
  @GeneratedValue
  private Long id;

  @Column
  private String name;

  @Column
  private Long reverseRelationId;

  public Relation() {
  }

  public Relation(String name, Long reverseRelationId) {
    this.name = name;
    this.reverseRelationId = reverseRelationId;
  }

  @Override
  public String toString() {
    return String.format(
        "Relation[id=%d, name='%s'']",
        id, name);
  }

  public Long getId() {
    return id;
  }

  public String getName() {
    return name;
  }

  public Long getReverseRelationId() {
    return reverseRelationId;
  }
}
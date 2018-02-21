package com.inner.satisfaction.backend.person;

import com.google.common.collect.ImmutableMap;
import com.google.common.collect.ImmutableMap.Builder;
import java.util.Map;

public enum Relation {

  SISTER(0),
  BROTHER(1),
  FATHER(2),
  DAUGHTER(3),
  MOTHER(4),
  SON(5);

  private final int id;
  private static Map<Integer, Relation> relationMap;

  static {
    Builder<Integer, Relation> relationMapBuilder = ImmutableMap.builder();
    for (Relation relation : Relation.values()){
      relationMapBuilder.put(relation.getId(), relation);
    }
    relationMap = relationMapBuilder.build();
  }

  Relation(int i) {
    id = i;
  }

  public int getId() {
    return this.id;
  }

  public static Relation fromId(int i) {
    return relationMap.get(i);
  }
}

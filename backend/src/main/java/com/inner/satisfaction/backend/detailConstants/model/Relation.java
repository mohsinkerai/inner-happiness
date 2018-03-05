package com.inner.satisfaction.backend.detailConstants.model;

import com.inner.satisfaction.backend.base.BaseEntity;
import lombok.Data;

import javax.persistence.Entity;

@Entity
@Data
public class Relation extends BaseEntity {
    String relation;
}

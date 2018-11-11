package com.inner.satisfaction.backend.base;

import java.io.Serializable;
import java.sql.Timestamp;
import javax.persistence.Column;
import javax.persistence.EntityListeners;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.MappedSuperclass;
import lombok.Data;
import org.springframework.data.annotation.CreatedBy;
import org.springframework.data.annotation.CreatedDate;
import org.springframework.data.annotation.LastModifiedBy;
import org.springframework.data.annotation.LastModifiedDate;
import org.springframework.data.jpa.domain.support.AuditingEntityListener;

@Data
@MappedSuperclass
@EntityListeners(AuditingEntityListener.class)
public abstract class BaseEntity implements Serializable {

  @Id
  @GeneratedValue
  protected Long id;

  protected boolean isActive;

  @CreatedBy
  protected String createdBy;

  @LastModifiedBy
  protected String updatedBy;

//  @CreatedDate
//  @Column(nullable = false, updatable = false)
//  private Timestamp createdOn;
//
//  @LastModifiedDate
//  @Column(nullable = false)
//  private Timestamp updatedOn;
}

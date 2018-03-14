package com.inner.satisfaction.backend.detailConstants.model;


import com.inner.satisfaction.backend.base.BaseEntity;
import lombok.Data;

import javax.persistence.Entity;

@Data
@Entity
public class MaritalStatus extends BaseEntity {

    String status;
}

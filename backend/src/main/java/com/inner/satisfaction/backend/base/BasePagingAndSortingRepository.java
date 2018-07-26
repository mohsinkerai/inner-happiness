package com.inner.satisfaction.backend.base;

import org.springframework.data.jpa.repository.JpaRepository;

public interface BasePagingAndSortingRepository<E extends BaseEntity> extends BaseRepository<E>,
  JpaRepository<E, Long> {

}

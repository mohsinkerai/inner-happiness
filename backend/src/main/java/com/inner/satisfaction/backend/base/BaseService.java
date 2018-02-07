package com.inner.satisfaction.backend.base;

import java.util.List;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;

public abstract class BaseService <E extends BaseEntity> {

  private final BaseRepository<E> baseRepository;
  private final BaseEntityValidation<E> baseEntityValidation;

  protected BaseService(BaseRepository<E> baseRepository,
      BaseEntityValidation<E> baseEntityValidation) {
    this.baseRepository = baseRepository;
    this.baseEntityValidation = baseEntityValidation;
  }

  public List<E> findAll() {
    return baseRepository.findAll();
  }

  public Page<E> findAll(Pageable pageable) {
    return baseRepository.findAll(pageable);
  }

  public E findOne(Long id) {
    return baseRepository.findOne(id);
  }

  public E save(E e) {
    baseEntityValidation.isValidToSave(e);
    return baseRepository.save(e);
  }
}

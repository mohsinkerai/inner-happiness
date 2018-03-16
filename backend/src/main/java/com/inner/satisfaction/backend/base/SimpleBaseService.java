package com.inner.satisfaction.backend.base;

import java.util.List;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;

public abstract class SimpleBaseService<E extends BaseEntity> {

  protected final BaseRepository<E> baseRepository;

  protected SimpleBaseService(BaseRepository<E> baseRepository) {
    this.baseRepository = baseRepository;
  }

  public List<E> findAll() {
    return baseRepository.findAll();
  }

  public Page<E> findAll(Pageable pageable) {
    return baseRepository.findAll(pageable);
  }

  public E findOne(Long id) {
    return baseRepository.getOne(id);
  }

  public E save(E e) {
    return baseRepository.save(e);
  }

  public void delete(E e) {
    baseRepository.delete(e);
  }
}

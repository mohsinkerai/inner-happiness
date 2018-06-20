package com.inner.satisfaction.backend.base;

import java.util.List;
import javax.persistence.EntityNotFoundException;
import lombok.extern.slf4j.Slf4j;
import org.springframework.data.domain.Example;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;

@Slf4j
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
    try {
      return baseRepository.findById(id).orElse(null);
    } catch (Exception ex) {
      log.info("Entity not found {}", id, ex);
      return null;
    }
  }

  public E save(E e) {
    try {
      return baseRepository.save(e);
    } catch (EntityNotFoundException ex) {
      log.error("Entity not found {}", e, ex);
      return null;
    }
  }

  public void delete(E e) {
    baseRepository.delete(e);
  }
}

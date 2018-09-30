package com.inner.satisfaction.backend.base;

import java.util.List;
import java.util.Optional;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;

public abstract class BaseService <E extends BaseEntity> extends SimpleBaseService<E>{

  protected final BaseEntityValidation<E> baseEntityValidation;

  protected BaseService(BaseRepository<E> baseRepository,
      BaseEntityValidation<E> baseEntityValidation) {
    super(baseRepository);
    this.baseEntityValidation = baseEntityValidation;
  }

  @Override
  public E save(E e) {
    baseEntityValidation.isValidToSave(e);
    return super.save(e);
  }

  @Override
  public void delete(E e) {
    baseEntityValidation.isValidToDelete(e);
    super.delete(e);
  }
}

package com.inner.satisfaction.backend.base;

import com.inner.satisfaction.backend.level.Level;
import java.util.List;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseStatus;

public abstract class BaseController<E extends BaseEntity> {

  public static final String PREFIX = "company/{companyId}/";

  private final BaseService<E> baseService;

  protected BaseController(BaseService<E> baseService) {
    this.baseService = baseService;
  }

  @RequestMapping("{id}")
  public ResponseEntity<E> findOne(
      @PathVariable("companyId") Long companyId,
      @PathVariable("id") Long entityId
  ) {
    E entity = baseService.findOne(entityId);
    return ResponseEntity.ok(entity);
  }

  @PostMapping("{id}")
  public ResponseEntity<E> putSave(
      @PathVariable("companyId") Long companyId,
      @PathVariable("id") Long entityId
  ) {
    E entity = baseService.findOne(entityId);
    return ResponseEntity.ok(entity);
  }

  @GetMapping("")
  @ResponseStatus(HttpStatus.CREATED)
  public List<E> findAll(
      @PathVariable("companyId") Long companyId
  ) {
    return baseService.findAll();
  }

  @PostMapping("")
  @ResponseStatus(HttpStatus.OK)
  public E save(Long companyId, E e) {
    return baseService.save(companyId, e);
  }

  @DeleteMapping("{id}")
  @ResponseStatus(HttpStatus.NO_CONTENT)
  public void delete(
      @PathVariable("companyId") Long companyId,
      @PathVariable("entityId") Long entityId
  ) {
    E e = baseService.findOne(entityId);
    baseService.delete(companyId, e);
  }
}

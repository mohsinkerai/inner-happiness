package com.inner.satisfaction.backend.base;

import com.inner.satisfaction.backend.level.Level;
import java.util.List;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseStatus;

public abstract class BaseController<E extends BaseEntity> {

  public static final String PREFIX = "";
  public static final String ROOT = "";
  public static final String ONE = ROOT + "one/{id}";
  public static final String ALL = ROOT + "all";

  private final BaseService<E> baseService;

  protected BaseController(BaseService<E> baseService) {
    this.baseService = baseService;
  }

  @GetMapping(ONE)
  public E findOne(
      @PathVariable("id") Long entityId
  ) {
    E entity = baseService.findOne(entityId);
    return entity;
  }

  @PostMapping(ONE)
  @ResponseStatus(HttpStatus.CREATED)
  public E putSave(
      @PathVariable("id")Long id,
      E e
  ) {
    e.setId(id);
    return save(e);
  }

  @GetMapping(value = ALL)
  @ResponseStatus(HttpStatus.OK)
  public List<E> findAll() {
    return baseService.findAll();
  }

  @PostMapping(ROOT)
  @ResponseStatus(HttpStatus.OK)
  public E save(@RequestBody E e) {
    return baseService.save(e);
  }

  @DeleteMapping(ONE)
  @ResponseStatus(HttpStatus.NO_CONTENT)
  public void delete(
      Long entityId
  ) {
    E e = baseService.findOne(entityId);
    baseService.delete(e);
  }
}

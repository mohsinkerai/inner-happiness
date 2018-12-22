package com.inner.satisfaction.backend.base;

import com.inner.satisfaction.backend.level.Level;
import java.util.List;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;

public abstract class BaseController<E extends BaseEntity> {

  public static final String CONSTANTS_PREFIX = "constants/";
  public static final String PREFIX = "";
  public static final String ROOT = "";
  public static final String ONE = ROOT + "one/{id}";
  public static final String ALL = ROOT + "all";
  public static final String PAGINATED = ROOT + "paginated";

  protected final SimpleBaseService<E> baseService;

  protected BaseController(SimpleBaseService<E> baseService) {
    this.baseService = baseService;
  }

  @GetMapping(ONE)
  public ResponseEntity<E> findOne(
    @PathVariable("id") Long entityId
  ) {
    E entity = baseService.findOne(entityId);
    if (entity == null) {
      return ResponseEntity.status(404).body(null);
    }
    return ResponseEntity.ok(entity);
  }

  @PutMapping(ONE)
  @ResponseStatus(HttpStatus.CREATED)
  public E putSave(
    @PathVariable("id") Long id,
    @RequestBody E e
  ) {
    e.setId(id);
    return baseService.save(e);
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

  @GetMapping(value = PAGINATED)
  @ResponseStatus(HttpStatus.OK)
  public Page<E> findAllPage(
    @RequestParam(value = "size", defaultValue = "10") int size,
    @RequestParam(value = "page", defaultValue = "0") int page) {
    PageRequest pageRequest = PageRequest.of(page - 1, size);
    return baseService.findAll(pageRequest);
  }
}

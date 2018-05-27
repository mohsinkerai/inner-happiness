package com.inner.satisfaction.backend.utils;

import com.inner.satisfaction.backend.base.BaseDto;
import com.inner.satisfaction.backend.base.BaseEntity;

public interface DtoEntityConverter<Dto extends BaseDto, Entity extends BaseEntity> {

  Dto convertTo(Entity entity);
  Entity convertFrom(Dto dto);
}

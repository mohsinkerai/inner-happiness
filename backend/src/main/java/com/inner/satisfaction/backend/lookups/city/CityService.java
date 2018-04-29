package com.inner.satisfaction.backend.lookups.city;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class CityService extends SimpleBaseService<City> {

  protected CityService(
      CityRepository baseRepository) {
    super(baseRepository);
  }
}

package com.inner.satisfaction.backend.lookups.country;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class CountryService extends SimpleBaseService<Country> {

  protected CountryService(
      CountryRepository baseRepository) {
    super(baseRepository);
  }
}

package com.inner.satisfaction.backend.lookups.country;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;
import static com.inner.satisfaction.backend.lookups.country.CountryController.PATH;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + PATH)
public class CountryController extends BaseController<Country> {

  public static final String PATH = "country";

  public CountryController(CountryService countryService) {
    super(countryService);
  }
}

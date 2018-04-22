package com.inner.satisfaction.backend.lookups.city;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;
import static com.inner.satisfaction.backend.lookups.city.CityController.PATH;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + PATH)
public class CityController extends BaseController<City> {

  public static final String PATH = "city";

  public CityController(CityService cityService) {
    super(cityService);
  }
}

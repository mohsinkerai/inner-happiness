package com.inner.satisfaction.backend.appconfiguration;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + "app-configuration")
public class ApplicationConfigurationController extends BaseController<ApplicationConfiguration> {

  private ApplicationConfigurationService applicationConfigurationService;

  public ApplicationConfigurationController(
    ApplicationConfigurationService applicationConfigurationService) {
    super(applicationConfigurationService);
    this.applicationConfigurationService = applicationConfigurationService;
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findByLevelId", method = RequestMethod.GET)
  public ApplicationConfiguration findByLevelId(
    @RequestParam("key") String key) {
    return applicationConfigurationService.findByKey(key);
  }
}

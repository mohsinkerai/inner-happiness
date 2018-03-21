package com.inner.satisfaction.backend.constants.dynamic.language;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;
import static com.inner.satisfaction.backend.constants.dynamic.language.LanguageController.PATH;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + PATH)
public class LanguageController extends BaseController<Language> {

  public static final String PATH = "language";

  public LanguageController(LanguageService languageService) {
    super(languageService);
  }
}

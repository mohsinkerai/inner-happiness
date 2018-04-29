package com.inner.satisfaction.backend.lookups.languageProficiency;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + LanguageProficiencyController.PATH)
public class LanguageProficiencyController extends BaseController<LanguageProficiency> {

  public static final String PATH = "language-proficiency";

  public LanguageProficiencyController(LanguageProficiencyService languageProficiencyService) {
    super(languageProficiencyService);
  }
}

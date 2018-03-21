package com.inner.satisfaction.backend.constants.dynamic.language;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class LanguageService extends SimpleBaseService<Language> {

  protected LanguageService(
      LanguageRepository baseRepository) {
    super(baseRepository);
  }
}

package com.inner.satisfaction.backend.lookups.languageProficiency;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class LanguageProficiencyService extends SimpleBaseService<LanguageProficiency> {

  protected LanguageProficiencyService(
      LanguageProficiencyRepository baseRepository) {
    super(baseRepository);
  }
}

package com.inner.satisfaction.backend.lookups.jamatititle;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class JamatiTitleService extends SimpleBaseService<JamatiTitle> {

  protected JamatiTitleService(
      JamatiTitleRepository baseRepository) {
    super(baseRepository);
  }
}

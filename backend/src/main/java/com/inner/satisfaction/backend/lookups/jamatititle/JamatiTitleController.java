package com.inner.satisfaction.backend.lookups.jamatititle;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + JamatiTitleController.PATH)
public class JamatiTitleController extends BaseController<JamatiTitle> {

  public static final String PATH = "jamati-title";

  public JamatiTitleController(JamatiTitleService jamatiTitleService) {
    super(jamatiTitleService);
  }
}

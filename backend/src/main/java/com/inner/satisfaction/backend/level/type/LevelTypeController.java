package com.inner.satisfaction.backend.level.type;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import com.inner.satisfaction.backend.base.BaseService;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
@RequestMapping(PREFIX + "level/type")
public class LevelTypeController extends BaseController<LevelType> {

  protected LevelTypeController(
      BaseService<LevelType> baseService) {
    super(baseService);
  }
}

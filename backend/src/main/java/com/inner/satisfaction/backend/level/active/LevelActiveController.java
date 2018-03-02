package com.inner.satisfaction.backend.level.active;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + "level/active")
public class LevelActiveController extends BaseController<LevelActive> {

  public LevelActiveController(LevelActiveService levelService) {
    super(levelService);
  }
}

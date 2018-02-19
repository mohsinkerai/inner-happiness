package com.inner.satisfaction.backend.position.active;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;
import static com.inner.satisfaction.backend.position.PositionController.PATH;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
@RequestMapping(PREFIX + PATH)
public class PositionOnActiveLevelController extends BaseController<PositionOnActiveLevel> {

  public static final String PATH = "position/level/active";

  public PositionOnActiveLevelController(PositionOnActiveLevelService positionOnActiveLevelService) {
    super(positionOnActiveLevelService);
  }
}

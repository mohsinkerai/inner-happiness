package com.inner.satisfaction.backend.position;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
@RequestMapping(PREFIX + PositionController.PATH)
public class PositionController extends BaseController<Position> {

  public static final String PATH = "position";

  public PositionController(PositionService positionService) {
    super(positionService);
  }
}

package com.inner.satisfaction.backend.cycle.position;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + CyclePositionOnActiveLevelController.PATH)
public class CyclePositionOnActiveLevelController extends BaseController<CyclePositionOnActiveLevel> {

  public static final String PATH = "cycle/position";

  public CyclePositionOnActiveLevelController(CyclePositionOnActiveLevelService cyclePositionOnActiveLevelService) {
    super(cyclePositionOnActiveLevelService);
  }
}

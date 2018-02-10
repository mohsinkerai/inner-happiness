package com.inner.satisfaction.backend.cycle;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
@RequestMapping(PREFIX + CycleController.PATH)
public class CycleController extends BaseController<Cycle> {

  public static final String PATH = "cycle";

  public CycleController(CycleService cycleService) {
    super(cycleService);
  }
}

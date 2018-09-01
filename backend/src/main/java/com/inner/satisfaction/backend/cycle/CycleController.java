package com.inner.satisfaction.backend.cycle;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + CycleController.PATH)
public class CycleController extends BaseController<Cycle> {

  public static final String PATH = "cycle";
  private final CycleService cycleService;


  public CycleController(CycleService cycleService) {
    super(cycleService);
    this.cycleService = cycleService;
  }

  @RequestMapping(method = RequestMethod.POST, value = "close")
  public void close() {
    cycleService.closeCycle();
  }

  @RequestMapping(method = RequestMethod.POST, value = "close")
  public void open(@RequestBody long cycleId) {
    cycleService.openCycle(cycleId);
  }
}

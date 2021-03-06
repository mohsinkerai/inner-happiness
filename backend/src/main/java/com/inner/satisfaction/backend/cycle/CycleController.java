package com.inner.satisfaction.backend.cycle;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + CycleController.PATH)
public class CycleController extends BaseController<Cycle> {

  public static final String PATH = "cycle";
  private final CycleService cycleService;
  private final CycleFacade cycleFacade;

  public CycleController(CycleService cycleService,
    CycleFacade cycleFacade) {
    super(cycleService);
    this.cycleService = cycleService;
    this.cycleFacade = cycleFacade;
  }

  @RequestMapping(method = RequestMethod.POST, value = "dismiss")
  public void dismiss(@RequestBody long cycleId) {
    cycleService.dismissCycle(cycleId);
  }

  @RequestMapping(method = RequestMethod.POST, value = "close")
  public void close(@RequestBody CycleCloseRequestDto cycleCloseRequestDto) {
    cycleFacade.closeCycle(cycleCloseRequestDto.getCycleId(), cycleCloseRequestDto.getEndingDate());
  }

  @RequestMapping(method = RequestMethod.POST, value = "create")
  public void open(@RequestBody CycleCreateRequestDto cycleRequestDto) {
    // Verify Opened cycle count (Should be 0)
    cycleService.openCycle(cycleRequestDto);
  }

  @RequestMapping(method = RequestMethod.POST, value = "appoint")
  public void appoint(@RequestBody long cycleId) {
    // Move this cycle to appointed state, appoint every member that is recommended, throw error if this cannot be done.
    // This can happen from either open state or from midterm state
    cycleFacade.appointInCycle(cycleId);
  }

  @RequestMapping(method = RequestMethod.POST, value = "midterm")
  public void midterm(@RequestBody long cycleId) {
    // Move this cycle to midterm state, if and only if cycle is in appointed state
    cycleFacade.openMidtermAppointment(cycleId);
  }

  @Override
  public Cycle save(Cycle cycle) {
    throw new RuntimeException(
      "Comeon !! You can't create cycle like this, use the proper way, that is cycle open api");
  }

  @Override
  public Cycle putSave(Long id, Cycle cycle) {
    throw new RuntimeException(
      "Comeon !! You can't create cycle like this, use the proper way, that is cycle open api");
  }

  @GetMapping("summary")
  public CycleSummaryDto cycleSummary(@RequestParam long cycleId) {
    return cycleFacade.getCycleSummary(cycleId);
  }
}

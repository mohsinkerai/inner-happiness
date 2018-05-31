package com.inner.satisfaction.backend.cycle.position;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + CyclePositionOnInstitutionController.PATH)
public class CyclePositionOnInstitutionController extends BaseController<CyclePositionOnInstitution> {

  public static final String PATH = "cycle/position";

  public CyclePositionOnInstitutionController(CyclePositionOnInstitutionService cyclePositionOnInstitutionService) {
    super(cyclePositionOnInstitutionService);
  }
}

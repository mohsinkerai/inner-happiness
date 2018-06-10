package com.inner.satisfaction.backend.position.active;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + PositionOnInstitutionController.PATH)
public class PositionOnInstitutionController extends BaseController<PositionOnInstitution> {

  public static final String PATH = "position/institution";

  public PositionOnInstitutionController(
      PositionOnInstitutionService positionOnInstitutionService) {
    super(positionOnInstitutionService);
  }
}

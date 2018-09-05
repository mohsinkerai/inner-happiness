package com.inner.satisfaction.backend.position;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import com.inner.satisfaction.backend.position.active.PositionOnInstitutionService;
import java.util.List;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + PositionController.PATH)
public class PositionController extends BaseController<Position> {

  public static final String PATH = "position";
  private final PositionService positionService;

  public PositionController(PositionService positionService) {
    super(positionService);
    this.positionService = positionService;
  }

  /**
   * Returns All PositionId at given institution Id with their names
   * It is needed when a institution is selected for action
   */
  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findByInstitutionId", method = RequestMethod.GET)
  public PositionRecommendationResponse findByInstitutionId(@RequestParam("institutionId") long institutionId) {
    return positionService.findByInstitutionId(institutionId);
  }
}

package com.inner.satisfaction.backend.position.active;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import java.util.List;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + PositionOnInstitutionController.PATH)
public class PositionOnInstitutionController extends BaseController<PositionOnInstitution> {

  public static final String PATH = "position/institution";
  private final PositionOnInstitutionService positionOnInstitutionService;

  public PositionOnInstitutionController(
    PositionOnInstitutionService positionOnInstitutionService) {
    super(positionOnInstitutionService);
    this.positionOnInstitutionService = positionOnInstitutionService;
  }

  /**
   * Returns All PositionId at given institution Id with their names
   * It is needed at summary page (Maybe)
   */
  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findByInstitutionId", method = RequestMethod.GET)
  public List<PositionOnInstitution> findByInstitutionId(
    @RequestParam("institutionId") long institutionId) {
    return positionOnInstitutionService.findByInstitutionId(institutionId);
  }
}

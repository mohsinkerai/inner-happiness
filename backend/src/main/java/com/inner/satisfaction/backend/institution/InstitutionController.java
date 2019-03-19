package com.inner.satisfaction.backend.institution;

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
@RequestMapping(PREFIX + "institution")
public class InstitutionController extends BaseController<Institution> {

  private InstitutionService institutionService;

  public InstitutionController(InstitutionService institutionService) {
    super(institutionService);
    this.institutionService = institutionService;
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findByLevelId", method = RequestMethod.GET)
  public List<Institution> findByLevelId(
    @RequestParam("levelId") Long levelId) {
    return institutionService.findByLevelId(levelId);
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findByLevelType", method = RequestMethod.GET)
  public List<Institution> findByLevelType(
    @RequestParam(required = false, value = "category") String category,
    @RequestParam("levelTypeId") Long levelTypeId) {
    return institutionService.findByLevelType(category, levelTypeId);
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findByInstitutionCategory", method = RequestMethod.GET)
  public List<Institution> findByCategory(
    @RequestParam("category") String category) {
    return institutionService.findByCategory(category);
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findByInstitutionCategoryAndParentLevelId", method = RequestMethod.GET)
  public List<Institution> findByCategoryAndParentLevelId(
    @RequestParam("category") String category,
    @RequestParam("parentLevelId") Long parentLevelId) {
    return institutionService.findByCategoryAndParentLevelId(category, parentLevelId);
  }

  @ResponseStatus(HttpStatus.OK)
  @RequestMapping(value = "/search/findByInstitutionCategoryAndLevelTypeId", method = RequestMethod.GET)
  public List<Institution> findByCategoryAndLevelTypeId(
    @RequestParam("category") String category,
    @RequestParam("levelTypeId") Long levelTypeId) {
    return institutionService.findByCategoryAndLevelTypeId(category, levelTypeId);
  }
}

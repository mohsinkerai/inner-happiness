package com.inner.satisfaction.backend.detailConstants;


import static org.springframework.web.bind.annotation.RequestMethod.GET;

import com.inner.satisfaction.backend.detailConstants.model.AreaOfOrigin;
import com.inner.satisfaction.backend.detailConstants.model.BussinessNature;
import com.inner.satisfaction.backend.detailConstants.model.BussinessType;
import com.inner.satisfaction.backend.detailConstants.model.City;
import com.inner.satisfaction.backend.detailConstants.model.Country;
import com.inner.satisfaction.backend.detailConstants.model.FieldOfInterest;
import com.inner.satisfaction.backend.detailConstants.model.Institution;
import com.inner.satisfaction.backend.detailConstants.model.JamatiTitles;
import com.inner.satisfaction.backend.detailConstants.model.MaritalStatus;
import com.inner.satisfaction.backend.detailConstants.model.Occupation;
import com.inner.satisfaction.backend.detailConstants.model.Relation;
import com.inner.satisfaction.backend.detailConstants.model.ReligiousQualification;
import com.inner.satisfaction.backend.detailConstants.model.Salutation;
import com.inner.satisfaction.backend.detailConstants.model.SecularStudyLevel;
import java.util.List;
import lombok.AllArgsConstructor;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(value = "/constants")
@AllArgsConstructor
public class ConstantsController {

  private ConstantsService constantsService;

  @RequestMapping(method = GET, value = "/jamatiTitles")
  public List<JamatiTitles> getJamatiTitles() {
    return constantsService.getAllJamatiTitles();
  }

  @RequestMapping(method = GET, value = "/maritalStatuses")
  public List<MaritalStatus> getAllMaritalStatuses() {
    return constantsService.getAllMaritalStatuses();
  }

  @RequestMapping(method = GET, value = "/areaOfOrigin")
  public List<AreaOfOrigin> getAllAreaOfOrigin() {
    return constantsService.getAllAreaOfOrigin();
  }


  @RequestMapping(method = GET, value = "/countries")
  public List<Country> getAllCountries() {
    return constantsService.getAllCountries();
  }

  @RequestMapping(method = GET, value = "/salutatuions")
  public List<Salutation> getAllSalutations() {
    return constantsService.getAllSalutations();
  }

  @RequestMapping(method = GET, value = "/secularLevels")
  public List<SecularStudyLevel> getAllSecularLevel() {
    return constantsService.getAllSecularLevel();
  }

  @RequestMapping(method = GET, value = "/religiousQualifications")
  public List<ReligiousQualification> getAllreligiousQualification() {
    return constantsService.getAllreligiousQualification();
  }

  @RequestMapping(method = GET, value = "/fieldOfInterests")
  public List<FieldOfInterest> getAllFieldOfInterest() {
    return constantsService.getAllFieldOfInterest();
  }

  @RequestMapping(method = GET, value = "/Occupations")
  public List<Occupation> getAllOccupation() {
    return constantsService.getAllOccupation();
  }

  @RequestMapping(method = GET, value = "/bussinessType")
  public List<BussinessType> getAllBussinessType() {
    return constantsService.getAllBussinessType();
  }

  @RequestMapping(method = GET, value = "/bussinessNature")
  public List<BussinessNature> getAllBussinessNature() {
    return constantsService.getAllBussinessNature();
  }

  @RequestMapping(method = GET, value = "/relations")
  public List<Relation> getAllRelation() {
    return constantsService.getAllRelation();
  }

  @RequestMapping(method = GET, value = "/institutions")
  public List<Institution> getAllInstitutions() {
    return constantsService.getAllInstitutions();
  }

  @RequestMapping(method = GET, value = "/cities")
  public List<City> getAllCity() {
    return constantsService.getAllCities();
  }
}

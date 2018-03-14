package com.inner.satisfaction.backend.detailConstants;

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
import com.inner.satisfaction.backend.detailConstants.repositories.AreaOfOriginRepository;
import com.inner.satisfaction.backend.detailConstants.repositories.BussinessNatureRepository;
import com.inner.satisfaction.backend.detailConstants.repositories.BussinessTypeRepository;
import com.inner.satisfaction.backend.detailConstants.repositories.CityRepository;
import com.inner.satisfaction.backend.detailConstants.repositories.CountryRepository;
import com.inner.satisfaction.backend.detailConstants.repositories.FieldOfInterestRepository;
import com.inner.satisfaction.backend.detailConstants.repositories.InstitutionRepository;
import com.inner.satisfaction.backend.detailConstants.repositories.JamatiTitlesRepository;
import com.inner.satisfaction.backend.detailConstants.repositories.MaritalStatusRepository;
import com.inner.satisfaction.backend.detailConstants.repositories.OccupationRepository;
import com.inner.satisfaction.backend.detailConstants.repositories.RelationRepository;
import com.inner.satisfaction.backend.detailConstants.repositories.ReligiousQualificationRepository;
import com.inner.satisfaction.backend.detailConstants.repositories.SalutationRepository;
import com.inner.satisfaction.backend.detailConstants.repositories.SecularStudyLevelRepository;
import java.util.List;
import lombok.AllArgsConstructor;
import org.springframework.stereotype.Service;

@Service
@AllArgsConstructor
public class ConstantsService {


  private final JamatiTitlesRepository jamatiTitlesRepository;
  private final MaritalStatusRepository maritalStatusRepository;
  private final AreaOfOriginRepository areaOfOriginRepository;
  private final CountryRepository countryRepository;
  private final SalutationRepository salutationRepository;
  private final SecularStudyLevelRepository secularStudyLevelRepository;
  private final ReligiousQualificationRepository religiousQualificationRepository;
  private final FieldOfInterestRepository fieldOfInterestRepository;
  private final OccupationRepository occupationRepository;
  private final BussinessTypeRepository bussinessTypeRepository;
  private final BussinessNatureRepository bussinessNatureRepository;
  private final RelationRepository relationRepository;
  private final InstitutionRepository institutionRepository;
  private final CityRepository cityRepository;

  public List<JamatiTitles> getAllJamatiTitles() {
    return jamatiTitlesRepository.findAll();
  }

  public List<MaritalStatus> getAllMaritalStatuses() {
    return maritalStatusRepository.findAll();
  }

  public List<AreaOfOrigin> getAllAreaOfOrigin() {
    return areaOfOriginRepository.findAll();
  }

  public List<Country> getAllCountries() {
    return countryRepository.findAll();
  }

  public List<Salutation> getAllSalutations() {
    return salutationRepository.findAll();
  }

  public List<SecularStudyLevel> getAllSecularLevel() {
    return secularStudyLevelRepository.findAll();
  }

  public List<ReligiousQualification> getAllreligiousQualification() {
    return religiousQualificationRepository.findAll();
  }

  public List<FieldOfInterest> getAllFieldOfInterest() {
    return fieldOfInterestRepository.findAll();
  }

  public List<Occupation> getAllOccupation() {
    return occupationRepository.findAll();
  }

  public List<BussinessType> getAllBussinessType() {
    return bussinessTypeRepository.findAll();
  }

  public List<BussinessNature> getAllBussinessNature() {
    return bussinessNatureRepository.findAll();
  }

  public List<Relation> getAllRelation() {
    return relationRepository.findAll();
  }

  public List<Institution> getAllInstitutions() {
    return institutionRepository.findAll();
  }

  public List<City> getAllCities() {
    return cityRepository.findAll();
  }
}
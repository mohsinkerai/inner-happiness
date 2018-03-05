package com.inner.satisfaction.backend.detailConstants;

import com.inner.satisfaction.backend.base.BaseRepository;
import com.inner.satisfaction.backend.detailConstants.model.*;
import com.inner.satisfaction.backend.detailConstants.repositories.*;
import lombok.AllArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@AllArgsConstructor
public class ConstantsService {


    private JamatiTitlesRepository jamatiTitlesRepository;
    private MaritalStatusRepository maritalStatusRepository;
    private AreaOfOriginRepository areaOfOriginRepository;
    private CountryRepository countryRepository;
    private SalutationRepository salutationRepository;
    private SecularStudyLevelRepository secularStudyLevelRepository;
    private ReligiousQualificationRepository religiousQualificationRepository;
    private FieldOfInterestRepository fieldOfInterestRepository;
    private OccupationRepository occupationRepository;
    private BussinessTypeRepository bussinessTypeRepository;
    private BussinessNatureRepository bussinessNatureRepository;
    private RelationRepository relationRepository;


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

    public List<BussinessNature> getAllBussinessNature(){
        return bussinessNatureRepository.findAll();
    }
    public List<Relation> getAllRelation(){
        return relationRepository.findAll();
    }
}
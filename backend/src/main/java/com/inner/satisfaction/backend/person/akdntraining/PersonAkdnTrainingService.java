package com.inner.satisfaction.backend.person.akdntraining;

import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.person.relation.PersonRelationPerson;
import org.springframework.stereotype.Service;

@Service
public class PersonAkdnTrainingService extends BaseService<PersonAkdnTraining>{

  protected PersonAkdnTrainingService(
      PersonAkdnTrainingRepository baseRepository,
      PersonAkdnTrainingValidation personAkdnTrainingValidation) {
    super(baseRepository, personAkdnTrainingValidation);
  }
}

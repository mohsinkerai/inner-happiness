package com.inner.satisfaction.backend.person.professionaltraining;

import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.person.relation.PersonRelationPerson;
import org.springframework.stereotype.Service;

@Service
public class PersonProfessionalTrainingService extends BaseService<PersonRelationPerson>{

  protected PersonProfessionalTrainingService(
      PersonProfessionalTrainingRepository baseRepository,
      PersonProfessionalTrainingValidation personProfessionalTrainingValidation) {
    super(baseRepository, personProfessionalTrainingValidation);
  }
}

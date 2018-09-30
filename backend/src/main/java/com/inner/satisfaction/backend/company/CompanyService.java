package com.inner.satisfaction.backend.company;

import com.inner.satisfaction.backend.base.BaseService;
import org.springframework.stereotype.Service;

@Service
public class CompanyService extends BaseService<Company>{

  protected CompanyService(
      CompanyRepository baseRepository,
      CompanyValidation companyValidation) {
    super(baseRepository, companyValidation);
  }
}

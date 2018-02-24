package com.inner.satisfaction.backend.company;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + "company")
public class CompanyController extends BaseController<Company> {

  public CompanyController(CompanyService companyService) {
    super(companyService);
  }
}

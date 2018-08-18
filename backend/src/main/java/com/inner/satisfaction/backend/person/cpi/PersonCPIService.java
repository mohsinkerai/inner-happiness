package com.inner.satisfaction.backend.person.cpi;

import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.base.SimpleBaseService;
import java.util.List;
import org.springframework.stereotype.Service;

@Service
public class PersonCPIService extends SimpleBaseService<PersonCPI> {

  private final PersonCPIRepository personCPIRepository;

  protected PersonCPIService(
      PersonCPIRepository baseRepository) {
    super(baseRepository);
    this.personCPIRepository = baseRepository;
  }

  public List<PersonCPI> findByCpiId(long cpiId) {
    return personCPIRepository.findByCpiId(cpiId);
  }

  public PersonCPI findByCpiIdAndIsAppointedTrue(long cpiId) {
    return personCPIRepository.findByCpiIdAndIsAppointedTrue(cpiId);
  }
}

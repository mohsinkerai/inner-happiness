package com.inner.satisfaction.backend.cycle;

import com.inner.satisfaction.backend.appconfiguration.ApplicationConfiguration;
import com.inner.satisfaction.backend.appconfiguration.ApplicationConfigurationKeys;
import com.inner.satisfaction.backend.appconfiguration.ApplicationConfigurationService;
import com.inner.satisfaction.backend.base.BaseService;
import javax.transaction.Transactional;
import org.springframework.stereotype.Service;
import org.springframework.util.Assert;

@Service
public class CycleService extends BaseService<Cycle> {

  private final ApplicationConfigurationService appConfigurationService;

  protected CycleService(
    ApplicationConfigurationService appConfigurationService,
    CycleRepository baseRepository,
    CycleValidation cycleValidation) {
    super(baseRepository, cycleValidation);
    this.appConfigurationService = appConfigurationService;
  }

  @Transactional
  public void closeCycle() {
    // Validation of Cycle
    ApplicationConfiguration currentCycle = appConfigurationService
      .findByKey(ApplicationConfigurationKeys.CURRENT_CYCLE_ID.name());
    if (currentCycle == null) {
      throw new RuntimeException("Cycle is Already Closed");
    }
    ApplicationConfiguration previousCycle = appConfigurationService
      .findByKey(ApplicationConfigurationKeys.PREVIOUS_CYCLE_ID.name());
    if (previousCycle == null) {
      throw new RuntimeException("Something is wrong in state, previous cycle should not be null");
    }
    previousCycle.setValue(currentCycle.getValue());
    appConfigurationService.save(previousCycle);
    appConfigurationService.delete(currentCycle);

    // POST Actions
  }

  @Transactional
  public void openCycle(long cycleId) {
    Cycle cycle = findOne(cycleId);
    Assert.notNull(cycle, "Cycle which needs to be open, should exist");
    ApplicationConfiguration currentCycle = appConfigurationService
      .findByKey(ApplicationConfigurationKeys.CURRENT_CYCLE_ID.name());
    Assert.isNull(currentCycle, "Current Cycle should not Exist");
    ApplicationConfiguration applicationConfiguration = ApplicationConfiguration.builder()
      .key(ApplicationConfigurationKeys.CURRENT_CYCLE_ID.name())
      .value(String.valueOf(cycleId))
      .build();
    appConfigurationService.save(applicationConfiguration);
  }
}

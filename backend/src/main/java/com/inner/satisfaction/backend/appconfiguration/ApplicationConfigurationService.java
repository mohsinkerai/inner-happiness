package com.inner.satisfaction.backend.appconfiguration;

import com.inner.satisfaction.backend.base.BaseService;
import com.inner.satisfaction.backend.base.SimpleBaseService;
import com.inner.satisfaction.backend.level.Level;
import com.inner.satisfaction.backend.level.LevelService;
import java.util.List;
import java.util.Set;
import java.util.stream.Collectors;
import org.springframework.stereotype.Service;

@Service
public class ApplicationConfigurationService extends SimpleBaseService<ApplicationConfiguration> {

  private final ApplicationConfigurationRepository repository;

  protected ApplicationConfigurationService(
    ApplicationConfigurationRepository baseRepository) {
    super(baseRepository);
    this.repository = baseRepository;
  }

  public ApplicationConfiguration findByKey(String key) {
    return repository.findByKey(key);
  }
}

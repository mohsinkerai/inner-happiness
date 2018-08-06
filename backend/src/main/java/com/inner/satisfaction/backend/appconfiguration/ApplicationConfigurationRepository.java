package com.inner.satisfaction.backend.appconfiguration;

import com.inner.satisfaction.backend.base.BaseRepository;
import java.util.List;
import org.springframework.stereotype.Repository;

@Repository
public interface ApplicationConfigurationRepository extends BaseRepository<ApplicationConfiguration> {

  ApplicationConfiguration findByKey(String key);
}

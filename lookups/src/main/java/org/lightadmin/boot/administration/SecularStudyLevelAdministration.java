package org.lightadmin.boot.administration;

import org.lightadmin.api.config.AdministrationConfiguration;
import org.lightadmin.api.config.builder.EntityMetadataConfigurationUnitBuilder;
import org.lightadmin.api.config.unit.EntityMetadataConfigurationUnit;
import org.lightadmin.boot.newdomain.City;
import org.lightadmin.boot.newdomain.SecularStudyLevel;

public class SecularStudyLevelAdministration extends AdministrationConfiguration<SecularStudyLevel> {

  @Override
  public EntityMetadataConfigurationUnit configuration(
      EntityMetadataConfigurationUnitBuilder configurationBuilder) {
    return configurationBuilder.nameField("name").singularName("Secular Study Level")
        .pluralName("Secular Study Level").build();
  }
}

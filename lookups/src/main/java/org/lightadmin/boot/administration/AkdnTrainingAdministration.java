package org.lightadmin.boot.administration;

import org.lightadmin.api.config.AdministrationConfiguration;
import org.lightadmin.api.config.builder.EntityMetadataConfigurationUnitBuilder;
import org.lightadmin.api.config.unit.EntityMetadataConfigurationUnit;
import org.lightadmin.boot.newdomain.AkdnTraining;
import org.lightadmin.boot.newdomain.AreaOfOrigin;

public class AkdnTrainingAdministration extends AdministrationConfiguration<AkdnTraining> {

  @Override
  public EntityMetadataConfigurationUnit configuration(
      EntityMetadataConfigurationUnitBuilder configurationBuilder) {
    return configurationBuilder.nameField("name").singularName("AKDN Training").pluralName("AKDN Trainings").build();
  }
}

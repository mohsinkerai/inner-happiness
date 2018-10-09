package org.lightadmin.boot.administration;

import org.lightadmin.api.config.AdministrationConfiguration;
import org.lightadmin.api.config.builder.EntityMetadataConfigurationUnitBuilder;
import org.lightadmin.api.config.unit.EntityMetadataConfigurationUnit;
import org.lightadmin.boot.newdomain.AreaOfOrigin;
import org.lightadmin.boot.newdomain.AreaOfStudy;

public class AreaOfStudyAdministration extends AdministrationConfiguration<AreaOfStudy> {

  @Override
  public EntityMetadataConfigurationUnit configuration(
      EntityMetadataConfigurationUnitBuilder configurationBuilder) {
    return configurationBuilder.nameField("name").singularName("Area of Study").pluralName("Areas of Study").build();
  }
}

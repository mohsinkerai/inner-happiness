package org.lightadmin.boot.administration;

import org.lightadmin.api.config.AdministrationConfiguration;
import org.lightadmin.api.config.builder.EntityMetadataConfigurationUnitBuilder;
import org.lightadmin.api.config.unit.EntityMetadataConfigurationUnit;
import org.lightadmin.boot.newdomain.Country;
import org.lightadmin.boot.newdomain.FieldOfInterest;

public class FieldOfInterestAdministration extends AdministrationConfiguration<FieldOfInterest> {

  @Override
  public EntityMetadataConfigurationUnit configuration(
      EntityMetadataConfigurationUnitBuilder configurationBuilder) {
    return configurationBuilder.nameField("name").singularName("Field Of Interest")
        .pluralName("Fields Of Interest").build();
  }
}

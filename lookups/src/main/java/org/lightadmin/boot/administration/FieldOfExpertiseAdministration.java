package org.lightadmin.boot.administration;

import org.lightadmin.api.config.AdministrationConfiguration;
import org.lightadmin.api.config.builder.EntityMetadataConfigurationUnitBuilder;
import org.lightadmin.api.config.unit.EntityMetadataConfigurationUnit;
import org.lightadmin.boot.newdomain.Country;
import org.lightadmin.boot.newdomain.FieldOfExpertise;

public class FieldOfExpertiseAdministration extends AdministrationConfiguration<FieldOfExpertise> {

  @Override
  public EntityMetadataConfigurationUnit configuration(
      EntityMetadataConfigurationUnitBuilder configurationBuilder) {
    return configurationBuilder.nameField("name").singularName("Field Of Expertise")
        .pluralName("Fields Of Expertise").build();
  }
}

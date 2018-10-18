package org.lightadmin.boot.administration;

import org.lightadmin.api.config.AdministrationConfiguration;
import org.lightadmin.api.config.builder.EntityMetadataConfigurationUnitBuilder;
import org.lightadmin.api.config.unit.EntityMetadataConfigurationUnit;
import org.lightadmin.boot.newdomain.City;
import org.lightadmin.boot.newdomain.Salutation;

public class SalutationAdministration extends AdministrationConfiguration<Salutation> {

  @Override
  public EntityMetadataConfigurationUnit configuration(
      EntityMetadataConfigurationUnitBuilder configurationBuilder) {
    return configurationBuilder.nameField("name").singularName("Salutation")
        .pluralName("Salutations").build();
  }
}

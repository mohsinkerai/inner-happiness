package org.lightadmin.boot.administration;

import org.lightadmin.api.config.AdministrationConfiguration;
import org.lightadmin.api.config.builder.EntityMetadataConfigurationUnitBuilder;
import org.lightadmin.api.config.unit.EntityMetadataConfigurationUnit;
import org.lightadmin.boot.newdomain.BusinessNature;
import org.lightadmin.boot.newdomain.Country;

public class CountryAdministration extends AdministrationConfiguration<Country> {

  @Override
  public EntityMetadataConfigurationUnit configuration(
      EntityMetadataConfigurationUnitBuilder configurationBuilder) {
    return configurationBuilder.nameField("name").singularName("Country")
        .pluralName("Countries").build();
  }
}

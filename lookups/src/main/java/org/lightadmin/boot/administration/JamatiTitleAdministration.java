package org.lightadmin.boot.administration;

import org.lightadmin.api.config.AdministrationConfiguration;
import org.lightadmin.api.config.builder.EntityMetadataConfigurationUnitBuilder;
import org.lightadmin.api.config.unit.EntityMetadataConfigurationUnit;
import org.lightadmin.boot.newdomain.Country;
import org.lightadmin.boot.newdomain.JamatiTitle;

public class JamatiTitleAdministration extends AdministrationConfiguration<JamatiTitle> {

  @Override
  public EntityMetadataConfigurationUnit configuration(
      EntityMetadataConfigurationUnitBuilder configurationBuilder) {
    return configurationBuilder.nameField("name").singularName("Jamati Title")
        .pluralName("Jamati Title").build();
  }
}

package org.lightadmin.boot.administration;

import org.lightadmin.api.config.AdministrationConfiguration;
import org.lightadmin.api.config.builder.EntityMetadataConfigurationUnitBuilder;
import org.lightadmin.api.config.unit.EntityMetadataConfigurationUnit;
import org.lightadmin.boot.newdomain.Country;
import org.lightadmin.boot.newdomain.EducationalInstitution;

public class EducationalInstitutionAdministration extends AdministrationConfiguration<EducationalInstitution> {

  @Override
  public EntityMetadataConfigurationUnit configuration(
      EntityMetadataConfigurationUnitBuilder configurationBuilder) {
    return configurationBuilder.nameField("name").singularName("Educational Institution")
        .pluralName("Educational Institutions").build();
  }
}

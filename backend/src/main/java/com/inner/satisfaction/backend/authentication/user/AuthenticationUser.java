package com.inner.satisfaction.backend.authentication.user;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.inner.satisfaction.backend.base.BaseEntity;
import com.inner.satisfaction.backend.config.JpaConverterJson;
import java.util.List;
import javax.persistence.Convert;
import javax.persistence.Entity;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Entity
@Builder
@NoArgsConstructor
@AllArgsConstructor
@JsonIgnoreProperties(ignoreUnknown = true)
public class AuthenticationUser extends BaseEntity {

  private String username;
  private String password;
  private boolean isActive;

  @Convert(converter = JpaConverterJson.class)
  private List<Long> allowedCompanies;
  @Convert(converter = JpaConverterJson.class)
  private List<String> roles;
}

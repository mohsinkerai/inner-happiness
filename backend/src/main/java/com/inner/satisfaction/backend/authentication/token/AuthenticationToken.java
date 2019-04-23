package com.inner.satisfaction.backend.authentication.token;

import com.inner.satisfaction.backend.base.BaseEntity;
import com.inner.satisfaction.backend.config.JpaConverterJson;
import java.sql.Timestamp;
import java.util.Collection;
import java.util.List;
import java.util.stream.Collectors;
import javax.persistence.Convert;
import javax.persistence.Entity;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.GrantedAuthority;

@Data
@Entity
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class AuthenticationToken extends BaseEntity implements Authentication {

  private String user;
  private String token;
  // Can login from any one company
  private int companyId;
  private boolean isActive;
  private Timestamp expiry;

  @Convert(converter = JpaConverterJson.class)
  private List<String> roles;

  @Override
  public Collection<? extends GrantedAuthority> getAuthorities() {
    return roles.stream()
      .map(AMSAuthority::new)
      .collect(Collectors.toList());
  }

  @Override
  public Object getCredentials() {
    return "**PROTECTED**";
  }

  @Override
  public Object getDetails() {
    return null;
  }

  @Override
  public Object getPrincipal() {
    return user;
  }

  @Override
  public boolean isAuthenticated() {
    return true;
  }

  @Override
  public void setAuthenticated(boolean b) throws IllegalArgumentException {
  }

  @Override
  public String getName() {
    return user;
  }
}

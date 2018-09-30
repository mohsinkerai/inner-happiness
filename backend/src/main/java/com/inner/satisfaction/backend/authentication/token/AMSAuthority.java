package com.inner.satisfaction.backend.authentication.token;

import lombok.AllArgsConstructor;
import lombok.Data;
import org.springframework.security.core.GrantedAuthority;

@Data
@AllArgsConstructor
public class AMSAuthority implements GrantedAuthority {

  private String amsAuthority;

  @Override
  public String getAuthority() {
    return amsAuthority;
  }
}

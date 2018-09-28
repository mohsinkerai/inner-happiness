package com.inner.satisfaction.backend.authentication;

import com.inner.satisfaction.backend.base.BaseEntity;
import java.util.List;
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
public class AuthenticationUser extends BaseEntity {

  private String username;
  private String password;
  private boolean isActive;
  private List<String> roles;
}

package com.inner.satisfaction.backend.authentication.token;

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
public class AuthenticationToken extends BaseEntity {

  private String token;
  private boolean isActive;
  private List<String> roles;
}

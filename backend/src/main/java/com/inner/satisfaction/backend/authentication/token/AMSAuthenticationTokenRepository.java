package com.inner.satisfaction.backend.authentication.token;

import com.inner.satisfaction.backend.base.BaseRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface AMSAuthenticationTokenRepository extends BaseRepository<AuthenticationToken> {

  AuthenticationToken findByToken(String token);
}

package com.inner.satisfaction.backend.authentication.token;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import java.util.Optional;
import org.springframework.stereotype.Service;

@Service
public class AuthenticationTokenService extends SimpleBaseService<AuthenticationToken> {

  private final AMSAuthenticationTokenRepository AMSAuthenticationTokenRepository;

  protected AuthenticationTokenService(
    AMSAuthenticationTokenRepository baseRepository) {
    super(baseRepository);
    this.AMSAuthenticationTokenRepository = baseRepository;
  }

  public Optional<AuthenticationToken> findByToken(String token) {
    return Optional.ofNullable(AMSAuthenticationTokenRepository.findByToken(token))
      .filter(t -> t.getExpiry().getTime() > System.currentTimeMillis());
  }
}

package com.inner.satisfaction.backend.authentication.token;

import com.inner.satisfaction.backend.base.BaseRepository;
import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class AuthenticationTokenService extends SimpleBaseService<AuthenticationToken> {

  protected AuthenticationTokenService(
    BaseRepository<AuthenticationToken> baseRepository) {
    super(baseRepository);
  }
}

package com.inner.satisfaction.backend.authentication;

import com.inner.satisfaction.backend.base.BaseRepository;
import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class AuthenticationUserService extends SimpleBaseService<AuthenticationUser> {

  protected AuthenticationUserService(
    BaseRepository<AuthenticationUser> baseRepository) {
    super(baseRepository);
  }
}

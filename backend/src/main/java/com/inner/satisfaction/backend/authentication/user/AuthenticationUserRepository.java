package com.inner.satisfaction.backend.authentication.user;

import com.inner.satisfaction.backend.base.BaseRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface AuthenticationUserRepository extends BaseRepository<AuthenticationUser> {

  AuthenticationUser findByUsernameAndIsActiveTrue(String username);
}

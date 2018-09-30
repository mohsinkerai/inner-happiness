package com.inner.satisfaction.backend.authentication.user;

import com.inner.satisfaction.backend.authentication.UserLoginDto;
import com.inner.satisfaction.backend.authentication.token.AuthenticationToken;
import com.inner.satisfaction.backend.authentication.token.AuthenticationTokenService;
import com.inner.satisfaction.backend.base.SimpleBaseService;
import java.sql.Timestamp;
import java.time.Clock;
import java.util.Optional;
import java.util.concurrent.TimeUnit;
import org.apache.commons.lang3.RandomStringUtils;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

@Service
public class AuthenticationUserService extends SimpleBaseService<AuthenticationUser> {

  private final Clock clock;
  private final long expiryInMinutes;
  private final PasswordEncoder passwordEncoder;
  private final AuthenticationTokenService authenticationTokenService;
  private final AuthenticationUserRepository authenticationUserRepository;

  protected AuthenticationUserService(
    Clock clock, AuthenticationUserRepository baseRepository,
    PasswordEncoder passwordEncoder,
    @Value("${token.expiry-in-minutes:120}") long expiryInMinutes,
    AuthenticationTokenService authenticationTokenService) {
    super(baseRepository);
    this.clock = clock;
    this.authenticationUserRepository = baseRepository;
    this.passwordEncoder = passwordEncoder;
    this.expiryInMinutes = expiryInMinutes;
    this.authenticationTokenService = authenticationTokenService;
  }

  public AuthenticationToken login(UserLoginDto loginDto) {
    AuthenticationUser au = authenticationUserRepository
      .findByUsernameAndIsActiveTrue(loginDto.getUsername());

    if (au != null && passwordEncoder.matches(loginDto.getPassword(), au.getPassword())) {
      String token = RandomStringUtils.randomAlphanumeric(32);
      AuthenticationToken authenticationToken = AuthenticationToken.builder()
        .token(token)
        .expiry(new Timestamp(TimeUnit.MINUTES.toMillis(expiryInMinutes) + clock.millis()))
        .isActive(true)
        .roles(au.getRoles())
        .user(au.getUsername())
        .build();

      return authenticationTokenService.save(authenticationToken);
      // Login
    } else {
      throw new RuntimeException("Incorrect username or password");
    }
  }

  public void updatePassword(long user, String newPassword) {
    Optional<AuthenticationUser> auOptional = authenticationUserRepository
      .findById(user);

    if (auOptional.isPresent()) {
      AuthenticationUser au = auOptional.get();
      String encodedPassword = passwordEncoder.encode(newPassword);
      au.setPassword(encodedPassword);
      save(au);
    } else {
      throw new RuntimeException("Incorrect userid");
    }
  }

  @Override
  public AuthenticationUser save(AuthenticationUser authenticationUser) {
    String encodedPassword = passwordEncoder.encode(authenticationUser.getPassword());
    authenticationUser.setPassword(encodedPassword);
    return super.save(authenticationUser);
  }
}

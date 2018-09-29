package com.inner.satisfaction.backend;

import com.google.common.collect.Lists;
import com.inner.satisfaction.backend.authentication.user.AuthenticationUser;
import com.inner.satisfaction.backend.authentication.user.AuthenticationUserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

//@Component
public class MyCli implements CommandLineRunner {

  @Autowired
  private AuthenticationUserService authenticationUserService;

  @Override
  public void run(String... args) throws Exception {
    AuthenticationUser user = AuthenticationUser.builder()
      .isActive(true)
      .password("admin")
      .username("admin")
      .roles(Lists.newArrayList("ADMIN"))
      .build();

    authenticationUserService.save(user);
  }
}

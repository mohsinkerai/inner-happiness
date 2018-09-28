package com.inner.satisfaction.backend.authentication;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + AuthenticationUserController.PATH)
public class AuthenticationUserController extends BaseController<AuthenticationUser> {

  public static final String PATH = "authentication";

  public AuthenticationUserController(AuthenticationUserService authenticationUserService) {
    super(authenticationUserService);
  }
}

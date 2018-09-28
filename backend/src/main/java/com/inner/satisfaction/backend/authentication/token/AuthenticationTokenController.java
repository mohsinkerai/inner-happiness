package com.inner.satisfaction.backend.authentication.token;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + AuthenticationTokenController.PATH)
public class AuthenticationTokenController extends BaseController<AuthenticationToken> {

  public static final String PATH = "authentication-user";

  public AuthenticationTokenController(AuthenticationTokenService authenticationTokenService) {
    super(authenticationTokenService);
  }
}

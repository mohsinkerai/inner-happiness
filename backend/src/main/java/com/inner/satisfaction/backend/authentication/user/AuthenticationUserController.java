package com.inner.satisfaction.backend.authentication.user;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.authentication.UserLoginDto;
import com.inner.satisfaction.backend.authentication.token.AuthenticationToken;
import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + AuthenticationUserController.PATH)
public class AuthenticationUserController extends BaseController<AuthenticationUser> {

  public static final String PATH = "auth";

  private final AuthenticationUserService authenticationUserService;

  public AuthenticationUserController(AuthenticationUserService authenticationUserService,
    AuthenticationUserService authenticationUserService1) {
    super(authenticationUserService);
    this.authenticationUserService = authenticationUserService1;
  }

  @PostMapping("login")
  @ResponseStatus(HttpStatus.OK)
  public AuthenticationToken login(@RequestBody UserLoginDto loginDto) {
    return authenticationUserService.login(loginDto);
  }

  @Override
  public AuthenticationUser putSave(Long id, AuthenticationUser authenticationUser) {
    throw new RuntimeException("Not allowed to Modify User, you can only change password, deactive or add new user");
  }
}

package com.inner.satisfaction.backend.config.security;

import com.inner.satisfaction.backend.authentication.user.AuthenticationUserController;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpMethod;
import org.springframework.security.config.annotation.method.configuration.EnableGlobalMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.builders.WebSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.web.servletapi.SecurityContextHolderAwareRequestFilter;

@Configuration
@EnableWebSecurity
@EnableGlobalMethodSecurity(prePostEnabled = true)
public class WebSecurityConfig extends WebSecurityConfigurerAdapter {

  private final AMSAuthenticationTokenFilter authenticationTokenFilter;

  public WebSecurityConfig(
    AMSAuthenticationTokenFilter authenticationTokenFilter) {
    this.authenticationTokenFilter = authenticationTokenFilter;
  }

  @Override
  protected void configure(HttpSecurity http) throws Exception {

    // Disable CSRF (cross site request forgery)
    http.csrf().disable();

    // No session will be created or used by spring security
    http.sessionManagement().sessionCreationPolicy(SessionCreationPolicy.STATELESS);

    // Entry points
    http.authorizeRequests()//
      .antMatchers(HttpMethod.OPTIONS, "/**").permitAll()
      .antMatchers("/" + AuthenticationUserController.PATH + "/login").permitAll()//
      .antMatchers("/users/signin").permitAll()//
      .antMatchers("/users/signup").permitAll()//
      // Disallow everything else..
      .anyRequest().authenticated();

    // If a user try to access a resource without having enough permissions
    http.exceptionHandling().accessDeniedPage("/" + AuthenticationUserController.PATH + "/login");

    http.addFilterAfter(authenticationTokenFilter, SecurityContextHolderAwareRequestFilter.class);

//    http.formLogin()
//      .disable();

//     Optional, if you want to test the API from a browser
//    http.httpBasic();
  }

  @Override
  public void configure(WebSecurity web) throws Exception {
    // Allow swagger to be accessed without authentication
    web.ignoring().antMatchers("/v2/api-docs")//
      .antMatchers("/swagger-resources/**")//
      .antMatchers("/swagger-ui.html")//
      .antMatchers("/swagger.json")//
      .antMatchers("/configuration/**")//
      .antMatchers("/webjars/**")//
      .antMatchers("/public");
  }
}
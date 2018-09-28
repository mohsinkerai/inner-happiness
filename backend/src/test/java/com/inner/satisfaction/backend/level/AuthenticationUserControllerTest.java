package com.inner.satisfaction.backend.level;

import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultHandlers.print;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

import com.inner.satisfaction.backend.base.AbstractIntegrationTest;
import org.junit.Test;
import org.springframework.http.MediaType;

public class AuthenticationTokenControllerTest extends AbstractIntegrationTest{

  @Test
  public void shouldFindByParentId() throws Exception {
    mockMvc.perform(
        get("/level/search/parent?value=2").contentType(MediaType.APPLICATION_JSON))
        .andDo(print())
        .andExpect(status().is2xxSuccessful());
  }
}
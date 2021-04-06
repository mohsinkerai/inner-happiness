package com.inner.satisfaction.backend;

import static org.hamcrest.CoreMatchers.startsWith;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultHandlers.print;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;

import com.inner.satisfaction.backend.base.AbstractIntegrationTest;
import org.junit.jupiter.api.Test;

public class BasicApplicationTests extends AbstractIntegrationTest {

  @Test
  public void loadContext() {
  }

  @Test
  public void healthCheckTest() throws Exception {
    mockMvc.perform(get("/actuator/health"))
        .andDo(print())
        .andExpect(jsonPath("$.status").value("UP"));

    mockMvc.perform(get("/health"))
        .andExpect(jsonPath("$.status", startsWith("UP")));
  }
}

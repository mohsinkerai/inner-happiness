package com.inner.satisfaction.backend.company;

import static org.hamcrest.Matchers.hasSize;
import static org.junit.Assert.*;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultHandlers.print;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

import com.inner.satisfaction.backend.base.AbstractIntegrationTest;
import org.junit.Test;
import org.springframework.http.MediaType;

public class ApplicationConfigurationTest extends AbstractIntegrationTest {

  @Test
  public void shouldFindAll() throws Exception {
    mockMvc.perform(get("/company/all").contentType(MediaType.APPLICATION_JSON))
        .andDo(print())
        .andExpect(status().is2xxSuccessful())
        .andExpect(jsonPath("$.[*]", hasSize(3)));
  }
}
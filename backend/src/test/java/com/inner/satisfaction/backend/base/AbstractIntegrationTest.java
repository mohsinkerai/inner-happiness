package com.inner.satisfaction.backend.base;

import static org.springframework.test.context.TestExecutionListeners.MergeMode.MERGE_WITH_DEFAULTS;

import org.bitbucket.radistao.test.annotation.AfterAllMethods;
import org.bitbucket.radistao.test.annotation.BeforeAllMethods;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.ActiveProfiles;
import org.springframework.test.context.TestExecutionListeners;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;

@RunWith(SpringRunner.class)
@SpringBootTest
@AutoConfigureMockMvc
@ActiveProfiles("test")
public abstract class AbstractIntegrationTest {

//  @ClassRule
//  public static EmbeddedMySQLRule embeddedMySQLRule = new EmbeddedMySQLRule();

  static MyTestExecutionListener testExecutionListener = new MyTestExecutionListener();

  @Autowired
  protected MockMvc mockMvc;

  @BeforeAllMethods
  public static void setUp() throws Exception {
    testExecutionListener.setUp();
  }

  @AfterAllMethods
  public static void tearDown() throws Exception {
    testExecutionListener.tearDown();
  }
}

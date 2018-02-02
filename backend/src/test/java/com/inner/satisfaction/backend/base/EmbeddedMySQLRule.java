package com.inner.satisfaction.backend.base;

import static com.wix.mysql.EmbeddedMysql.anEmbeddedMysql;
import static com.wix.mysql.config.Charset.UTF8;
import static com.wix.mysql.distribution.Version.v5_6_23;

import com.wix.mysql.EmbeddedMysql;
import com.wix.mysql.config.MysqldConfig;
import java.util.TimeZone;
import lombok.extern.slf4j.Slf4j;
import org.junit.rules.TestRule;
import org.junit.runner.Description;
import org.junit.runners.model.Statement;

@Slf4j
public class EmbeddedMySQLRule implements TestRule {

  @Override
  public Statement apply(Statement statement, Description description) {
    return new TestStatement(statement);
  }

  class TestStatement extends Statement {

    private final Statement base;
    private EmbeddedMysql mysqld;

    public TestStatement(Statement base) {
      this.base = base;
    }

    @Override
    public void evaluate() throws Throwable {
      try {
        setUp();
        base.evaluate();
      } catch (Exception e) {
        throw e;
      } finally {
        tearDown();
      }
    }

    public void setUp() {
      MysqldConfig config = MysqldConfig.aMysqldConfig(v5_6_23)
          .withCharset(UTF8)
          .withPort(3302)
          .withTimeZone(TimeZone.getDefault())
          .withTempDir(System.getProperty("java.io.tmpdir"))
          .build();

      log.info("Username {} and Password {} from Embedded MySQL", config.getUsername(),
          config.getPassword());

      mysqld = anEmbeddedMysql(config)
          .addSchema("inner_satisfaction")
          .start();
    }

    public void tearDown() {
      if (mysqld != null) {
        mysqld.stop();
      }
    }
  }
}

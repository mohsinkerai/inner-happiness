package com.inner.satisfaction.backend.base;

import static com.wix.mysql.EmbeddedMysql.anEmbeddedMysql;
import static com.wix.mysql.ScriptResolver.classPathScript;
import static com.wix.mysql.config.Charset.UTF8;
import static com.wix.mysql.distribution.Version.v5_6_23;

import com.wix.mysql.EmbeddedMysql;
import com.wix.mysql.config.MysqldConfig;
import java.util.TimeZone;
import lombok.extern.slf4j.Slf4j;
import org.springframework.core.Ordered;
import org.springframework.core.annotation.Order;
import org.springframework.test.context.TestContext;
import org.springframework.test.context.TestExecutionListener;
import org.springframework.test.context.support.AbstractTestExecutionListener;

@Slf4j
public class MyTestExecutionListener {

  public MyTestExecutionListener() {
    setUp();
  }

  private EmbeddedMysql mysqld;

  public void setUp() {
    MysqldConfig config = MysqldConfig.aMysqldConfig(v5_6_23)
        .withCharset(UTF8)
        .withPort(3302)
        .withTimeZone(TimeZone.getDefault())
        .withTempDir(System.getProperty("java.io.tmpdir"))
        .build();

    log.info("Username {} and Password {} from Embedded MySQL", config.getUsername(),
        config.getPassword());

    if (mysqld == null) {
      mysqld = anEmbeddedMysql(config)
          .addSchema("inner_satisfaction", classPathScript("dump.sql"))
          .start();
    }
  }

  public void tearDown() {
    if (mysqld != null) {
      mysqld.stop();
    }
  }
}

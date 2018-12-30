package com.inner.satisfaction.backend;

import com.inner.satisfaction.backend.appointment.AppointmentPositionService;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

@Slf4j
//@Component
public class MyCLI implements CommandLineRunner {

  @Autowired
  private AppointmentPositionService appointmentPositionService;

  @Override
  public void run(String... args) throws Exception {
    log.info("not recommended persons {}", appointmentPositionService.findByCycleIdWhereNoOneIsRecommended(14));
  }
}

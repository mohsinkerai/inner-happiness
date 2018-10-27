package com.inner.satisfaction.backend.person.appointment.validation.message;

import com.inner.satisfaction.backend.base.BaseRepository;
import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class ValidationMessageService extends SimpleBaseService<ValidationMessage> {

  protected ValidationMessageService(
    BaseRepository<ValidationMessage> baseRepository) {
    super(baseRepository);
  }
}

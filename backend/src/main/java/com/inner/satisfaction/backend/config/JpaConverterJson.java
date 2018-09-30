package com.inner.satisfaction.backend.config;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import java.io.IOException;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import javax.persistence.AttributeConverter;
import lombok.extern.slf4j.Slf4j;

@Slf4j
public class JpaConverterJson implements AttributeConverter<Object, String> {

  private final ObjectMapper objectMapper;

  public JpaConverterJson() {
    objectMapper = new ObjectMapper();
    DateFormat df = new SimpleDateFormat("yyyy-MM-dd");
    objectMapper.setDateFormat(df);
  }


  @Override
  public String convertToDatabaseColumn(Object meta) {
    if (meta == null) {
      return null;
    }
    try {
      return objectMapper.writeValueAsString(meta);
    } catch (JsonProcessingException ex) {
      log.error("Unable to Serialize Column", ex);
      return null;
    }
  }

  @Override
  public Object convertToEntityAttribute(String dbData) {
    if (dbData == null) {
      return null;
    }
    try {
      return objectMapper.readValue(dbData, Object.class);
    } catch (IOException ex) {
      log.error("Unable to DeSerialize Column", ex);
      return null;
    }
  }
}
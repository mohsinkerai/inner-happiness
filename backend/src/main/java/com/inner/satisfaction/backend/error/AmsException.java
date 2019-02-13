package com.inner.satisfaction.backend.error;

import com.google.common.collect.Maps;
import java.util.Map;
import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class AmsException extends RuntimeException {

  private final int errorCode;
  private final String message;
  private final Map<String, Object> details;

  public AmsException(final ErrorEnumType errorEnumType, final Map<String, Object> details) {
    this.errorCode = errorEnumType.getErrorCode();
    this.message = errorEnumType.getMessage();
    this.details = Maps.newHashMap(details);
  }

  public AmsException(final ErrorEnumType errorEnumType) {
    this.errorCode = errorEnumType.getErrorCode();
    this.message = errorEnumType.getMessage();
    this.details = Maps.newHashMap();
  }
}

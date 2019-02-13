package com.inner.satisfaction.backend.error;

import java.sql.Timestamp;
import java.util.Map;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.ResponseStatus;

@Slf4j
@ControllerAdvice
public class ExceptionInterceptor {

  @ExceptionHandler(AmsException.class)
  @ResponseStatus(HttpStatus.BAD_REQUEST)
  public ErrorResponse handleAmsException(AmsException amsException) {
    ErrorResponse errorResponse = ErrorResponse.builder()
      .message(amsException.getMessage())
      .errorCode(String.valueOf(amsException.getErrorCode()))
      .timestamp(new Timestamp(System.currentTimeMillis()))
      .details(amsException.getDetails())
      .build();
    log.info("System Logical Error Due to {}", errorResponse);
    return errorResponse;
  }

  @Builder
  @Data
  @NoArgsConstructor
  @AllArgsConstructor
  public class ErrorResponse {

    private String message;
    private Timestamp timestamp;
    private String errorCode;
    private Map<String, Object> details;
  }
}

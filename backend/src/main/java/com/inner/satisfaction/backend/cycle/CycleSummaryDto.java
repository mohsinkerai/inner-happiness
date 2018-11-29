package com.inner.satisfaction.backend.cycle;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@JsonIgnoreProperties(ignoreUnknown = true)
public class CycleSummaryDto {

  private long nominatedPerson;
  private long recommendedPerson;
  private int formsEnteredToday;
  private int formsEnteredYesterday;
}

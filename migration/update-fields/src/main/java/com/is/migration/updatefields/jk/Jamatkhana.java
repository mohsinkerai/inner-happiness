package com.is.migration.updatefields.jk;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Builder
@Data
@AllArgsConstructor
@NoArgsConstructor
public class Jamatkhana {

  private String ncCode;
  private String eoCode;
  private String ncName;
  private String eoName;
  private String lcName;
  private String rcName;
}

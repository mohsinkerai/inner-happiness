package com.inner.satisfaction.backend.position;

import com.inner.satisfaction.backend.base.BaseDto;
import com.inner.satisfaction.backend.institution.Institution;
import com.inner.satisfaction.backend.person.Person;
import com.inner.satisfaction.backend.person.appointment.PersonAppointment;
import java.util.List;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@AllArgsConstructor
@NoArgsConstructor
public class PositionDetailsDto extends BaseDto {

  private long cpiId;
  private List<PositionDetailsPersonDto> personsNominated;
  private PositionDetailsPersonDto incumbent;
  private Institution institution;
  private Position position;

  @Data
  @NoArgsConstructor
  @AllArgsConstructor
  public class PositionDetailsPersonDto extends BaseDto {

    private Person person;
    private PersonAppointment personAppointment;
  }
}
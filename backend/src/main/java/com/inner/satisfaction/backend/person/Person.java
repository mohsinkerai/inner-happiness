package com.inner.satisfaction.backend.person;

import com.inner.satisfaction.backend.base.BaseEntity;
import java.sql.Timestamp;
import javax.persistence.Entity;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Entity
public class Person extends BaseEntity{

  private String cnic;
  private String passportNumber;
  private String imagePath;

  private String salutation;
  private String firstName;
  private String fatherName;
  private String familyName;
  // 1 male, 2 female
  private String jamatiTitle;
  private int gender;
  private Timestamp dateOfBirth;
  private String maritalStatus;
  private String residentialAddress;
  private String city;
  private String residenceTelephone;
  private String mobilePhone;
  private String email;

  private String areaOfOrigin;

  // LevelID To be Exact
  private long regionalCouncilId;
  private long localCouncilId;
  private long jamatkhanaId;
  private String relocation;
  private Timestamp relocationDateTime;
}

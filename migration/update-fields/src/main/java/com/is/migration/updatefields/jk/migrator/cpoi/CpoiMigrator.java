package com.is.migration.updatefields.jk.migrator.cpoi;

import com.is.migration.updatefields.jk.persistance.cpoi.CPoi;
import com.is.migration.updatefields.jk.persistance.cpoi.CPoiRepository;
import com.is.migration.updatefields.jk.persistance.poi.Poi;
import com.is.migration.updatefields.jk.persistance.poi.PoiRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class CpoiMigrator {

  @Autowired
  private CPoiRepository cPoiRepo;

  @Autowired
  private PoiRepository poiRepo;

  public void migrate() {
    for (Poi poi : poiRepo.findAll()) {
      CPoi cpoi = CPoi.builder()
          .cycleId(poi.getCycleId())
          .id(poi.getId())
          .poiId(poi.getId())
          .desired(poi.getDesired())
          .max(poi.getMax())
          .min(poi.getMin()).build();
      cPoiRepo.save(cpoi);
    }
  }

}

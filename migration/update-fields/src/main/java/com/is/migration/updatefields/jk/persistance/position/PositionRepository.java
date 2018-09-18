package com.is.migration.updatefields.jk.persistance.position;

import java.util.List;
import org.springframework.data.jpa.repository.JpaRepository;

public interface PositionRepository extends JpaRepository<Position, Long> {

  List<Position> findByOldIdEqualsAndSeatIdEquals(Long oldId, Long seatId);

  List<Position> findByOldId(Long oldId);
}

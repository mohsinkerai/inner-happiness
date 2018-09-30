package com.inner.satisfaction.backend.lookups.relation;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class RelationService extends SimpleBaseService<Relation> {

  protected RelationService(
      RelationRepository baseRepository) {
    super(baseRepository);
  }
}

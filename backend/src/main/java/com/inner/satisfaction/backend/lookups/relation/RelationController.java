package com.inner.satisfaction.backend.lookups.relation;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + RelationController.PATH)
public class RelationController extends BaseController<Relation> {

  public static final String PATH = "relation";

  public RelationController(RelationService relationService) {
    super(relationService);
  }
}

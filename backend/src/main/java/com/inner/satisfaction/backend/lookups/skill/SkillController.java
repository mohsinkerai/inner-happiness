package com.inner.satisfaction.backend.lookups.skill;

import static com.inner.satisfaction.backend.base.BaseController.CONSTANTS_PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(CONSTANTS_PREFIX + SkillController.PATH)
public class SkillController extends BaseController<Skill> {

  public static final String PATH = "skill";

  public SkillController(SkillService skillService) {
    super(skillService);
  }
}

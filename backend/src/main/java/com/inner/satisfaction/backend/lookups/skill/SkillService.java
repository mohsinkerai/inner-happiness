package com.inner.satisfaction.backend.lookups.skill;

import com.inner.satisfaction.backend.base.SimpleBaseService;
import org.springframework.stereotype.Service;

@Service
public class SkillService extends SimpleBaseService<Skill> {

  protected SkillService(
      SkillRepository baseRepository) {
    super(baseRepository);
  }
}

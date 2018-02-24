package com.inner.satisfaction.backend.level;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import com.inner.satisfaction.backend.person.Person;
import com.inner.satisfaction.backend.person.PersonService;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + LevelController.PATH)
public class LevelController extends BaseController<Level> {

  public static final String PATH = "level";

  public LevelController(LevelService levelService) {
    super(levelService);
  }
}

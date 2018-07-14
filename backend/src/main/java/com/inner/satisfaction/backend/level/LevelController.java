package com.inner.satisfaction.backend.level;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import com.inner.satisfaction.backend.person.Person;
import java.util.Set;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(PREFIX + LevelController.PATH)
public class LevelController extends BaseController<Level> {

  public static final String PATH = "level";
  private final LevelService levelService;

  public LevelController(LevelService levelService) {
    super(levelService);
    this.levelService = levelService;
  }

  @RequestMapping("/search/parent")
  @ResponseStatus(HttpStatus.OK)
  public Set<Level> findByLevelParentId(@RequestParam("value") long levelParentId) {
    return levelService.findByLevelParentId(levelParentId);
  }

  @RequestMapping("/search/type")
  @ResponseStatus(HttpStatus.OK)
  public Set<Level> findByLevelTypeId(@RequestParam("value") long levelTypeId) {
    return levelService.findByLevelParentId(levelTypeId);
  }
}

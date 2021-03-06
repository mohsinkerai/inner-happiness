package com.inner.satisfaction.backend.level;

import static com.inner.satisfaction.backend.base.BaseController.PREFIX;

import com.inner.satisfaction.backend.base.BaseController;
import java.util.Set;
import javax.websocket.server.PathParam;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
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

  @RequestMapping({"/search/parent", "/search/findByParentId"})
  @ResponseStatus(HttpStatus.OK)
  public Set<Level> findByLevelParentId(@RequestParam("value") long levelParentId) {
    return levelService.findByLevelParentId(levelParentId);
  }

  @RequestMapping({"/search/type", "/search/findByLevelTypeId"})
  @ResponseStatus(HttpStatus.OK)
  public Set<Level> findByLevelTypeId(@RequestParam("value") int levelTypeId) {
    return levelService.findByLevelTypeId(levelTypeId);
  }

  @RequestMapping(method = RequestMethod.PATCH, value = {"update/close/{levelId}"})
  @ResponseStatus(HttpStatus.OK)
  public void levelClose(@PathParam("levelId") long levelId) {
    levelService.close(levelId);
  }

  @RequestMapping(method = RequestMethod.PATCH, value = {"update/parentChange/{levelId}"})
  @ResponseStatus(HttpStatus.OK)
  public void levelParentChange(@PathParam("levelId") long levelId, @RequestBody long newParentId) {
    levelService.updateParent(levelId, newParentId);
  }
}

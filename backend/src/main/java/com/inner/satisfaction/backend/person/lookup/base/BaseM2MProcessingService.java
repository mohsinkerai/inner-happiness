package com.inner.satisfaction.backend.person.lookup.base;

import com.google.common.collect.Lists;
import com.inner.satisfaction.backend.base.BaseDto;
import com.inner.satisfaction.backend.base.BaseEntity;
import com.inner.satisfaction.backend.person.Person;
import java.util.List;

public abstract class BaseM2MProcessingService<E extends BaseEntity, D extends BaseDto> {

  /**
   * Used during save of person. Ideally it should extract respective field out of person
   * <ol>
   * <li>Removes all existance of person from m-n table</li>
   * <li>Extract respective dto/field from person</li>
   * <li>If it exists, put it off, if it doesn't save it and get its id</li>
   * <li>Save M-N relation</li>
   * </ol>
   */
  public List<E> processList(Person person, long personId) {
    removeAllEntityByPersonId(personId);
    List<D> dto = convert(person);
    return processDto(dto, personId);
  }

  public Person populatePerson(Person person) {
    Long personId = person.getId();
    List<E> personEntities = findPersonEntities(personId);
    return populateEntityInPerson(person, personEntities);
  }

  private List<E> processDto(List<D> dto, long personId) {
    List<E> entities = Lists.newArrayList();

    for (D d : dto) {
      setEntityId(d, getEntityId(d));
      setPersonId(d, personId);
      E e = convert(d);
      saveEntity(e);
      entities.add(e);
    }

    return entities;
  }

  /**
   * Finds and returns all the related entities to person
   *
   * It returns all the related Entities associated to that person. For example person skills, so it
   * should return all the skills associated to that person. Similarly for Person Relation Person,
   * it should return all those persons who are associated to that persons. That is my family
   * members
   */
  protected abstract List<E> findPersonEntities(long personId);

  /**
   * Populates back entity in person
   *
   * This method actually knows that where exactly to put output given by findPersonEntities into
   * real person object. Used in findOnePerson
   *
   * Lets take example for skills again, now findPersonEntities returns list of skills possessed by
   * that person. you need to put it into that specific place. For relations, you need to put it
   * into specific relations.
   */
  protected abstract Person populateEntityInPerson(Person person, List<E> e);

  /**
   * Returns id for M-N table.
   *
   * For Example of it is Person-Skills, it should give you id for particular skill given. If this
   * skill doesn't exist, it is expected to create it. similar case is with person relation person.
   * if relative of person doesn't exist, it is expected to create it.
   */
  protected abstract long getEntityId(D dto);

  /**
   * Sets personId to respective Field
   */
  protected abstract D setPersonId(D dto, long id);

  /**
   * sets Entity Id to respective dto.
   *
   * For example in skills, it should put specific skill id in the dto. In person relation person,
   * if it should add id.
   */
  protected abstract D setEntityId(D dto, long id);

  /**
   * Converts Dto to Entitydto
   *
   * During save process, first we made sure that referenced entity exists such as skills or
   * relative of a person. If it didn't existed, we created it. Now when it exist and person exist
   * and everything exist, we should convert them to Entity in order to save it.
   */
  protected abstract E convert(D dto);

  /**
   * Convert Raw Field to Dto
   *
   * It is used in save flow. It gets person, it needs to extract specific dto from person for
   * further processing. One such example is of skills. This method should convert person to
   * skillsDto by extracting List<String> skills and converting them to skillsDto
   */
  protected abstract List<D> convert(Person person);

  /**
   * removes all entities by personId
   *
   * Removes all M-N entitites from M-N Tables particular to that person. This is done before saving
   * new entities, to ensure all previous data has been wiped off
   */
  protected abstract void removeAllEntityByPersonId(long personId);

  /**
   * save entity and returns value
   */
  protected abstract E saveEntity(E e);
}

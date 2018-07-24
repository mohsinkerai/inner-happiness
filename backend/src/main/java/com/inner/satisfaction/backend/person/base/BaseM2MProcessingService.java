package com.inner.satisfaction.backend.person.base;

import com.google.common.collect.Lists;
import com.inner.satisfaction.backend.base.BaseDto;
import com.inner.satisfaction.backend.base.BaseEntity;
import com.inner.satisfaction.backend.person.Person;
import java.util.List;
import java.util.stream.Collectors;

public abstract class BaseM2MProcessingService<E extends BaseEntity, D extends BaseDto> {

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

    for(D d : dto) {
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
   */
  protected abstract List<E> findPersonEntities(long personId);

  /**
   * Populates back entity in person
   */
  protected abstract Person populateEntityInPerson(Person person, List<E> e);

  /**
   * Returns id
   */
  protected abstract long getEntityId(D dto);

  /**
   * Sets personId to respective Field
   */
  protected abstract D setPersonId(D dto, long id);

  /**
   * sets Entity Id
   */
  protected abstract D setEntityId(D dto, long id);

  /**
   * Converts Dto to Entitydto
   */
  protected abstract E convert(D dto);

  /**
   * Convert Raw Field to Dto
   */
  protected abstract List<D> convert(Person person);

  /**
   * removes all entities by personId
   */
  protected abstract void removeAllEntityByPersonId(long personId);

  /**
   * save entity and returns value
   */
  protected abstract E saveEntity(E e);
}

package org.lightadmin.boot.repository;

import org.lightadmin.boot.newdomain.Hotel;
import org.springframework.data.repository.PagingAndSortingRepository;
import org.springframework.transaction.annotation.Transactional;

@Transactional
public interface HotelRepository extends PagingAndSortingRepository<Hotel, Long> {

}
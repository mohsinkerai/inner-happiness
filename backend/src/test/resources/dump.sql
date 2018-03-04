-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.7.21-log - MySQL Community Server (GPL)
-- Server OS:                    Win64
-- HeidiSQL Version:             9.5.0.5196
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Dumping structure for table inner_satisfaction.company
CREATE TABLE IF NOT EXISTS `company` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COMMENT='A table to store level types, a level type can be Regional, Southern, National, Jamatkhana';

-- Dumping data for table inner_satisfaction.company: ~3 rows (approximately)
/*!40000 ALTER TABLE `company` DISABLE KEYS */;
INSERT INTO `company` (`id`, `name`) VALUES
	(1, 'National Council'),
	(2, 'Estate Office'),
	(3, 'AKDN');
/*!40000 ALTER TABLE `company` ENABLE KEYS */;

-- Dumping structure for table inner_satisfaction.cpal_awardee
CREATE TABLE IF NOT EXISTS `cpal_awardee` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `person_id` bigint(20) NOT NULL,
  `cpal_id` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='Award of Position, lets say Who Becomes President';

-- Dumping data for table inner_satisfaction.cpal_awardee: ~1 rows (approximately)
/*!40000 ALTER TABLE `cpal_awardee` DISABLE KEYS */;
INSERT INTO `cpal_awardee` (`id`, `person_id`, `cpal_id`) VALUES
	(1, 1, 2);
/*!40000 ALTER TABLE `cpal_awardee` ENABLE KEYS */;

-- Dumping structure for table inner_satisfaction.cpal_nomination
CREATE TABLE IF NOT EXISTS `cpal_nomination` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `person_id` bigint(20) NOT NULL,
  `cpal_id` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='Nomination of President, Who is nominated for President';

-- Dumping data for table inner_satisfaction.cpal_nomination: ~4 rows (approximately)
/*!40000 ALTER TABLE `cpal_nomination` DISABLE KEYS */;
INSERT INTO `cpal_nomination` (`id`, `person_id`, `cpal_id`) VALUES
	(1, 1, 2),
	(2, 2, 2),
	(3, 3, 2),
	(4, 4, 2);
/*!40000 ALTER TABLE `cpal_nomination` ENABLE KEYS */;

-- Dumping structure for table inner_satisfaction.cycle
CREATE TABLE IF NOT EXISTS `cycle` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `start_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `end_date` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='Cycles Available, Lets say Cycle 2018-2019';

-- Dumping data for table inner_satisfaction.cycle: ~1 rows (approximately)
/*!40000 ALTER TABLE `cycle` DISABLE KEYS */;
INSERT INTO `cycle` (`id`, `name`, `start_date`, `end_date`) VALUES
	(1, 'Cycle#38', '2015-07-11 00:00:00', '2018-07-10 00:00:00');
/*!40000 ALTER TABLE `cycle` ENABLE KEYS */;

-- Dumping structure for table inner_satisfaction.cycle_position_on_active_level
CREATE TABLE IF NOT EXISTS `cycle_position_on_active_level` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `cycle_id` bigint(20) NOT NULL,
  `position_on_active_level_id` bigint(20) NOT NULL,
  `min_count` int(11) DEFAULT NULL,
  `desired` int(11) DEFAULT NULL,
  `max_count` int(11) DEFAULT NULL,
  `nominations` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='A Triangular Table which connect Level Active Position to Cycle';

-- Dumping data for table inner_satisfaction.cycle_position_on_active_level: ~2 rows (approximately)
/*!40000 ALTER TABLE `cycle_position_on_active_level` DISABLE KEYS */;
INSERT INTO `cycle_position_on_active_level` (`id`, `cycle_id`, `position_on_active_level_id`, `min_count`, `desired`, `max_count`, `nominations`) VALUES
	(1, 1, 1, 1, 1, 1, 3),
	(2, 2, 2, 1, 1, 1, 3);
/*!40000 ALTER TABLE `cycle_position_on_active_level` ENABLE KEYS */;

-- Dumping structure for table inner_satisfaction.level
CREATE TABLE IF NOT EXISTS `level` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `level_type_id` bigint(20) NOT NULL,
  `name` varchar(255) NOT NULL,
  `full_name` varchar(255) DEFAULT NULL,
  `short_code` varchar(255) NOT NULL,
  `address` varchar(255) DEFAULT NULL,
  `company_id` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='A table to store actual levels, Council for Pakistan, Southern Council';

-- Dumping data for table inner_satisfaction.level: ~4 rows (approximately)
/*!40000 ALTER TABLE `level` DISABLE KEYS */;
INSERT INTO `level` (`id`, `level_type_id`, `name`, `full_name`, `short_code`, `address`, `company_id`) VALUES
	(1, 1, 'Pakistan', 'His Highness Prince Aga Khan Shia Imami Ismaili Council for Pakistan', 'PK', 'Opp. Garden Jamatkhana, Britto Road, Garden East', 1),
	(2, 2, 'Southern', 'His Highness Prince Aga Khan Shia Imami Ismaili Council for Southern Region', 'SO', 'Opp. Garden Jamatkhana, Britto Road, Garden East', 1),
	(3, 3, 'Karimabad', 'His Highness Prince Aga Khan Shia Imami Ismaili Council for Karimabad', 'SO.03', 'Room # 1, Karimabad Jamatkhana Karachi', 1),
	(4, 4, 'Karimabad Jamatkhana', 'Karimabad Jamatkhana', 'SO.03.03', 'Room # 1, Karimabad Jamatkhana Karachi', 2);
/*!40000 ALTER TABLE `level` ENABLE KEYS */;

-- Dumping structure for table inner_satisfaction.level_active
CREATE TABLE IF NOT EXISTS `level_active` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `level_id` bigint(20) DEFAULT NULL,
  `level_active_parent_id` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='A table to store active levels, Council for Pakistan, Southern Council, Active level is a virtuality on levels';

-- Dumping data for table inner_satisfaction.level_active: ~2 rows (approximately)
/*!40000 ALTER TABLE `level_active` DISABLE KEYS */;
INSERT INTO `level_active` (`id`, `level_id`, `level_active_parent_id`) VALUES
	(1, 1, NULL),
	(2, 2, '1');
/*!40000 ALTER TABLE `level_active` ENABLE KEYS */;

-- Dumping structure for table inner_satisfaction.level_type
CREATE TABLE IF NOT EXISTS `level_type` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='A table to store level types, a level type can be Regional, Southern, National, Jamatkhana';

-- Dumping data for table inner_satisfaction.level_type: ~4 rows (approximately)
/*!40000 ALTER TABLE `level_type` DISABLE KEYS */;
INSERT INTO `level_type` (`id`, `name`) VALUES
	(1, 'National'),
	(2, 'Regional'),
	(3, 'Local'),
	(4, 'Jamatkhana');
/*!40000 ALTER TABLE `level_type` ENABLE KEYS */;

-- Dumping structure for table inner_satisfaction.person
CREATE TABLE IF NOT EXISTS `person` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `email` varchar(255) DEFAULT NULL,
  `date_of_birth` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='A Person in the real world, i.e. Mohsin Kerai or Zeeshan Damani';

-- Dumping data for table inner_satisfaction.person: ~4 rows (approximately)
/*!40000 ALTER TABLE `person` DISABLE KEYS */;
INSERT INTO `person` (`id`, `name`, `email`, `date_of_birth`) VALUES
	(1, 'Mohsin Mansoor Kerai', NULL, '1993-07-27 00:00:00'),
	(2, 'Zeeshan Moiz Damani', NULL, '1994-10-10 00:00:00'),
	(3, 'Naveed Tejani', NULL, '1989-11-07 00:00:00'),
	(4, 'Amsal Akber', NULL, '1995-05-13 00:00:00');
/*!40000 ALTER TABLE `person` ENABLE KEYS */;

-- Dumping structure for table inner_satisfaction.person_relation_person
CREATE TABLE IF NOT EXISTS `person_relation_person` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `first_person_id` bigint(20) NOT NULL,
  `second_person_id` bigint(20) NOT NULL,
  `relation_id` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='A Person in the real world, i.e. Mohsin Kerai or Zeeshan Damani';

-- Dumping data for table inner_satisfaction.person_relation_person: ~1 rows (approximately)
/*!40000 ALTER TABLE `person_relation_person` DISABLE KEYS */;
INSERT INTO `person_relation_person` (`id`, `first_person_id`, `second_person_id`, `relation_id`) VALUES
	(1, 1, 2, 1);
/*!40000 ALTER TABLE `person_relation_person` ENABLE KEYS */;

-- Dumping structure for table inner_satisfaction.position
CREATE TABLE IF NOT EXISTS `position` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='Stores Positions Available at Any Levels, Lets say President, Chairman, Sec, Hon. Sec, It Consultant';

-- Dumping data for table inner_satisfaction.position: ~2 rows (approximately)
/*!40000 ALTER TABLE `position` DISABLE KEYS */;
INSERT INTO `position` (`id`, `name`) VALUES
	(1, 'President'),
	(2, 'Member Finance');
/*!40000 ALTER TABLE `position` ENABLE KEYS */;

-- Dumping structure for table inner_satisfaction.position_on_active_level
CREATE TABLE IF NOT EXISTS `position_on_active_level` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `position_id` bigint(20) DEFAULT NULL,
  `level_active_id` bigint(20) DEFAULT NULL,
  `is_active` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='A table to store active position w.r.t level, lets say President Exist in National Council and Chairman in Health Board';

-- Dumping data for table inner_satisfaction.position_on_active_level: ~2 rows (approximately)
/*!40000 ALTER TABLE `position_on_active_level` DISABLE KEYS */;
INSERT INTO `position_on_active_level` (`id`, `position_id`, `level_active_id`, `is_active`) VALUES
	(1, 1, 1, b'1'),
	(2, 2, 1, b'1');
/*!40000 ALTER TABLE `position_on_active_level` ENABLE KEYS */;

-- Dumping structure for table inner_satisfaction.relation
CREATE TABLE IF NOT EXISTS `relation` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='A Person in the real world, i.e. Mohsin Kerai or Zeeshan Damani';

-- Dumping data for table inner_satisfaction.relation: ~1 rows (approximately)
/*!40000 ALTER TABLE `relation` DISABLE KEYS */;
INSERT INTO `relation` (`id`, `name`) VALUES
	(1, 'Brother');
/*!40000 ALTER TABLE `relation` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;

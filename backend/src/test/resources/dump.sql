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

-- Dumping structure for table inner_satisfaction.databasechangelog
CREATE TABLE IF NOT EXISTS `databasechangelog` (
  `ID` varchar(255) NOT NULL,
  `AUTHOR` varchar(255) NOT NULL,
  `FILENAME` varchar(255) NOT NULL,
  `DATEEXECUTED` datetime NOT NULL,
  `ORDEREXECUTED` int(11) NOT NULL,
  `EXECTYPE` varchar(10) NOT NULL,
  `MD5SUM` varchar(35) DEFAULT NULL,
  `DESCRIPTION` varchar(255) DEFAULT NULL,
  `COMMENTS` varchar(255) DEFAULT NULL,
  `TAG` varchar(255) DEFAULT NULL,
  `LIQUIBASE` varchar(20) DEFAULT NULL,
  `CONTEXTS` varchar(255) DEFAULT NULL,
  `LABELS` varchar(255) DEFAULT NULL,
  `DEPLOYMENT_ID` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table inner_satisfaction.databasechangelog: ~20 rows (approximately)
/*!40000 ALTER TABLE `databasechangelog` DISABLE KEYS */;
INSERT INTO `databasechangelog` (`ID`, `AUTHOR`, `FILENAME`, `DATEEXECUTED`, `ORDEREXECUTED`, `EXECTYPE`, `MD5SUM`, `DESCRIPTION`, `COMMENTS`, `TAG`, `LIQUIBASE`, `CONTEXTS`, `LABELS`, `DEPLOYMENT_ID`) VALUES
	('01-0', 'mohsin.kerai', 'classpath:db/changelog/01-basic-tables.xml', '2018-02-25 01:29:58', 1, 'EXECUTED', '7:4a086a27235b627847898e628b63d8c7', 'createTable tableName=company; insert tableName=company; insert tableName=company; insert tableName=company', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('01-1', 'mohsin.kerai', 'classpath:db/changelog/01-basic-tables.xml', '2018-02-25 01:29:58', 2, 'EXECUTED', '7:45e3f1e3a77d46ae5ebcb31a313b2a8e', 'createTable tableName=level_type; insert tableName=level_type; insert tableName=level_type; insert tableName=level_type; insert tableName=level_type', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('01-2', 'mohsin.kerai', 'classpath:db/changelog/01-basic-tables.xml', '2018-02-25 01:29:58', 3, 'EXECUTED', '7:08f7151011134d179b9a4ecd7062bc52', 'createTable tableName=level; insert tableName=level; insert tableName=level; insert tableName=level; insert tableName=level', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('01-3', 'mohsin.kerai', 'classpath:db/changelog/01-basic-tables.xml', '2018-02-25 01:29:58', 4, 'EXECUTED', '7:ab4c86d388fbb18b9d2ae17e717ffc09', 'createTable tableName=level_active; insert tableName=level_active; insert tableName=level_active', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('01-4', 'mohsin.kerai', 'classpath:db/changelog/01-basic-tables.xml', '2018-02-25 01:29:58', 5, 'EXECUTED', '7:12a667edc1e6c8532e255e19669c13bc', 'createTable tableName=position; insert tableName=position; insert tableName=position', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('01-5', 'mohsin.kerai', 'classpath:db/changelog/01-basic-tables.xml', '2018-02-25 01:29:59', 6, 'EXECUTED', '7:0d7fe706e5f59fa4f357b2b2a3da18b9', 'createTable tableName=cycle; insert tableName=cycle', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('02-01', 'mohsin.kerai', 'classpath:db/changelog/02-basic-tables.xml', '2018-02-25 01:29:59', 7, 'EXECUTED', '7:7ad6584b484ce5359b51e7713d09bffa', 'createTable tableName=position_on_active_level; insert tableName=position_on_active_level', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('02-02', 'mohsin.kerai', 'classpath:db/changelog/02-basic-tables.xml', '2018-02-25 01:29:59', 8, 'EXECUTED', '7:42988f7c20172dd84fdcca327937fb1a', 'createTable tableName=person', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('02-03', 'mohsin.kerai', 'classpath:db/changelog/02-basic-tables.xml', '2018-02-25 01:29:59', 9, 'EXECUTED', '7:7f8ae59ba55cda7d3e25065ce42b0269', 'insert tableName=person; insert tableName=person', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('02-04', 'mohsin.kerai', 'classpath:db/changelog/02-basic-tables.xml', '2018-02-25 01:29:59', 10, 'EXECUTED', '7:87fdc399215dbf47941ca93764759ab0', 'createTable tableName=relation', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('02-05', 'mohsin.kerai', 'classpath:db/changelog/02-basic-tables.xml', '2018-02-25 01:29:59', 11, 'EXECUTED', '7:2fda9356f6f02be324be8e42e43b042f', 'insert tableName=relation', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('02-06', 'mohsin.kerai', 'classpath:db/changelog/02-basic-tables.xml', '2018-02-25 01:29:59', 12, 'EXECUTED', '7:b1418c2ab881acfa30f2993922d6281d', 'createTable tableName=person_relation_person', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('02-07', 'mohsin.kerai', 'classpath:db/changelog/02-basic-tables.xml', '2018-02-25 01:29:59', 13, 'EXECUTED', '7:a2a555db512ef6c50e8ef3f4bf96356f', 'insert tableName=person_relation_person', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('02-08', 'mohsin.kerai', 'classpath:db/changelog/02-basic-tables.xml', '2018-02-25 01:29:59', 14, 'EXECUTED', '7:b1de271de1da4bb389be930b10f970c0', 'createTable tableName=cycle_position_on_active_level', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('02-09', 'mohsin.kerai', 'classpath:db/changelog/02-basic-tables.xml', '2018-02-25 01:29:59', 15, 'EXECUTED', '7:2816d6be0164283221ffaa543c80dcc3', 'insert tableName=cycle_position_on_active_level', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('03-01', 'mohsin.kerai', 'classpath:db/changelog/03-basic-tables.xml', '2018-02-25 01:29:59', 16, 'EXECUTED', '7:d61eb82edc67bd13d9956a4889a57d11', 'createTable tableName=cpal_nomination', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('03-02', 'mohsin.kerai', 'classpath:db/changelog/03-basic-tables.xml', '2018-02-25 01:29:59', 17, 'EXECUTED', '7:b7348d57433767cefe37ff9d6f3af8cc', 'insert tableName=cpal_nomination', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('03-03', 'mohsin.kerai', 'classpath:db/changelog/03-basic-tables.xml', '2018-02-25 01:29:59', 18, 'EXECUTED', '7:fa5da046b3bafd54fcf578ba35572044', 'createTable tableName=cpal_awardee', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('03-04', 'mohsin.kerai', 'classpath:db/changelog/03-basic-tables.xml', '2018-02-25 01:29:59', 19, 'EXECUTED', '7:e65370a0369f68e8bf3864c2c5eec492', 'insert tableName=cpal_awardee', '', NULL, '3.5.3', NULL, NULL, '9504198019'),
	('03-05', 'mohsin.kerai', 'classpath:db/changelog/03-basic-tables.xml', '2018-02-25 01:29:59', 20, 'EXECUTED', '7:fc2433dcde6378c237fd22b3999de9f2', 'insert tableName=position_on_active_level; insert tableName=cycle_position_on_active_level; insert tableName=person; insert tableName=person; insert tableName=cpal_nomination; insert tableName=cpal_nomination; insert tableName=cpal_nomination', '', NULL, '3.5.3', NULL, NULL, '9504198019');
/*!40000 ALTER TABLE `databasechangelog` ENABLE KEYS */;

-- Dumping structure for table inner_satisfaction.databasechangeloglock
CREATE TABLE IF NOT EXISTS `databasechangeloglock` (
  `ID` int(11) NOT NULL,
  `LOCKED` bit(1) NOT NULL,
  `LOCKGRANTED` datetime DEFAULT NULL,
  `LOCKEDBY` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table inner_satisfaction.databasechangeloglock: ~1 rows (approximately)
/*!40000 ALTER TABLE `databasechangeloglock` DISABLE KEYS */;
INSERT INTO `databasechangeloglock` (`ID`, `LOCKED`, `LOCKGRANTED`, `LOCKEDBY`) VALUES
	(1, b'0', NULL, NULL);
/*!40000 ALTER TABLE `databasechangeloglock` ENABLE KEYS */;

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

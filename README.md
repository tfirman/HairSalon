# Hair Salon

####An app designed as a customer database for a hair salon

#### By Tim Firman

## Description

_This application is intended to operate a customer database for a hair salon.  The database will contain a list of stylists who work at the salon, and a list of client for each stylist.  Per the assignment instructions, each client will associate with exactly one stylist.  Lists of both stylists and clients will be maintained, and the user can add to either list.

##Specifications:

* The database and interface will support adding a new stylist

* A stylist will be saved with a field for additional details, and may initially have no clients

* The database and interface will support a query for a list of all stylists

* The database and interface will support a query for details on a particular stylist, including a list of clients, if any

* Support for the addition of clients will be implemented, where each client must always be assigned to exactly one stylist

## Setup/Installation Requirements

_to be added_

##Setup
This app uses a MySQL database.  To set this up, run the following commands on a MySQL Server:

CREATE DATABASE tim_firman;
USE tim_firman;
CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255), description VARCHAR(255));
CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylistid BIGINT);

CREATE DATABASE IF NOT EXISTS `tim_firman_test` DEFAULT CHARSET=utf8 COLLATE utf8_general_ci;
CREATE TABLE `tim_firman_test`.`clients` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `stylistid` bigint(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
ALTER TABLE `tim_firman_test`.`clients` ADD PRIMARY KEY (`id`), ADD UNIQUE KEY `id` (`id`);
ALTER TABLE `tim_firman_test`.`clients` MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;
CREATE TABLE `tim_firman_test`.`stylists` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
ALTER TABLE `tim_firman_test`.`stylists` ADD PRIMARY KEY (`id`), ADD UNIQUE KEY `id` (`id`);
ALTER TABLE `tim_firman_test`.`stylists` MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

## Known Bugs

None Known

## Technologies Used

_This is a MVC C# server_

### License

Copyright (c) 2018 **_Tim Firman_**

This software is licensed under the GPL license.

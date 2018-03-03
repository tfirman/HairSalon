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

* The database and interface will support the deletion of each or all of the stylists and clients

* The database and interface will provide a view of all and individual views of each particular client

* The name of each stylist must be editable, as well as all details of each client

* A specialty must be created and along with view pages for all and each specialty

* Each stylist may have one or more specialties added

* The individual view for each stylist must list specialties, and the individual view for each specialty must list the matching stylists

## Setup/Installation Requirements

##Setup
This app uses a MySQL database.  To set this up, run the following commands on a MySQL Server:

CREATE DATABASE tim_firman;
USE tim_firman;
CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylistid BIGINT);
CREATE TABLE specialties (id serial PRIMARY KEY, name VARCHAR(255));
CREATE TABLE specialties_stylists (id serial PRIMARY KEY, specialty_id INT, stylist_id INT);
CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255), description VARCHAR(255));

CREATE DATABASE tim_firman_test;
USE tim_firman_test;
CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylistid BIGINT);
CREATE TABLE specialties (id serial PRIMARY KEY, name VARCHAR(255));
CREATE TABLE specialties_stylists (id serial PRIMARY KEY, specialty_id INT, stylist_id INT);
CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255), description VARCHAR(255));

Once the database is set up as described above, a dotnet restore command will be needed to restore the object files for the app, then the app should be run (dotnet run)

## Known Bugs

In spite of the specification request, not "ALL" of the client information is editable - specifically the unique client ID is not editable, because that would be silly.

It is possible for the user to add a Stylist to a Specialty more than once, and vice versa.

## Technologies Used

_This is an MVC C# server, accessing a MySQL database_

### License

Copyright (c) 2018 **_Tim Firman_**

This software is licensed under the GPL license.

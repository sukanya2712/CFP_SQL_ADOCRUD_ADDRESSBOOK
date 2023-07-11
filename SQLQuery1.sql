CREATE DATABASE AddressBooKk;

use AddressBooKk;
CREATE TABLE contacts(
ID int primary key Identity(1,1),
FirstName varchar(50),
LastName varchar(50),
Email varchar(50),
PhoneNumber varchar(50),
City varchar(50),
SState varchar(50),
Zip varchar(50),
)

SELECT * From contacts

DROP DATABASE AddressBook;


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

-----------------------------------------------------------------STORE PROCEDURE ADDCONTACT-----------------------------------------------------------------------

CREATE PROCEDURE AddOrderNewCustomer
    @FirstName VARCHAR(100),
    @LastName VARCHAR(100),
    @Email VARCHAR(20),
	@PhoneNumber VARCHAR(20),
	@City VARCHAR(20),
	@State VARCHAR(20),
	@Zip VARCHAR(20)
AS
BEGIN
    INSERT INTO Contacts VALUES(@FirstName,@LastName,@Email,@PhoneNumber,@City,@State,@Zip)
END





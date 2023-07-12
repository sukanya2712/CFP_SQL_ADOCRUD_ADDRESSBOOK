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

CREATE PROCEDURE AddOrderNewCustomerr
    @FirstName VARCHAR(100),
    @LastName VARCHAR(100),
    @Email VARCHAR(20),
	@PhoneNumber VARCHAR(20),
	@City VARCHAR(20),
	@SState VARCHAR(20),
	@Zip VARCHAR(20)
AS
BEGIN
    INSERT INTO Contacts VALUES(@FirstName,@LastName,@Email,@PhoneNumber,@City,@SState,@Zip)
END

--------------------------------------------------------------STORE PROCEDURE DISPLAYCONTACT-----------------------------------------------------------------------
CREATE PROCEDURE DisplayContact
AS
BEGIN
      Select * from contacts
END

-------------------------------------------------------------TRANSACTIONAL STORE-PRO EDITCONTACT-------------------------------------------------------------------
CREATE PROCEDURE EditContact
    @Id INT,
    @FirstName VARCHAR(100),
    @LastName VARCHAR(100),
    @Email VARCHAR(20),
	@PhoneNumber VARCHAR(20),
	@City VARCHAR(20),
	@SState VARCHAR(20),
	@Zip VARCHAR(20)
AS
BEGIN
    UPDATE Contacts
    SET FirstName = @FirstName,
	    LastName  = @LastName,
        Email = @Email,
        PhoneNumber = @PhoneNumber,
		City = @City,
		SState = @SState,
		Zip = @Zip

    WHERE Id = @Id
END





create database PeopleInformation
--------------------------------------------
use PeopleInformation

CREATE TABLE Persons (
    PersonID int Identity(1,1),
    LastName varchar(50),
    FirstName varchar(50),
    Address varchar(50),
    EmailAddress varchar(50),
	PhoneNumber varchar(50),
	PostalCode int
);

Create Table Login
(
   loginID int Identity(1,1),
   FirstName varchar(50),
   LastName varchar(50),
   EmailAddress varchar(50),
	Password varchar(50)

)


INSERT INTO Persons (LastName, FirstName, Address, EmailAddress, PhoneNumber, PostalCode)
VALUES
    ('Doe', 'John', '123 Main St', 'john.doe@example.com', '1234567890', 12345),
    ('Smith', 'Jane', '456 Elm St', 'jane.smith@example.com', '9876543210', 54321),
    ('Johnson', 'Michael', '789 Oak St', 'michael.johnson@example.com', '5555555555', 55555),
    ('Williams', 'Emily', '321 Maple Ave', 'emily.williams@example.com', '1112223333', 11111),
    ('Brown', 'David', '654 Pine Rd', 'david.brown@example.com', '4444444444', 44444),
    ('Davis', 'Olivia', '987 Cedar Ln', 'olivia.davis@example.com', '7778889999', 77777),
    ('Miller', 'Sophia', '456 Birch Blvd', 'sophia.miller@example.com', '2223334444', 22222),
    ('Wilson', 'James', '789 Walnut St', 'james.wilson@example.com', '9998887777', 99999),
    ('Anderson', 'Ava', '123 Pine St', 'ava.anderson@example.com', '6667778888', 66666),
    ('Taylor', 'Liam', '456 Oak Ave', 'liam.taylor@example.com', '3332221111', 33333);


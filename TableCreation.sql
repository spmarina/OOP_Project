create database AutoRentDB
go

use AutoRentDB

go

create table Cars
(
	Cars_ID int Primary key identity,
	Brand nvarchar(50) not null,
	Model varchar(50) not null,
	Price DECIMAL(9,2) not null,
	Availability BIT not null
)


go


create table Customers
(
	Customers_ID int Primary key identity,
	LastName nvarchar(50) not null,
	FirstName nvarchar(50) not null,
	MiddleName nvarchar(50) not null,
	Phone nvarchar(20) not null,
	ActiveRent BIT not null,
)


go


create table Rents
(
	Rent_ID int Primary key identity,
	Customers_ID int foreign key references Customers(Customers_ID),
	Cars_ID int foreign key references Cars(Cars_ID),
	FirstDate datetime not null,
	LastDate datetime not null,
)

go

create table Cards
(
	Cards_ID int Primary key identity,
	Customers_ID int foreign key references Customers(Customers_ID),
	Cashback tinyint default 0 not null,
	Points bigint default 0 not null,
	Payment bigint default 0 not null,
)

go

create table Contracts
(
	Contracts_ID int Primary key identity,
	Customers_ID int foreign key references Customers(Customers_ID),
	Number BIGINT not null,
	CreateDate datetime not null,
	Cards_ID int foreign key references Cards(Cards_ID),
)

go


create table ServiceDate
(
	ServiceDate_ID int Primary key identity,
	Cars_ID int foreign key references Cars(Cars_ID),
	PreviousDate datetime not null,
	NextDate datetime not null,
)

go

create table Admins
(
	Admins_ID int Primary key identity,
	CreateLogin nvarchar(50) not null,
	CreatePassword nvarchar(50) not null,
	Sales bigint default 0 not null,
)


go



create table Discounts
(
	Discounts_ID int Primary key identity,
	Cars_ID int foreign key references Cars(Cars_ID),
	NewPrice tinyint default 0 not null,
)


go

---------------------------------------------------------

ALTER TABLE Contracts
ADD DocumentLink NVARCHAR(max) not null;
go

INSERT INTO Cars 
VALUES
('BMW', 'X7 M Sport', 14000, 1),
('Mercedes-Benz', 'GLS', 16500, 0),
('Audi', 'A7', 10000, 1)

go


INSERT INTO Customers 
VALUES
('Ivanov', 'Egor', 'Andreevich', +79173451789, 1),
('Emelyanova', 'Darya', 'Daniilovna', +79176661320, 1),
('Stolyarova', 'Yliya', 'Vladimirovna', +79654569302, 0)

go





INSERT INTO Cards 
VALUES
(1, 3, 400, 40000),
(2, 10, 1400, 140000),
(3, 5, 520, 52000)

go



ALTER TABLE ServiceDate
ALTER COLUMN PreviousDate DATE;
go

ALTER TABLE ServiceDate
ALTER COLUMN NextDate DATE;
go

ALTER TABLE Contracts
ALTER COLUMN CreateDate DATE;
go




INSERT INTO ServiceDate (Cars_ID, PreviousDate, NextDate)
VALUES
(1, '2022-05-29', '2024-11-29'),
(2, '2023-04-15', '2025-04-15'),
(3, '2022-08-24', '2024-08-14');

go

INSERT INTO Discounts 
VALUES
(1, 15),
(3, 5)

go

INSERT INTO Admins 
VALUES
('Michail', '12345678', 560000),
('Christina', 'L0veCars', 1245000)


go


INSERT INTO Contracts 
VALUES
(1, 567423, '2022-03-11', 1, 'https://www.youtube.com/watch?v=dQw4w9WgXcQ'),
(2, 891023, '2023-12-06', 2, 'https://www.youtube.com/watch?v=dQw4w9WgXcQ'),
(3, 459012, '2024-01-10', 3, 'https://www.youtube.com/watch?v=dQw4w9WgXcQ')


go

delete Rents;
go



----------------------------------------------------------------------

EXEC sp_rename 'Rent', 'Rents'

go


ALTER TABLE Rents
ALTER COLUMN FirstDate DATE;
go

ALTER TABLE Rents
ALTER COLUMN LastDate DATE;
go



INSERT INTO Rents 
VALUES
(1, 3, '29/05/2024', '01/06/2024'),
(2, 1, '01/06/2024', '03/06/2024');

go



EXEC sp_rename 'ServiceDate', 'ServicesDates'

go

INSERT INTO ServicesDates (Cars_ID, PreviousDate, NextDate)
VALUES
(1, '2022-05-29', '2024-11-29'),
(2, '2023-04-15', '2025-04-15'),
(3, '2022-08-24', '2024-08-14');

go

SELECT * FROM ServicesDates;

go
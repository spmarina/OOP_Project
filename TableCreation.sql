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


create table Rent
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


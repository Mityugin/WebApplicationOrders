CREATE DATABASE Orders_DB
GO

USE Orders_DB
GO

CREATE TABLE Clients
(
     Number int primary key identity,
     Name nvarchar(250),
     Address nvarchar(250),
	 Vip bit NOT NULL
)
GO

CREATE TABLE Goods
(
     Number int primary key identity,
	 Name nvarchar(250),
     Price decimal (20,2)
)
GO

CREATE TABLE Orders
(
     Number int primary key identity,
     TotalPrice decimal (20,2),
	 ClientNumber int NOT NULL,
     Description  nvarchar(250) NOT NULL 
)
GO


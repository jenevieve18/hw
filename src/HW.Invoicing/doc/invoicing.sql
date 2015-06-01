use invoicing;

create table Item(
	Id integer not null primary key identity,
	Name varchar(255)
);

create table [User](
	Id integer not null primary key identity,
	Name varchar(255),
	[Password] varchar(255)
);

create table Customer(
	Id integer not null primary key identity,
	Name varchar(255),
	[Address] varchar(255),
	Phone varchar(255),
	Email varchar(255)
);

create table Invoice(
	Id integer not null primary key identity,
	Date smalldatetime,
	CustomerId integer
);

create table InvoiceItem(
	Id integer not null primary key identity,
	InvoiceId integer,
	Quantity decimal,
	Price decimal
);

alter table Item add Description varchar(255);
alter table Item add Price decimal;

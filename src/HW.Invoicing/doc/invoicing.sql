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

create table CustomerPrice(
	Id integer not null primary key identity,
	CustomerId integer,
	ItemId integer,
	Price decimal
);

create table CustomerNotes(
	Id integer not null primary key identity,
	CustomerId integer,
	Notes varchar(255),
	CreatedAt datetime,
	CreatedBy integer
);

create table CustomerContact(
	Id integer not null primary key identity,
	CustomerId integer,
	Contact varchar(255),
	Phone varchar(255),
	Mobile varchar(255),
	Email varchar(255)
);

alter table Customer add Number varchar(255);

alter table Customer add Mobile varchar(255);

create table CustomerTimebook(
	Id integer not null primary key identity,
	CustomerId integer,
	CustomerContactId integer,
	ItemId integer,
	Quantity decimal,
	Price decimal,
	Consultant varchar(255),
	Comments varchar(255)
);

create table News(
	Id integer not null primary key identity,
	Content text
);

alter table News add Date datetime;

alter table CustomerNotes alter column Notes text;

alter table CustomerTimebook add Department varchar(255);

sp_rename 'CustomerPrice', 'CustomerItem';

create table Unit(
	Id integer not null primary key identity,
	Name varchar(255)
);

alter table Item add UnitId integer;

update Item set UnitId = 1;

create table Issue(
	Id integer not null primary key identity,
	Title varchar(255),
	Description text
);

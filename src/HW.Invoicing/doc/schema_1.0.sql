create database invoicing;
go

use invoicing;
go

create table Item(
	Id integer not null primary key identity,
	Name varchar(255)
);
go

create table [User](
	Id integer not null primary key identity,
	Name varchar(255),
	[Password] varchar(255)
);
go

create table Customer(
	Id integer not null primary key identity,
	Name varchar(255),
	[Address] varchar(255),
	Phone varchar(255),
	Email varchar(255)
);
go

create table Invoice(
	Id integer not null primary key identity,
	Date smalldatetime,
	CustomerId integer
);
go

create table InvoiceItem(
	Id integer not null primary key identity,
	InvoiceId integer,
	Quantity decimal,
	Price decimal
);
go

alter table Item add Description varchar(255);
go
alter table Item add Price decimal;
go

create table CustomerPrice(
	Id integer not null primary key identity,
	CustomerId integer,
	ItemId integer,
	Price decimal
);
go

create table CustomerNotes(
	Id integer not null primary key identity,
	CustomerId integer,
	Notes varchar(255),
	CreatedAt datetime,
	CreatedBy integer
);
go

create table CustomerContact(
	Id integer not null primary key identity,
	CustomerId integer,
	Contact varchar(255),
	Phone varchar(255),
	Mobile varchar(255),
	Email varchar(255)
);
go

alter table Customer add Number varchar(255);
go

alter table Customer add Mobile varchar(255);
go

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
go

create table News(
	Id integer not null primary key identity,
	Content text
);
go

alter table News add Date datetime;
go

alter table CustomerNotes alter column Notes text;
go

alter table CustomerTimebook add Department varchar(255);
go

sp_rename 'CustomerPrice', 'CustomerItem';
go

create table Unit(
	Id integer not null primary key identity,
	Name varchar(255)
);
go

alter table Item add UnitId integer;
go

update Item set UnitId = 1 where Id in (1, 2, 3, 4, 5);
go

create table Issue(
	Id integer not null primary key identity,
	Title varchar(255),
	Description text
);
go

alter table Customer alter [Address] PostalAddress varchar(255);
go

alter table CustomerTimebook add Date datetime;
go

alter table Item add Inactive tinyint;
go

alter table Item alter column Inactive integer;
go

alter table Issue add Status integer;
go

alter table Issue add Inactive integer;
go

alter table CustomerNotes add Inactive integer;
go

alter table CustomerItem add Inactive integer;
go

alter table CustomerContact add Inactive integer;
go

alter table Issue add CreatedAt datetime,
CreatedBy integer,
UpdatedAt datetime,
UpdatedBy integer;
go

update Issue set CreatedAt = GETDATE();
go

alter table CustomerContact add Type integer;
go

alter table Unit add Inactive integer;
go

alter table CustomerTimebook add Inactive integer;
go

alter table CustomerTimebook add Status integer;
go

alter table Customer add InvoiceAddress varchar(255),
PostalAddress varchar(255),
PurchaseOrderNumber varchar(255),
YourReferencePerson varchar(255),
OurReferencePerson varchar(255);
go

alter table [User] add Color varchar(255);
go

alter table Customer add Inactive integer;
go

create table Company(
	Id integer not null primary key identity,
	Name varchar(255),
	Address varchar(255),
	Phone varchar(255),
	BankAccountNumber varchar(255),
	TIN varchar(255)
);
go

create table InvoiceTimebook(
	Id integer not null primary key identity,
	InvoiceId integer,
	CustomerTimebookId integer
);
go

drop table InvoiceItem;
go

alter table CustomerTimebook add InternalComments text;
go

alter table CustomerItem add SortOrder integer;
go

alter table Invoice add Comments text;
go

create table GeneratedNumber(
	Id integer not null primary key identity,
	Invoice integer
);
go

alter table Invoice add Number varchar(255);
go

alter table CustomerTimebook add VAT decimal;
go

alter table CustomerTimebook alter column Comments text;
go

create table Lang(
	Id integer not null primary key identity,
	Name varchar(255)
);
go

insert into Lang(Name) values('SE');
go
insert into Lang(Name) values('EN');
go

alter table Customer add LangId integer;
go

update Customer set LangId = 2;
go

create table Currency(
	Id integer not null primary key identity,
	Code varchar(255),
	Name varchar(255)
);
go

alter table Invoice add MaturityDate datetime
go

alter table Invoice add Status integer
go

update Invoice set Status = 1;
go

alter table Company add FinancialMonthStart datetime,
FinancialMonthEnd datetime;
go

alter table Invoice add InternalComments text;
go

alter table Invoice add Exported integer;
go

alter table Lang add Code varchar(255);
go

update Lang set Code = Name;
go
update Lang set Name = 'Svenska' where Id = 1;
go
update Lang set Name = 'English' where Id = 2;
go

alter table Customer add HasSubscription integer;
go

alter table Customer add SubscriptionItemId integer,
SubscriptionStartDate datetime,
SubscriptionEndDate datetime;
go

alter table Customer add SubscriptionHasEndDate integer;
go

alter table CustomerTimebook add SubscriptionStartDate date,
SubscriptionEndDate date;
go

alter table CustomerTimebook add IsSubscription integer;
go

alter table Company add UserId integer;
go

update Issue set Status = 4 where Status = 3;
go
update Issue set Status = 3 where Status = 2;
go
update Issue set Status = 2 where Status = 1;
go

alter table Company add InvoicePrefix varchar(255);
go

alter table Customer add CompanyId integer;
go

update Customer set CompanyId = 1;
go

alter table Item add CompanyId integer;
go

update Item set CompanyId = 1;
go

alter table Unit add CompanyId integer;
go

update Unit set CompanyId = 1;
go

alter table GeneratedNumber add CompanyId integer;
go

update GeneratedNumber set CompanyId = 1;
go

alter table Company add Selected integer;
go

alter table Company add HasSubscriber integer;
go

alter table Company add InvoiceLogo varchar(255);
go

alter table Company add InvoiceTemplate varchar(255);
go

alter table CustomerTimebook alter column Quantity decimal(16, 2);
go

alter table Company add Terms text;
go

alter table Company add Signature varchar(255);
go

alter table Company add AgreementEmailText text;
go

create table CustomerAgreement(
	Id integer not null primary key identity,
	CustomerId integer,
	Date datetime
);
go

alter table Company add AgreementEmailSubject text;
go

alter table CustomerAgreement add Lecturer varchar(255),
LectureDate datetime,
Runtime varchar(255),
LectureTitle varchar(255),
Location varchar(255),
Contact varchar(255),
Mobile varchar(255),
Email varchar(255),
Compensation varchar(255),
PaymentTerms varchar(255),
BillingAddress varchar(255),
OtherInformation text;
go

alter table CustomerAgreement drop column LectureDate;
go

alter table Company add Email varchar(255);
go

alter table Company add AgreementPrefix varchar(255);
go

alter table Company add OrganizationNumber varchar(255);
go

alter table CustomerAgreement add IsClosed int;
go

create table CustomerAgreementDateTimeAndPlace(
	Id integer not null primary key identity,
	CustomerAgreementId integer,
	Date datetime,
	TimeFrom varchar(255),
	TimeTo varchar(255),
	Runtime varchar(255),
	Address varchar(255)
);
go

update CustomerAgreement set Compensation = null;
go

alter table CustomerAgreement alter column Compensation decimal(18, 2);
go

alter table CustomerAgreement add ContactPlaceSigned varchar(255),
ContactDateSigned datetime,
ContactName varchar(255),
ContactTitle varchar(255),
ContactCompany varchar(255),
DateSigned datetime;
go

alter table Company add AgreementSignedEmailText text,
AgreementSignedEmailSubject text;
go
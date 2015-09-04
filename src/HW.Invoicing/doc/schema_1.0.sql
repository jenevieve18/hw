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

update Item set UnitId = 1 where Id in (1, 2, 3, 4, 5);

create table Issue(
	Id integer not null primary key identity,
	Title varchar(255),
	Description text
);

alter table Customer alter Address PostalAddress varchar(255);

alter table CustomerTimebook add Date datetime;

alter table Item add Inactive tinyint;

alter table Item alter column Inactive integer;

alter table Issue add Status integer;

alter table Issue add Inactive integer;

alter table CustomerNotes add Inactive integer;

alter table CustomerItem add Inactive integer;

alter table CustomerContact add Inactive integer;

alter table Issue add CreatedAt datetime,
CreatedBy integer,
UpdatedAt datetime,
UpdatedBy integer;

update Issue set CreatedAt = GETDATE();

alter table CustomerContact add Type integer;

alter table Unit add Inactive integer;

alter table CustomerTimebook add Inactive integer;

alter table CustomerTimebook add Status integer;

alter table Customer add InvoiceAddress varchar(255),
PostalAddress varchar(255),
PurchaseOrderNumber varchar(255),
YourReferencePerson varchar(255),
OurReferencePerson varchar(255);

alter table [User] add Color varchar(255);

alter table Customer add Inactive integer;

create table Company(
	Id integer not null primary key identity,
	Name varchar(255),
	Address varchar(255),
	Phone varchar(255),
	BankAccountNumber varchar(255),
	TIN varchar(255)
);

create table InvoiceTimebook(
	Id integer not null primary key identity,
	InvoiceId integer,
	CustomerTimebookId integer
);

drop table InvoiceItem;

alter table CustomerTimebook add InternalComments text;

alter table CustomerItem add SortOrder integer;

alter table Invoice add Comments text;

create table GeneratedNumber(
	Id integer not null primary key identity,
	Invoice integer
);

alter table Invoice add Number varchar(255);

alter table CustomerTimebook add VAT decimal;

alter table CustomerTimebook alter column Comments text;

create table Lang(
	Id integer not null primary key identity,
	Name varchar(255)
);

insert into Lang(Name) values('SE');
insert into Lang(Name) values('EN');

alter table Customer add LangId integer;

update Customer set LangId = 2;

create table Currency(
	Id integer not null primary key identity,
	Code varchar(255),
	Name varchar(255)
);

alter table Invoice add MaturityDate datetime;

alter table Invoice add Status integer;

update Invoice set Status = 1;

alter table Company add FinancialMonthStart datetime,
FinancialMonthEnd datetime;

alter table Invoice add InternalComments text;

alter table Invoice add Exported integer;

alter table Lang add Code varchar(255);

update Lang set Code = Name;
update Lang set Name = 'Svenska' where Id = 1;
update Lang set Name = 'English' where Id = 2;

alter table Customer add HasSubscription integer;

alter table Customer add SubscriptionItemId integer,
SubscriptionStartDate datetime,
SubscriptionEndDate datetime;

alter table Customer add SubscriptionHasEndDate integer;

alter table CustomerTimebook add SubscriptionStartDate date,
SubscriptionEndDate date;

alter table CustomerTimebook add IsSubscription integer;

alter table Company add UserId integer;

update Issue set Status = 4 where Status = 3;
update Issue set Status = 3 where Status = 2;
update Issue set Status = 2 where Status = 1;

alter table Company add InvoicePrefix varchar(255);

alter table Customer add CompanyId integer;

update Customer set CompanyId = 1;

alter table Item add CompanyId integer;

update Item set CompanyId = 1;

alter table Unit add CompanyId integer;

update Unit set CompanyId = 1;

alter table GeneratedNumber add CompanyId integer;

update GeneratedNumber set CompanyId = 1;

alter table Company add Selected integer;

alter table Company add HasSubscriber integer;

alter table Company add InvoiceLogo varchar(255);

alter table Company add InvoiceTemplate varchar(255);

alter table CustomerTimebook alter column Quantity decimal(16, 2);

alter table Company add Terms text;

alter table Company add Signature varchar(255);

alter table Company add AgreementEmailText text;

create table CustomerAgreement(
	Id integer not null primary key identity,
	CustomerId integer,
	Date datetime
);

alter table Company add AgreementEmailSubject text;

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

alter table CustomerAgreement drop column LectureDate;

alter table Company add Email varchar(255);

alter table Company add AgreementPrefix varchar(255);

alter table Company add OrganizationNumber varchar(255);

alter table CustomerAgreement add IsClosed int;

create table CustomerAgreementDateTimeAndPlace(
	Id integer not null primary key identity,
	CustomerAgreementId integer,
	Date datetime,
	TimeFrom varchar(255),
	TimeTo varchar(255),
	Runtime varchar(255),
	Address varchar(255)
);

update CustomerAgreement set Compensation = null;

alter table CustomerAgreement alter column Compensation decimal(18, 2);

alter table CustomerAgreement add ContactPlaceSigned varchar(255),
ContactDateSigned datetime,
ContactName varchar(255),
ContactTitle varchar(255),
ContactCompany varchar(255),
DateSigned datetime;

alter table Company add AgreementSignedEmailText text,
AgreementSignedEmailSubject text;

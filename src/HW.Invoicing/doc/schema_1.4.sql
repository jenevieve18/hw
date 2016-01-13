use invoicing;

alter table Customer add Status integer;
go

update Customer set Status = 1
where Inactive = 1;
go

alter table Customer drop column Inactive;
go

create table UserLink(
	Id integer not null primary key identity,
	UserId integer,
	Link integer
);
go

create table UserCompany(
	Id integer not null primary key identity,
	UserId integer,
	CompanyId integer
);
go

create table UserCompanyLink(
	Id integer not null primary key identity,
	UserId integer,
	CompanyId integer,
	Link integer
);
go

alter table CustomerTimebook add IsHeader integer;
go

alter table InvoiceTimebook add SortOrder integer;
go

alter table Company add InvoiceEmail varchar(255);
go

alter table Company add InvoiceEmailCC varchar(255);
go

alter table Company add InvoiceEmailSubject varchar(255);
go

alter table Company add InvoiceEmailText varchar(255);
go

alter table Customer add InvoiceEmail varchar(255);
go

alter table Customer add InvoiceEmailCC varchar(255);
go

alter table CustomerContact add PurchaseOrderNumber varchar(255);
go

alter table Customer add ContactPersonId integer;
go

alter table Item add Consultant varchar(255);
go

create table IssueComment(
	Id integer not null primary key identity,
	IssueId integer,
	Comments text,
	UserId integer
);
go

alter table Company alter column InvoiceEmailText text;
go

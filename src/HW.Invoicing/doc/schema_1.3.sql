use invoicing;

alter table Company add Website varchar(255);
go

alter table Company add InvoiceExporter integer;
go

alter table [User] add SelectedCompany integer;
go
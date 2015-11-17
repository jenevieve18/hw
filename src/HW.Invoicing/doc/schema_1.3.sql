use invoicing;

alter table Company add Website varchar(255);

alter table Company add InvoiceExporter integer;

alter table [User] add SelectedCompany integer;

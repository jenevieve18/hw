use invoicing;

exec sp_rename 'Customer.LangId', 'Language', 'COLUMN';
go

alter table Customer add Currency integer;
go

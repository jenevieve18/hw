use invoicing;

alter table CustomerContact add Title varchar(255);
go

alter table Company add AgreementTemplate varchar(255);
go

exec sp_rename 'User.[Name]', 'Username', 'COLUMN';
go

alter table [User] add Name varchar(255);
go
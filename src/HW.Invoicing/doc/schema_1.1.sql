use invoicing;

alter table CustomerContact add Title varchar(255);

alter table Company add AgreementTemplate varchar(255);

exec sp_rename 'User.[Name]', 'Username', 'COLUMN';

alter table [User] add Name varchar(255);

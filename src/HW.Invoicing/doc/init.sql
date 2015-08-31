use invoicing;

insert into [User](Name, [Password]) values('Danx', 'Start123!!!');

insert into Company(Name, UserId) values('Interactive Health Group in Stockholm AB', 1);

insert into GeneratedNumber(Invoice, CompanyId) values(300, 1); -- If the latest is 300 and the next invoice is 301
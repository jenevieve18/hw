use invoicing;

insert into [User](Name, Username, [Password])
values('Dan', 'Dan', 'Start123!!!');
go

insert into Company(Name, UserId, FinancialMonthStart, FinancialMonthEnd, InvoicePrefix)
values('Interactive Health Group in Stockholm AB', 1, GETDATE(), DATEADD(YEAR, 1, GETDATE()), 'IHGF');
go

insert into GeneratedNumber(Invoice, CompanyId)
values(300, 1);
go -- If the latest is 300 and the next invoice is 301
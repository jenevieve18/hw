use invoicing;

alter table CustomerTimebook add DateHidden int;
go

create table Milestone(
	Id integer not null primary key identity,
	Name varchar(255)
);
go

alter table Issue add MilestoneId integer;
go

alter table Issue add Priority integer;
go

alter table CustomerTimebook alter column Price decimal(16,2);
alter table CustomerItem alter column Price decimal(16,2);
go
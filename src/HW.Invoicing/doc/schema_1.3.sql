use invoicing;

create table Milestone(
	Id integer not null primary key identity,
	Name varchar(255)
);

alter table Issue add Priority integer;

alter table CustomerTimebook alter column Price decimal(16,2);
alter table CustomerItem alter column Price decimal(16,2);

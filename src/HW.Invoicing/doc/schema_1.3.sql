use invoicing;

create table Milestone(
	Id integer not null primary key identity,
	Name varchar(255)
);

alter table Issue add Priority integer;

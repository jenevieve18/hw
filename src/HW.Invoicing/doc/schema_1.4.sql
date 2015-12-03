use invoicing;

alter table Customer add Status integer;

update Customer set Status = 1
where Inactive = 1;

alter table Customer drop column Inactive;

create table UserLink(
	Id integer not null primary key identity,
	UserId integer,
	Link integer
);
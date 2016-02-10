use healthWatch;

alter table Exercise add Status integer;
go

create table FAQ(
	FAQID integer not null primary key identity,
	Name varchar(255)
);
go

create table FAQLang(
	FAQLangID integer not null primary key identity,
	FAQID integer,
	LangID integer,
	Question varchar(255),
	Answer text
);
go

alter table Issue add Status integer;
go

alter table Exercise add Script text;
go

alter table SuperSponsor add Comment varchar(255);
go

alter table Sponsor add Comment varchar(255);
go

alter table SponsorProjectRoundUnit add OnlyEveryDays integer;
go

alter table SponsorProjectRoundUnit add GoToStatistics integer;
go

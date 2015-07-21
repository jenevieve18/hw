use healthWatch;

alter table Exercise add Status integer;

create table FAQ(
	FAQID integer not null primary key identity,
	Name varchar(255)
);

create table FAQLang(
	FAQLangID integer not null primary key identity,
	FAQID integer,
	LangID integer,
	Question varchar(255),
	Answer text
);

alter table Issue add Status integer;

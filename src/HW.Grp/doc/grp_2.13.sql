use healthWatch;

SET IDENTITY_INSERT ManagerFunction ON 
INSERT INTO ManagerFunction(ManagerFunctionID, ManagerFunction, URL, Expl) 
VALUES (9, 'My Exercises', 'myexercise.aspx', 'Manager Exercises') 
SET IDENTITY_INSERT ManagerFunction OFF

insert into ManagerFunctionLang(ManagerFunctionID, ManagerFunction, URL, Expl, LangID)
values(9, 'Chef övningar', 'myexercise.aspx', 'Chef övningar', 1);
insert into ManagerFunctionLang(ManagerFunctionID, ManagerFunction, URL, Expl, LangID)
values(9, 'My Exercises', 'myexercise.aspx', 'My Exercises', 2);

create table SponsorAdminExercise(
	SponsorAdminExerciseID integer not null primary key identity,
	Date datetime,
	SponsorAdminID integer,
	ExerciseVariantLangID integer
);

create table SponsorAdminExerciseDataInput(
	SponsorAdminExerciseDataInputID integer not null primary key identity,
	SponsorAdminExerciseID integer,
	Content text,
	[Order] integer
);

create table UserSession(
	UserSessionID integer not null primary key identity,
	UserHostAddress varchar(255),
	UserAgent varchar(255)
);

alter table UserSession add LangID integer;

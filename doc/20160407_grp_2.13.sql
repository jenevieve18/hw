use healthWatch;

--SET IDENTITY_INSERT ManagerFunction ON 
--INSERT INTO ManagerFunction(ManagerFunctionID, ManagerFunction, URL, Expl) 
--VALUES (9, 'My Exercises', 'myexercise.aspx', 'Manager Exercises') 
--SET IDENTITY_INSERT ManagerFunction OFF

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

insert into ManagerFunctionLang(ManagerFunctionID, ManagerFunction, URL, Expl, LangID)
values(1, 'Organization', 'org.aspx', 'administer units and users', 4);
insert into ManagerFunctionLang(ManagerFunctionID, ManagerFunction, URL, Expl, LangID)
values(2, 'Statistics', 'stats.aspx', 'view results and compare groups', 4);
insert into ManagerFunctionLang(ManagerFunctionID, ManagerFunction, URL, Expl, LangID)
values(3, 'Messages', 'messages.aspx', 'administer messages, invitations and reminders', 4);
insert into ManagerFunctionLang(ManagerFunctionID, ManagerFunction, URL, Expl, LangID)
values(4, 'Managers', 'managers.aspx', 'administer unit managers', 4);
insert into ManagerFunctionLang(ManagerFunctionID, ManagerFunction, URL, Expl, LangID)
values(7, 'Exercises', 'exercise.aspx', 'manager exercises', 4);
insert into ManagerFunctionLang(ManagerFunctionID, ManagerFunction, URL, Expl, LangID)
values(8, 'Reminders', 'reminders.aspx', 'reminders settings', 4);
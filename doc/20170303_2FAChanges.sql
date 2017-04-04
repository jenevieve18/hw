use healthwatch;

alter table [User] add Enable2FA integer;

alter table Sponsor add Enable2FA integer;

--drop table UserLogin;
create table UserLogin(
	UserLoginID integer not null primary key identity,
	UserID integer,
	IPAddress varchar(255),
	LoginAttempt datetime,
	ResourceID varchar(255),
	UserToken varchar(255),
	ActiveLoginAttempt integer,
	Unblocked integer,
	FromWebService integer,
	FromWebsite integer
);

--drop table UserSecret;
create table UserSecret(
	UserSecretID integer not null primary key identity,
	UserID integer,
	SecretKey varchar(255)
);
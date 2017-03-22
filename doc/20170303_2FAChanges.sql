use healthwatch;

alter table [User] add Enable2FA integer;

alter table Sponsor add Enable2FA integer;

create table UserLogin(
	UserLoginAttemptID integer not null primary key identity,
	UserID integer,
	IPAddress varchar(255),
	LoginAttempt datetime,
	ResourceID varchar(255),
	SecretKey varchar(255)
);

alter table UserLogin add UserToken varchar(255);
use healthwatch;

alter table [User] add Enable2FA integer;

alter table Sponsor add Enable2FA integer;

create table UserSecret(
	UserSecretID integer not null primary key identity,
	UserID integer,
	SecretKey varchar(2048)
);

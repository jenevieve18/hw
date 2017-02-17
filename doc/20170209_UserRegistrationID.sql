use healthwatch
go

create table UserRegistrationID(
	UserRegistrationID integer not null primary key identity,
	UserID integer,
	RegistrationID varchar(255)
)
go

alter table UserRegistrationID add PhoneName varchar(255)
go

alter table UserRegistrationID add Inactive integer
go

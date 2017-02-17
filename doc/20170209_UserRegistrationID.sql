use healthwatch
go

create table UserRegistrationID(
	UserRegistrationID integer not null primary key identity,
	UserID integer,
	RegistrationID varchar(255)
)
go
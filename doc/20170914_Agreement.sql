use healthwatch;

create table UserAgreement(
	UserAgreementID integer not null primary key identity,
	UserID integer,
	AgreementID integer,
	AgreementDate datetime
);

create table Agreement(
	AgreementID integer not null primary key identity,
	EffectivityDate datetime,
	Name varchar(255),
	AgreementContentID integer,
	Type integer
);

create table AgreementLang(
	AgreementLangID integer not null primary key identity,
	AgreementID integer,
	Name varchar(255),
	LangID integer
);

create table AgreementContent(
	AgreementContentID integer not null primary key identity,
	Name varchar(255)
);

create table AgreementContentLang(
	AgreementContentLangID integer not null primary key identity,
	AgreementText text,
	Name varchar(255),
	AgreementContentID integer,
	LangID integer
);

use healthwatch;

create table SponsorAdminExercise(
	SponsorAdminExerciseID integer not null primary key identity,
	Date datetime,
	SponsorAdminID integer,
	ExerciseVariantLangID integer,
	Comments text
);

create table SponsorAdminExerciseDataInput(
	SponsorAdminExerciseDataInputID integer not null primary key identity,
	SponsorAdminExerciseID integer,
	ValueText text,
	SortOrder integer,
	ValueInt integer,
	Type integer
);

create table SponsorAdminExerciseDataInputComponent(
	SponsorAdminExerciseDataInputComponentID integer not null primary key identity,
	SponsorAdminExerciseDataInputID integer,
	ValueText varchar(255),
	SortOrder integer,
	ValueInt integer
);

alter table SponsorAdminExerciseDataInputComponent add Class varchar(255);

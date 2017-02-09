use healthwatch
go

alter table ExerciseLang add ExerciseContent text
go

create table SponsorExerciseDataInput(
	SponsorExerciseDataInputID integer not null primary key identity,
	Content text,
	SponsorID integer,
	[Order] integer,
	ExerciseVariantLangID integer
)
go
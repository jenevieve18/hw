use healthwatch

create table SponsorAdminExerciseComments(
	SponsorAdminExerciseCommentsID integer not null primary key identity,
	SponsorAdminExerciseID integer,
	Comments text
)
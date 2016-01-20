use healthWatch
go

alter table SponsorAdmin add UniqueKey varchar(255)
go

alter table SponsorExtendedSurvey add Answers int;
go

alter table Department add SortStringLength int;
go

alter table dbo.SponsorExtendedSurveyDepartment add Answers int;
go

alter table dbo.SponsorExtendedSurveyDepartment add Total int;
go

alter table SponsorAdmin add UniqueKeyUsed int
go

use eform
go
alter table PlotTypeLang add ShortName nvarchar(255)
go

update PlotTypeLang set ShortName = 'Line' where PlotTypeLangID = 1
go
update PlotTypeLang set ShortName = 'Line (± SD)' where PlotTypeLangID = 2
go
update PlotTypeLang set ShortName = 'Line (± 1.96 SD)' where PlotTypeLangID = 3
go
update PlotTypeLang set ShortName = 'BoxPlot (Min/Max)' where PlotTypeLangID = 4
go
update PlotTypeLang set ShortName = 'BoxPlot (Tukey)' where PlotTypeLangID = 5
go

use eForm
go
alter table PlotTypeLang add SupportsMultipleSeries int
go

update PlotTypeLang set SupportsMultipleSeries = 1 where PlotTypeLangID in (1, 2, 3) -- not including 4 and 5 because box plots don't support multiple series for now.
go

-- Missing Table when running new instance
USE [healthWatch]
GO

/****** Object:  Table [dbo].[SponsorProjectRoundUnitDepartment]    Script Date: 05/26/2014 11:10:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SponsorProjectRoundUnitDepartment](
	[SponsorProjectRoundUnitID] [int] NULL,
	[ReportID] [int] NULL,
	[DepartmentID] [int] NULL,
	[SponsorProjectRoundUnitDepartmentID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_SponsorProjectRoundUnitDepartment] PRIMARY KEY CLUSTERED 
(
	[SponsorProjectRoundUnitDepartmentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

use healthWatch
go
alter table SponsorAdminSession add EndDT datetime
go

USE [healthWatch]
GO

/****** Object:  Table [dbo].[Issue]    Script Date: 09/21/2014 17:01:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Issue](
	[IssueID] [int] IDENTITY(1,1) NOT NULL,
	[IssueDate] [smalldatetime] NULL,
	[Title] [varchar](255) NULL,
	[Description] [text] NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_Issue] PRIMARY KEY CLUSTERED 
(
	[IssueID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [healthWatch]
GO

/****** Object:  Table [dbo].[ManagerFunctionLang]    Script Date: 01/08/2015 10:08:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ManagerFunctionLang](
	[ManagerFunctionLangID] [int] IDENTITY(1,1) NOT NULL,
	[ManagerFunctionID] [int] NULL,
	[ManagerFunction] [varchar](64) NULL,
	[URL] [varchar](128) NULL,
	[Expl] [varchar](256) NULL,
	[LangID] [int] NULL,
 CONSTRAINT [PK_ManagerFunctionLang] PRIMARY KEY CLUSTERED 
(
	[ManagerFunctionLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

insert into ManagerFunctionLang(ManagerFunctionID, LangID, ManagerFunction, URL, Expl)
values(1, 1, 'Organisation', 'org.aspx', 'Administrera enheter och användare')
go
insert into ManagerFunctionLang(ManagerFunctionID, LangID, ManagerFunction, URL, Expl)
values(2, 1, 'Statistik', 'stats.aspx', 'Visa resultat och jämför grupper')
go
insert into ManagerFunctionLang(ManagerFunctionID, LangID, ManagerFunction, URL, Expl)
values(3, 1, 'Meddelanden', 'messages.aspx', 'Administrera meddelanden, inbjudningar och påminnelser')
go
insert into ManagerFunctionLang(ManagerFunctionID, LangID, ManagerFunction, URL, Expl)
values(4, 1, 'Chefer', 'managers.aspx', 'Administrera enheternas chefer')
go
insert into ManagerFunctionLang(ManagerFunctionID, LangID, ManagerFunction, URL, Expl)
values(7, 1, 'Övningar', 'exercise.aspx', 'Chefsövningar')
go

insert into ManagerFunctionLang(ManagerFunctionID, LangID, ManagerFunction, URL, Expl)
values(1, 2, 'Organization', 'org.aspx', 'administer units and users')
go
insert into ManagerFunctionLang(ManagerFunctionID, LangID, ManagerFunction, URL, Expl)
values(2, 2, 'Statistics', 'stats.aspx', 'view results and compare groups')
go
insert into ManagerFunctionLang(ManagerFunctionID, LangID, ManagerFunction, URL, Expl)
values(3, 2, 'Messages', 'messages.aspx', 'administer messages, invitations and reminders')
go
insert into ManagerFunctionLang(ManagerFunctionID, LangID, ManagerFunction, URL, Expl)
values(4, 2, 'Managers', 'managers.aspx', 'administer unit managers')
go
insert into ManagerFunctionLang(ManagerFunctionID, LangID, ManagerFunction, URL, Expl)
values(7, 2, 'Exercises', 'exercise.aspx', 'manager exercises')
go

use healthWatch
go
alter table SponsorAdmin add LastName varchar(255)
go
alter table SponsorAdmin add PermanentlyDeleteUsers int
go

use healthWatch
go
alter table SponsorAdmin add InviteSubject text
go
alter table SponsorAdmin add InviteTxt text
go
alter table SponsorAdmin add InviteReminderSubject text
go
alter table SponsorAdmin add InviteReminderTxt text
go
alter table SponsorAdmin add AllMessageSubject text
go
alter table SponsorAdmin add AllMessageBody text
go

use healthWatch
go
create table SponsorAdminExtendedSurvey(
	SponsorAdminExtendedSurveyID int primary key not null identity,
	SponsorAdminID int,
	EmailSubject text,
	EmailBody text,
	FinishedEmailSubject text,
	FinishedEmailBody text,
	InviteReminderLastSent smalldatetime,
	InviteLastSent smalldatetime,
	AllMessageLastSent smalldatetime,
	EmailLastSent smalldatetime,
	FinishedLastSent smalldatetime
)
go

use healthWatch
go
alter table Department add LoginDays int
go
alter table Department add LoginWeekday int
go

use healthWatch
go
insert into ManagerFunction(ManagerFunction, URL, Expl) values('Reminders', 'reminders.aspx', 'reminder settings')
go
insert into ManagerFunctionLang(ManagerFunctionID, ManagerFunction, URL, Expl, LangID) values(8, 'Påminnelser', 'reminders.aspx', 'reminders settings', 1)
go
insert into ManagerFunctionLang(ManagerFunctionID, ManagerFunction, URL, Expl, LangID) values(8, 'Reminders', 'reminders.aspx', 'reminders settings', 2)
go

use healthWatch
go
alter table SponsorAdminExtendedSurvey add SponsorExtendedSurveyID int
go

use healthWatch
go
alter table SponsorAdminExtendedSurvey drop column InviteLastSent
go
alter table SponsorAdminExtendedSurvey drop column InviteReminderLastSent
go
alter table SponsorAdminExtendedSurvey drop column AllMessageLastSent
go

use healthWatch
go
alter table SponsorAdmin add InviteLastSent smalldatetime
go
alter table SponsorAdmin add InviteReminderLastSent smalldatetime
go
alter table SponsorAdmin add AllMessageLastSent smalldatetime
go

-- Inserting initial Swedish plot types from English ones.
use eform
go
insert into plottypelang(plottypeid, langid, name, description)
select id, 1, name, description from plottype
go

use eForm
go
update PlotTypeLang set ShortName = 'Line' where PlotTypeLangID = 6
go
update PlotTypeLang set ShortName = 'Line (± SD)' where PlotTypeLangID = 7
go
update PlotTypeLang set ShortName = 'Line (± 1.96 SD)' where PlotTypeLangID = 8
go
update PlotTypeLang set ShortName = 'BoxPlot (Min/Max)' where PlotTypeLangID = 9
go
update PlotTypeLang set ShortName = 'BoxPlot (Tukey)' where PlotTypeLangID = 10
go

use healthwatch
go
alter table SponsorAdmin add LoginLastSent smalldatetime
go

CREATE FUNCTION FindDepartmentWithReminder(@DepartmentID INTEGER)
RETURNS TABLE
AS
RETURN (
	WITH SelectRecursiveDepartment(DepartmentID, Department, ParentDepartmentID, LoginDays, LoginWeekday, Level) AS (
		SELECT DepartmentID, Department, ParentDepartmentID, LoginDays, LoginWeekday, 0 AS Level
		FROM Department
		WHERE ParentDepartmentID IS NULL
		OR LoginDays IS NOT NULL
		OR LoginWeekday IS NOT NULL
		UNION ALL
		SELECT d.DepartmentID, d.Department, d.ParentDepartmentID, q.LoginDays, q.LoginWeekday, Level + 1 as Level
		FROM Department d
		INNER JOIN SelectRecursiveDepartment q ON d.ParentDepartmentID = q.DepartmentID
	)
	SELECT DepartmentID, Department, ParentDepartmentID, LoginDays, LoginWeekday, Level
	FROM SelectRecursiveDepartment
	WHERE DepartmentID = @DepartmentID
)
go
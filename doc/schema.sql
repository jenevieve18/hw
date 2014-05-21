USE [healthwatch]
GO

CREATE TABLE [dbo].[AdminNews](
 [AdminNewsID] [int] IDENTITY(1,1) NOT NULL,
 [DT] [datetime] NULL,
 [News] [nvarchar](max) NULL,
 CONSTRAINT [PK_AdminNews] PRIMARY KEY CLUSTERED 
(
 [AdminNewsID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [healthWatch]
GO

/****** Object:  Table [dbo].[SponsorAdminSession]    Script Date: 05/08/2014 22:16:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SponsorAdminSession](
	[SponsorAdminID] [int] NOT NULL,
	[DT] [datetime] NULL,
	[SponsorAdminSessionID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_SponsorAdminSession] PRIMARY KEY CLUSTERED 
(
	[SponsorAdminSessionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [healthWatch]
GO

/****** Object:  Table [dbo].[SponsorAdminSessionFunction]    Script Date: 05/08/2014 22:19:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SponsorAdminSessionFunction](
	[SponsorAdminSessionFunctionID] [int] IDENTITY(1,1) NOT NULL,
	[ManagerFunctionID] [int] NULL,
	[DT] [datetime] NULL,
	[SponsorAdminSessionID] [int] NULL,
 CONSTRAINT [PK_SponsorAdminSessionFunction] PRIMARY KEY CLUSTERED 
(
	[SponsorAdminSessionFunctionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

use healthWatch;
alter table Sponsor add MinUserCountToDisclose int;

use healthWatch;
alter table SponsorInvite add StoppedPercent int;

use healthwatch;
alter table Department add MinUserCountToDisclose int;

ALTER TABLE dbo.SponsorExtendedSurvey ADD Total int NULL
GO

UPDATE SponsorExtendedSurvey
SET SponsorExtendedSurvey.Total =
(SELECT COUNT(*) FROM SponsorInvite WHERE SponsorInvite.[Stopped] IS NULL AND SponsorInvite.SponsorID = SponsorExtendedSurvey.SponsorID)
WHERE SponsorExtendedSurvey.Total IS NULL OR SponsorExtendedSurvey.SponsorExtendedSurveyID IN
(SELECT s.SponsorExtendedSurveyID FROM SponsorExtendedSurvey s INNER JOIN eform..ProjectRound p ON s.ProjectRoundID = p.ProjectRoundID WHERE p.Closed IS NULL)

-- 04/10/2014
use eform;

create table PlotType(
	Id int not null primary key identity(1, 1),
	Name nvarchar(max),
	Description nvarchar(max)
);

insert into PlotType(Name, Description) values
('Line Chart', 'chart displaying mean values.'),
('Line Chart with Standard Deviation', 'chart displaying mean values with Standard Deviation whiskers. The SD is a theoretical statistical measure that illustrates the range (variation from the average) in which approximately 67 % of the responses are. A low standard deviation indicates that the responses tend to be very close to the mean (lower variation); a high standard deviation indicates that the responses are spread out over a large range of values.'),
('Line Chart with Standard Deviation and Confidence Interval', 'chart displaying mean values, including whiskers that in average covers 1.96 SD, i.e. a theoretical distribution of approximately 95% of observations.'),
('Box Plot Min/Max', 'median value chart, including one set of whiskers that covers 50% of observations, and another set of whiskers that captures min and max values'),
('Box Plot', 'median value chart, similar to the min/max BloxPlot but removes outlying extreme values');

USE [eForm]
GO

/****** Object:  Table [dbo].[PlotTypeLang]    Script Date: 4/29/2014 2:50:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PlotTypeLang](
	[PlotTypeLangID] [int] IDENTITY(1,1) NOT NULL,
	[PlotTypeID] [int] NULL,
	[LangID] [int] NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_PlotTypeLanguage] PRIMARY KEY CLUSTERED 
(
	[PlotTypeLangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

-- 04/27/2014: insert default plot types to english
use eform;
insert into plottypelang(plottypeid, langid, name, description)
select id, 2, name, description from plottype;


USE HealthWatch
GO
ALTER TABLE Sponsor ADD EmailFrom NVARCHAR(255)
GO

use eform;
alter table PlotTypeLang add ShortName nvarchar(255);

update PlotTypeLang set ShortName = 'Line' where PlotTypeLangID = 1;
update PlotTypeLang set ShortName = 'Line (± SD)' where PlotTypeLangID = 2;
update PlotTypeLang set ShortName = 'Line (± 1.96 SD)' where PlotTypeLangID = 3;
update PlotTypeLang set ShortName = 'BoxPlot (Min/Max)' where PlotTypeLangID = 4;
update PlotTypeLang set ShortName = 'BoxPlot (Tukey)' where PlotTypeLangID = 5;

alter table PlotTypeLang add SupportsMultipleSeries int;
update PlotTypeLang set SupportsMultipleSeries = 1 where PlotTypeLangID in (1, 2, 3); -- not including 4 and 5 because box plots don't support multiple series for now.

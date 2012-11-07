USE [master]
GO
/****** Object:  Database [healthWatch]    Script Date: 11/07/2012 21:38:15 ******/
CREATE DATABASE [healthWatch] ON  PRIMARY 
( NAME = N'healthWatch_Data', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\healthWatch.mdf' , SIZE = 478464KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'healthWatch_Log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\healthWatch_1.ldf' , SIZE = 43264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
GO
ALTER DATABASE [healthWatch] SET COMPATIBILITY_LEVEL = 90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [healthWatch].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [healthWatch] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [healthWatch] SET ANSI_NULLS OFF
GO
ALTER DATABASE [healthWatch] SET ANSI_PADDING OFF
GO
ALTER DATABASE [healthWatch] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [healthWatch] SET ARITHABORT OFF
GO
ALTER DATABASE [healthWatch] SET AUTO_CLOSE ON
GO
ALTER DATABASE [healthWatch] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [healthWatch] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [healthWatch] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [healthWatch] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [healthWatch] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [healthWatch] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [healthWatch] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [healthWatch] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [healthWatch] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [healthWatch] SET  DISABLE_BROKER
GO
ALTER DATABASE [healthWatch] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [healthWatch] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [healthWatch] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [healthWatch] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [healthWatch] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [healthWatch] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [healthWatch] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [healthWatch] SET  READ_WRITE
GO
ALTER DATABASE [healthWatch] SET RECOVERY FULL
GO
ALTER DATABASE [healthWatch] SET  MULTI_USER
GO
ALTER DATABASE [healthWatch] SET PAGE_VERIFY TORN_PAGE_DETECTION
GO
ALTER DATABASE [healthWatch] SET DB_CHAINING OFF
GO
USE [healthWatch]
GO
/****** Object:  User [healthWatch]    Script Date: 11/07/2012 21:38:15 ******/
CREATE USER [healthWatch] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [eForm]    Script Date: 11/07/2012 21:38:15 ******/
CREATE USER [eForm] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[WiseLang]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WiseLang](
	[WiseLangID] [int] IDENTITY(1,1) NOT NULL,
	[WiseID] [int] NULL,
	[LangID] [int] NULL,
	[Wise] [nvarchar](max) NULL,
	[WiseBy] [nvarchar](64) NULL,
 CONSTRAINT [PK_WiseLang] PRIMARY KEY CLUSTERED 
(
	[WiseLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wise]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wise](
	[WiseID] [int] IDENTITY(1,1) NOT NULL,
	[LastShown] [smalldatetime] NULL,
 CONSTRAINT [PK_Wise] PRIMARY KEY CLUSTERED 
(
	[WiseID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserToken]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserToken](
	[UserToken] [uniqueidentifier] NOT NULL,
	[UserID] [int] NOT NULL,
	[Expires] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_UserToken] PRIMARY KEY CLUSTERED 
(
	[UserToken] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSponsorExtendedSurvey]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSponsorExtendedSurvey](
	[UserSponsorExtendedSurveyID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[SponsorExtendedSurveyID] [int] NULL,
	[ProjectRoundUserID] [int] NULL,
	[AnswerID] [int] NULL,
	[FinishedEmail] [smalldatetime] NULL,
	[ContactRequestDT] [smalldatetime] NULL,
	[ContactRequest] [text] NULL,
 CONSTRAINT [PK_UserSponsorExtendedSurvey] PRIMARY KEY CLUSTERED 
(
	[UserSponsorExtendedSurveyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProjectRoundUserAnswer]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserProjectRoundUserAnswer](
	[UserProjectRoundUserAnswerID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectRoundUserID] [int] NULL,
	[AnswerKey] [varchar](32) NULL,
	[DT] [smalldatetime] NULL,
	[UserProfileID] [int] NULL,
	[AnswerID] [int] NULL,
 CONSTRAINT [PK_UserProjectRoundUserAnswer] PRIMARY KEY CLUSTERED 
(
	[UserProjectRoundUserAnswerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_AnswerID_ProjectRoundUserID] ON [dbo].[UserProjectRoundUserAnswer] 
(
	[AnswerID] ASC,
	[ProjectRoundUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_AnswerKey] ON [dbo].[UserProjectRoundUserAnswer] 
(
	[AnswerKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ProjectRoundUserID] ON [dbo].[UserProjectRoundUserAnswer] 
(
	[ProjectRoundUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserProfileID] ON [dbo].[UserProjectRoundUserAnswer] 
(
	[UserProfileID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProjectRoundUser]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProjectRoundUser](
	[UserProjectRoundUserID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[ProjectRoundUnitID] [int] NULL,
	[ProjectRoundUserID] [int] NULL,
 CONSTRAINT [PK_UserProjectRoundUser] PRIMARY KEY CLUSTERED 
(
	[UserProjectRoundUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserID] ON [dbo].[UserProjectRoundUser] 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfileBQ]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfileBQ](
	[UserBQID] [int] IDENTITY(1,1) NOT NULL,
	[UserProfileID] [int] NULL,
	[BQID] [int] NULL,
	[ValueInt] [int] NULL,
	[ValueText] [text] NULL,
	[ValueDate] [smalldatetime] NULL,
 CONSTRAINT [PK_UserBQ] PRIMARY KEY CLUSTERED 
(
	[UserBQID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserID] ON [dbo].[UserProfileBQ] 
(
	[UserProfileID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[UserProfileID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[SponsorID] [int] NULL,
	[DepartmentID] [int] NULL,
	[ProfileComparisonID] [int] NULL,
	[Created] [smalldatetime] NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[UserProfileID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ProfileComparisonID] ON [dbo].[UserProfile] 
(
	[ProfileComparisonID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserID] ON [dbo].[UserProfile] 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMeasureComponent]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMeasureComponent](
	[UserMeasureComponentID] [int] IDENTITY(1,1) NOT NULL,
	[UserMeasureID] [int] NULL,
	[MeasureComponentID] [int] NOT NULL,
	[ValInt] [int] NULL,
	[ValDec] [decimal](18, 10) NULL,
	[ValTxt] [text] NULL,
 CONSTRAINT [PK_UserMeasureComponent] PRIMARY KEY CLUSTERED 
(
	[UserMeasureComponentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMeasure]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMeasure](
	[UserMeasureID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[DT] [smalldatetime] NULL,
	[CreatedDT] [smalldatetime] NULL,
	[DeletedDT] [smalldatetime] NULL,
	[UserProfileID] [int] NULL,
 CONSTRAINT [PK_UserMeasure] PRIMARY KEY CLUSTERED 
(
	[UserMeasureID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](255) NULL,
	[Email] [varchar](255) NULL,
	[Password] [varchar](255) NULL,
	[Created] [smalldatetime] NULL,
	[UserKey] [uniqueidentifier] NULL,
	[SponsorID] [int] NULL,
	[Reminder] [int] NULL,
	[AttitudeSurvey] [smalldatetime] NULL,
	[UserProfileID] [int] NULL,
	[DepartmentID] [int] NULL,
	[ReminderLastSent] [smalldatetime] NULL,
	[EmailFailure] [smalldatetime] NULL,
	[ReminderType] [int] NULL,
	[ReminderLink] [int] NULL,
	[ReminderSettings] [varchar](64) NULL,
	[ReminderNextSend] [smalldatetime] NULL,
	[LID] [int] NULL,
	[AltEmail] [varchar](255) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_SponsorID] ON [dbo].[User] 
(
	[SponsorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemSettingsLang]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SystemSettingsLang](
	[SystemSettingsLangID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [int] NULL,
	[LID] [int] NULL,
	[ReminderMessage] [text] NULL,
	[ReminderSubject] [varchar](1000) NULL,
	[ReminderAutoLogin] [text] NULL,
 CONSTRAINT [PK_SystemSettingsLang] PRIMARY KEY CLUSTERED 
(
	[SystemSettingsLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SystemSettings]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SystemSettings](
	[SystemID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectRoundUnitID] [int] NULL,
	[ReminderMessage] [text] NULL,
	[ReminderSubject] [varchar](1000) NULL,
	[ReminderEmail] [varchar](100) NULL,
 CONSTRAINT [PK_System] PRIMARY KEY CLUSTERED 
(
	[SystemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SuperSponsorLang]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuperSponsorLang](
	[SuperSponsorLangID] [int] IDENTITY(1,1) NOT NULL,
	[SuperSponsorID] [int] NULL,
	[LangID] [int] NULL,
	[Slogan] [text] NULL,
	[Header] [text] NULL,
 CONSTRAINT [PK_SuperSponsorLang] PRIMARY KEY CLUSTERED 
(
	[SuperSponsorLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuperSponsor]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SuperSponsor](
	[SuperSponsorID] [int] IDENTITY(1,1) NOT NULL,
	[SuperSponsor] [varchar](255) NULL,
	[Logo] [int] NULL,
 CONSTRAINT [PK_SuperSponsor] PRIMARY KEY CLUSTERED 
(
	[SuperSponsorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SuperAdminSponsor]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuperAdminSponsor](
	[SuperAdminSponsorID] [int] IDENTITY(1,1) NOT NULL,
	[SuperAdminID] [int] NULL,
	[SponsorID] [int] NULL,
	[SeeUsers] [int] NULL,
 CONSTRAINT [PK_SuperAdminSponsor] PRIMARY KEY CLUSTERED 
(
	[SuperAdminSponsorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuperAdmin]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SuperAdmin](
	[SuperAdminID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](64) NULL,
	[Password] [varchar](64) NULL,
 CONSTRAINT [PK_SuperAdmin] PRIMARY KEY CLUSTERED 
(
	[SuperAdminID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorPRUBQmapVal]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorPRUBQmapVal](
	[SponsorPRUBQmapValID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorPRUBQmapID] [int] NULL,
	[BAID] [int] NULL,
	[OCID] [int] NULL,
 CONSTRAINT [PK_SponsorPRUBQmapVal] PRIMARY KEY CLUSTERED 
(
	[SponsorPRUBQmapValID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_SponsorPRUBQmapID] ON [dbo].[SponsorPRUBQmapVal] 
(
	[SponsorPRUBQmapID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsorPRUBQmap]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorPRUBQmap](
	[SponsorPRUBQmapID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorProjectRoundUnitID] [int] NULL,
	[BQID] [int] NULL,
	[QID] [int] NULL,
	[OID] [int] NULL,
	[FN] [int] NULL,
 CONSTRAINT [PK_SponsorPRUBQmap] PRIMARY KEY CLUSTERED 
(
	[SponsorPRUBQmapID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_SponsorProjectRoundUnitID] ON [dbo].[SponsorPRUBQmap] 
(
	[SponsorProjectRoundUnitID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsorProjectRoundUnitLang]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SponsorProjectRoundUnitLang](
	[SponsorProjectRoundUnitLangID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorProjectRoundUnitID] [int] NULL,
	[LangID] [int] NULL,
	[Nav] [varchar](255) NULL,
	[Feedback] [varchar](255) NULL,
 CONSTRAINT [PK_SponsorProjectRoundUnitLang] PRIMARY KEY CLUSTERED 
(
	[SponsorProjectRoundUnitLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorProjectRoundUnit]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SponsorProjectRoundUnit](
	[SponsorProjectRoundUnitID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorID] [int] NULL,
	[ProjectRoundUnitID] [int] NULL,
	[Nav] [varchar](255) NULL,
	[SurveyKey] [uniqueidentifier] NULL,
	[SortOrder] [int] NULL,
	[Feedback] [varchar](255) NULL,
	[Ext] [int] NULL,
	[SurveyID] [int] NULL,
 CONSTRAINT [PK_SponsorProjectRoundUnit] PRIMARY KEY CLUSTERED 
(
	[SponsorProjectRoundUnitID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_ProjectRoundUnitID] ON [dbo].[SponsorProjectRoundUnit] 
(
	[ProjectRoundUnitID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_SponsorID] ON [dbo].[SponsorProjectRoundUnit] 
(
	[SponsorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsorLogo]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SponsorLogo](
	[SponsorLogoID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorID] [int] NULL,
	[URL] [varchar](255) NULL,
 CONSTRAINT [PK_SponsorLogo] PRIMARY KEY CLUSTERED 
(
	[SponsorLogoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_SponsorID] ON [dbo].[SponsorLogo] 
(
	[SponsorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsorLang]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorLang](
	[SponsorLangID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorID] [int] NULL,
	[LangID] [int] NULL,
	[TreatmentOfferText] [text] NULL,
	[TreatmentOfferIfNeededText] [text] NULL,
	[AlternativeTreatmentOfferText] [text] NULL,
 CONSTRAINT [PK_SponsorLang] PRIMARY KEY CLUSTERED 
(
	[SponsorLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsorInviteBQ]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorInviteBQ](
	[SponsorInviteBQID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorInviteID] [int] NULL,
	[BQID] [int] NULL,
	[BAID] [int] NULL,
	[ValueInt] [int] NULL,
	[ValueDate] [smalldatetime] NULL,
	[ValueText] [text] NULL,
 CONSTRAINT [PK_SponsorInviteBQ] PRIMARY KEY CLUSTERED 
(
	[SponsorInviteBQID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsorInvite]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SponsorInvite](
	[SponsorInviteID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorID] [int] NULL,
	[DepartmentID] [int] NULL,
	[Email] [varchar](256) NULL,
	[UserID] [int] NULL,
	[Sent] [smalldatetime] NULL,
	[InvitationKey] [uniqueidentifier] NULL,
	[Consent] [smalldatetime] NULL,
	[Stopped] [smalldatetime] NULL,
	[StoppedReason] [int] NULL,
	[PreviewExtendedSurveys] [int] NULL,
 CONSTRAINT [PK_SponsorInvite] PRIMARY KEY CLUSTERED 
(
	[SponsorInviteID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorExtendedSurveyDepartment]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorExtendedSurveyDepartment](
	[SponsorExtendedSurveyDepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorExtendedSurveyID] [int] NULL,
	[DepartmentID] [int] NULL,
	[RequiredUserCount] [int] NULL,
	[Hide] [int] NULL,
	[Ext] [int] NULL,
 CONSTRAINT [PK_SponsorExtendedSurveyDepartment] PRIMARY KEY CLUSTERED 
(
	[SponsorExtendedSurveyDepartmentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsorExtendedSurveyBQ]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorExtendedSurveyBQ](
	[SponsorExtendedSurveyBQID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorExtendedSurveyID] [int] NULL,
	[ProjectRoundQOID] [int] NULL,
	[BQID] [int] NULL,
	[FN] [int] NULL,
 CONSTRAINT [PK_SponsorExtendedSurveyBQ] PRIMARY KEY CLUSTERED 
(
	[SponsorExtendedSurveyBQID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsorExtendedSurveyBA]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorExtendedSurveyBA](
	[SponsorExtendedSurveyBAID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorExtendedSurveyID] [int] NULL,
	[ProjectRoundQOID] [int] NULL,
	[OptionComponentID] [int] NULL,
	[BAID] [int] NULL,
 CONSTRAINT [PK_SponsorExtendedSurveyBA] PRIMARY KEY CLUSTERED 
(
	[SponsorExtendedSurveyBAID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsorExtendedSurvey]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SponsorExtendedSurvey](
	[SponsorExtendedSurveyID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorID] [int] NULL,
	[ProjectRoundID] [int] NULL,
	[Internal] [varchar](256) NULL,
	[EformFeedbackID] [int] NULL,
	[RequiredUserCount] [int] NULL,
	[PreviousProjectRoundID] [int] NULL,
	[RoundText] [varchar](256) NULL,
	[EmailSubject] [varchar](256) NULL,
	[EmailBody] [text] NULL,
	[EmailLastSent] [datetime] NULL,
	[IndividualFeedbackID] [int] NULL,
	[IndividualFeedbackEmailSubject] [text] NULL,
	[IndividualFeedbackEmailBody] [text] NULL,
	[WarnIfMissingQID] [int] NULL,
	[ExtraEmailSubject] [varchar](256) NULL,
	[ExtraEmailBody] [text] NULL,
	[FinishedEmailSubject] [varchar](256) NULL,
	[FinishedEmailBody] [text] NULL,
	[FinishedLastSent] [datetime] NULL,
 CONSTRAINT [PK_SponsorExtendedSurvey] PRIMARY KEY CLUSTERED 
(
	[SponsorExtendedSurveyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorBQ]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorBQ](
	[SponsorBQID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorID] [int] NULL,
	[BQID] [int] NULL,
	[Forced] [int] NULL,
	[SortOrder] [int] NULL,
	[Hidden] [int] NULL,
	[Fn] [int] NULL,
	[InGrpAdmin] [int] NULL,
	[IncludeInTreatmentReq] [int] NULL,
	[Organize] [int] NULL,
 CONSTRAINT [PK_SponsorBQ] PRIMARY KEY CLUSTERED 
(
	[SponsorBQID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_BQID] ON [dbo].[SponsorBQ] 
(
	[BQID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_SponsorID] ON [dbo].[SponsorBQ] 
(
	[SponsorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsorAdminFunction]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorAdminFunction](
	[SponsorAdminFunctionID] [int] IDENTITY(1,1) NOT NULL,
	[ManagerFunctionID] [int] NULL,
	[SponsorAdminID] [int] NULL,
 CONSTRAINT [PK_SponsorAdminFunction] PRIMARY KEY CLUSTERED 
(
	[SponsorAdminFunctionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsorAdminDepartment]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorAdminDepartment](
	[SponsorAdminDepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorAdminID] [int] NULL,
	[DepartmentID] [int] NULL,
 CONSTRAINT [PK_SponsorAdminDepartment] PRIMARY KEY CLUSTERED 
(
	[SponsorAdminDepartmentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsorAdmin]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SponsorAdmin](
	[SponsorAdminID] [int] IDENTITY(1,1) NOT NULL,
	[Usr] [varchar](50) NULL,
	[Pas] [varchar](50) NULL,
	[SponsorID] [int] NULL,
	[Name] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[SuperUser] [int] NULL,
	[SponsorAdminKey] [uniqueidentifier] NULL,
	[Anonymized] [int] NULL,
	[SeeUsers] [int] NULL,
	[ReadOnly] [int] NULL,
 CONSTRAINT [PK_SponsorAdmin] PRIMARY KEY CLUSTERED 
(
	[SponsorAdminID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sponsor]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sponsor](
	[SponsorID] [int] IDENTITY(1,1) NOT NULL,
	[Sponsor] [varchar](255) NULL,
	[Application] [varchar](255) NULL,
	[ProjectRoundUnitID] [int] NULL,
	[SponsorKey] [uniqueidentifier] NULL,
	[InviteTxt] [text] NULL,
	[InviteReminderTxt] [text] NULL,
	[LoginTxt] [text] NULL,
	[InviteLastSent] [smalldatetime] NULL,
	[InviteReminderLastSent] [smalldatetime] NULL,
	[LoginLastSent] [smalldatetime] NULL,
	[InviteSubject] [text] NULL,
	[InviteReminderSubject] [text] NULL,
	[LoginSubject] [text] NULL,
	[LoginDays] [int] NULL,
	[LoginWeekday] [int] NULL,
	[LID] [int] NULL,
	[TreatmentOffer] [int] NULL,
	[TreatmentOfferText] [text] NULL,
	[TreatmentOfferEmail] [varchar](50) NULL,
	[TreatmentOfferIfNeededText] [text] NULL,
	[TreatmentOfferBQ] [int] NULL,
	[TreatmentOfferBQfn] [int] NULL,
	[TreatmentOfferBQmorethan] [int] NULL,
	[InfoText] [text] NULL,
	[ConsentText] [text] NULL,
	[Closed] [datetime] NULL,
	[Deleted] [datetime] NULL,
	[SuperSponsorID] [int] NULL,
	[AlternativeTreatmentOfferText] [text] NULL,
	[AlternativeTreatmentOfferEmail] [varchar](50) NULL,
	[SponsorApiKey] [uniqueidentifier] NULL,
	[AllMessageSubject] [text] NULL,
	[AllMessageBody] [text] NULL,
	[AllMessageLastSent] [smalldatetime] NULL,
	[ForceLID] [int] NULL,
 CONSTRAINT [PK_Sponsor] PRIMARY KEY CLUSTERED 
(
	[SponsorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Session]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Session](
	[SessionID] [int] IDENTITY(1,1) NOT NULL,
	[Referrer] [varchar](512) NULL,
	[DT] [datetime] NULL,
	[UserAgent] [varchar](512) NULL,
	[UserID] [int] NULL,
	[IP] [varchar](16) NULL,
	[EndDT] [datetime] NULL,
	[Host] [varchar](64) NULL,
	[Site] [varchar](32) NULL,
	[AutoEnded] [bit] NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[SessionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_UserID] ON [dbo].[Session] 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reminder]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reminder](
	[ReminderID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[DT] [smalldatetime] NULL,
	[Subject] [text] NULL,
	[Body] [text] NULL,
 CONSTRAINT [PK_Reminder] PRIMARY KEY CLUSTERED 
(
	[ReminderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfileComparisonBQ]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfileComparisonBQ](
	[ProfileComparisonBQID] [int] IDENTITY(1,1) NOT NULL,
	[BQID] [int] NULL,
	[ValueInt] [int] NULL,
	[ProfileComparisonID] [int] NULL,
 CONSTRAINT [PK_ProfileComparisonBQ] PRIMARY KEY CLUSTERED 
(
	[ProfileComparisonBQID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfileComparison]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProfileComparison](
	[ProfileComparisonID] [int] IDENTITY(1,1) NOT NULL,
	[Hash] [varchar](48) NULL,
 CONSTRAINT [PK_ProfileComparison] PRIMARY KEY CLUSTERED 
(
	[ProfileComparisonID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MeasureTypeLang]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MeasureTypeLang](
	[MeasureTypeLangID] [int] IDENTITY(1,1) NOT NULL,
	[MeasureTypeID] [int] NULL,
	[LangID] [int] NULL,
	[MeasureType] [varchar](255) NULL,
 CONSTRAINT [PK_MeasureTypeLang] PRIMARY KEY CLUSTERED 
(
	[MeasureTypeLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MeasureType]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MeasureType](
	[MeasureTypeID] [int] IDENTITY(1,1) NOT NULL,
	[MeasureType] [varchar](64) NULL,
	[SortOrder] [int] NULL,
	[Active] [int] NULL,
 CONSTRAINT [PK_MeasureType] PRIMARY KEY CLUSTERED 
(
	[MeasureTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MeasureLang]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MeasureLang](
	[MeasureLangID] [int] IDENTITY(1,1) NOT NULL,
	[MeasureID] [int] NULL,
	[LangID] [int] NULL,
	[Measure] [varchar](255) NULL,
 CONSTRAINT [PK_MeasureLang] PRIMARY KEY CLUSTERED 
(
	[MeasureLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MeasureComponentPart]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeasureComponentPart](
	[MeasureComponentPartID] [int] IDENTITY(1,1) NOT NULL,
	[MeasureComponentID] [int] NULL,
	[MeasureComponentPart] [int] NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_MeasureComponentPart] PRIMARY KEY CLUSTERED 
(
	[MeasureComponentPartID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeasureComponentLang]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MeasureComponentLang](
	[MeasureComponentLangID] [int] IDENTITY(1,1) NOT NULL,
	[MeasureComponentID] [int] NULL,
	[LangID] [int] NULL,
	[MeasureComponent] [varchar](255) NULL,
	[Unit] [varchar](255) NULL,
 CONSTRAINT [PK_MeasureComponentLang] PRIMARY KEY CLUSTERED 
(
	[MeasureComponentLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MeasureComponent]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MeasureComponent](
	[MeasureComponentID] [int] IDENTITY(1,1) NOT NULL,
	[MeasureID] [int] NULL,
	[MeasureComponent] [varchar](64) NULL,
	[Type] [int] NULL,
	[Required] [int] NULL,
	[SortOrder] [int] NULL,
	[Unit] [varchar](16) NULL,
	[Decimals] [int] NULL,
	[ShowInList] [int] NULL,
	[ShowUnitInList] [int] NULL,
	[ShowInGraph] [int] NULL,
	[Inherit] [int] NULL,
	[AutoScript] [varchar](1000) NULL,
 CONSTRAINT [PK_MeasureComponent] PRIMARY KEY CLUSTERED 
(
	[MeasureComponentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MeasureCategoryLang]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MeasureCategoryLang](
	[MeasureCategoryLangID] [int] IDENTITY(1,1) NOT NULL,
	[MeasureCategoryID] [int] NULL,
	[LangID] [int] NULL,
	[MeasureCategory] [varchar](255) NULL,
 CONSTRAINT [PK_MeasureCategoryLang] PRIMARY KEY CLUSTERED 
(
	[MeasureCategoryLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MeasureCategory]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MeasureCategory](
	[MeasureCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[MeasureCategory] [varchar](64) NULL,
	[MeasureTypeID] [int] NULL,
	[SortOrder] [int] NULL,
	[SPRUID] [int] NULL,
 CONSTRAINT [PK_MeasureCategory] PRIMARY KEY CLUSTERED 
(
	[MeasureCategoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Measure]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Measure](
	[MeasureID] [int] IDENTITY(1,1) NOT NULL,
	[Measure] [varchar](64) NULL,
	[MeasureCategoryID] [int] NULL,
	[SortOrder] [int] NULL,
	[MoreInfo] [text] NULL,
 CONSTRAINT [PK_Measure] PRIMARY KEY CLUSTERED 
(
	[MeasureID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ManagerFunction]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ManagerFunction](
	[ManagerFunctionID] [int] IDENTITY(1,1) NOT NULL,
	[ManagerFunction] [varchar](64) NULL,
	[URL] [varchar](128) NULL,
	[Expl] [varchar](256) NULL,
 CONSTRAINT [PK_ManagerFunction] PRIMARY KEY CLUSTERED 
(
	[ManagerFunctionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LID]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LID](
	[LID] [int] IDENTITY(1,1) NOT NULL,
	[Language] [varchar](64) NULL,
 CONSTRAINT [PK_LID] PRIMARY KEY CLUSTERED 
(
	[LID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Lang]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lang](
	[LangID] [int] IDENTITY(1,1) NOT NULL,
	[Lang] [varchar](16) NULL,
 CONSTRAINT [PK_Lang] PRIMARY KEY CLUSTERED 
(
	[LangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExerciseVariantLang]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ExerciseVariantLang](
	[ExerciseVariantLangID] [int] IDENTITY(1,1) NOT NULL,
	[ExerciseVariantID] [int] NULL,
	[ExerciseFile] [varchar](255) NULL,
	[ExerciseFileSize] [int] NULL,
	[ExerciseContent] [text] NULL,
	[ExerciseWindowX] [int] NULL,
	[ExerciseWindowY] [int] NULL,
	[Lang] [int] NULL,
 CONSTRAINT [PK_ExerciseVariantLang] PRIMARY KEY CLUSTERED 
(
	[ExerciseVariantLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExerciseVariant]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExerciseVariant](
	[ExerciseVariantID] [int] IDENTITY(1,1) NOT NULL,
	[ExerciseID] [int] NULL,
	[ExerciseTypeID] [int] NULL,
 CONSTRAINT [PK_ExerciseVariant] PRIMARY KEY CLUSTERED 
(
	[ExerciseVariantID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExerciseTypeLang]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ExerciseTypeLang](
	[ExerciseTypeLangID] [int] IDENTITY(1,1) NOT NULL,
	[ExerciseTypeID] [int] NULL,
	[ExerciseType] [varchar](64) NULL,
	[ExerciseSubtype] [varchar](64) NULL,
	[Lang] [int] NULL,
 CONSTRAINT [PK_ExerciseTypeLang] PRIMARY KEY CLUSTERED 
(
	[ExerciseTypeLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExerciseType]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExerciseType](
	[ExerciseTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ExerciseTypeSortOrder] [int] NULL,
 CONSTRAINT [PK_ExerciseType] PRIMARY KEY CLUSTERED 
(
	[ExerciseTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExerciseStats]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExerciseStats](
	[ExerciseStatsID] [int] IDENTITY(1,1) NOT NULL,
	[ExerciseVariantLangID] [int] NULL,
	[UserID] [int] NULL,
	[DateTime] [smalldatetime] NULL,
	[UserProfileID] [int] NULL,
 CONSTRAINT [PK_ExerciseStats] PRIMARY KEY CLUSTERED 
(
	[ExerciseStatsID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExerciseMiracle]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExerciseMiracle](
	[ExerciseMiracleID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[DateTime] [smalldatetime] NULL,
	[DateTimeChanged] [smalldatetime] NULL,
	[Miracle] [text] NULL,
	[AllowPublish] [bit] NULL,
	[Published] [bit] NULL,
 CONSTRAINT [PK_ExerciseMiracle] PRIMARY KEY CLUSTERED 
(
	[ExerciseMiracleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExerciseLang]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ExerciseLang](
	[ExerciseLangID] [int] IDENTITY(1,1) NOT NULL,
	[ExerciseID] [int] NULL,
	[Exercise] [varchar](255) NULL,
	[ExerciseTime] [varchar](16) NULL,
	[ExerciseTeaser] [text] NULL,
	[Lang] [int] NULL,
	[New] [bit] NULL,
 CONSTRAINT [PK_ExerciseLang] PRIMARY KEY CLUSTERED 
(
	[ExerciseLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExerciseCategoryLang]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ExerciseCategoryLang](
	[ExerciseCategoryLangID] [int] IDENTITY(1,1) NOT NULL,
	[ExerciseCategoryID] [int] NULL,
	[ExerciseCategory] [varchar](255) NULL,
	[Lang] [int] NULL,
 CONSTRAINT [PK_ExerciseCategoryLang] PRIMARY KEY CLUSTERED 
(
	[ExerciseCategoryLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExerciseCategory]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExerciseCategory](
	[ExerciseCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[ExerciseCategorySortOrder] [int] NULL,
 CONSTRAINT [PK_ExerciseCategory] PRIMARY KEY CLUSTERED 
(
	[ExerciseCategoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExerciseAreaLang]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ExerciseAreaLang](
	[ExerciseAreaLangID] [int] IDENTITY(1,1) NOT NULL,
	[ExerciseAreaID] [int] NULL,
	[ExerciseArea] [varchar](255) NULL,
	[Lang] [int] NULL,
 CONSTRAINT [PK_ExerciseAreaLang] PRIMARY KEY CLUSTERED 
(
	[ExerciseAreaLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExerciseArea]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ExerciseArea](
	[ExerciseAreaID] [int] IDENTITY(1,1) NOT NULL,
	[ExerciseAreaSortOrder] [int] NULL,
	[ExerciseAreaImg] [varchar](255) NULL,
 CONSTRAINT [PK_ExerciseArea] PRIMARY KEY CLUSTERED 
(
	[ExerciseAreaID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Exercise]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Exercise](
	[ExerciseID] [int] IDENTITY(1,1) NOT NULL,
	[ExerciseAreaID] [int] NULL,
	[ExerciseSortOrder] [int] NULL,
	[ExerciseImg] [varchar](255) NULL,
	[RequiredUserLevel] [int] NULL,
	[Minutes] [int] NULL,
	[ExerciseCategoryID] [int] NULL,
	[PrintOnBottom] [int] NULL,
	[ReplacementHead] [text] NULL,
 CONSTRAINT [PK_Exercise] PRIMARY KEY CLUSTERED 
(
	[ExerciseID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Diary]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Diary](
	[DiaryID] [int] IDENTITY(1,1) NOT NULL,
	[DiaryNote] [text] NULL,
	[DiaryDate] [smalldatetime] NULL,
	[UserID] [int] NULL,
	[CreatedDT] [smalldatetime] NULL,
	[DeletedDT] [smalldatetime] NULL,
	[Mood] [int] NULL,
 CONSTRAINT [PK_Diary] PRIMARY KEY CLUSTERED 
(
	[DiaryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserID] ON [dbo].[Diary] 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorID] [int] NULL,
	[Department] [varchar](256) NULL,
	[ParentDepartmentID] [int] NULL,
	[SortOrder] [int] NULL,
	[SortString] [varchar](1024) NULL,
	[DepartmentShort] [varchar](64) NULL,
	[DepartmentAnonymized] [varchar](16) NULL,
	[PreviewExtendedSurveys] [int] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CX]    Script Date: 11/07/2012 21:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CX](
	[CXID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_CX] PRIMARY KEY CLUSTERED 
(
	[CXID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[cf_yearMonthDay]    Script Date: 11/07/2012 21:38:18 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[cf_yearMonthDay] (@dt AS SmallDateTime)  
RETURNS VARCHAR(10) AS  
BEGIN 
RETURN '' + CAST(YEAR(@dt) AS VARCHAR(4)) + '-' + RIGHT('0' + CAST(MONTH(@dt) AS VARCHAR(2)),2) + '-' + RIGHT('0' + CAST(DAY(@dt) AS VARCHAR(2)),2)
END
GO
/****** Object:  Table [dbo].[BQvisibility]    Script Date: 11/07/2012 21:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BQvisibility](
	[BQvisibilityID] [int] IDENTITY(1,1) NOT NULL,
	[ChildBQID] [int] NULL,
	[BQID] [int] NULL,
	[BAID] [int] NULL,
 CONSTRAINT [PK_SponsorBQVisibility] PRIMARY KEY CLUSTERED 
(
	[BQvisibilityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BQLang]    Script Date: 11/07/2012 21:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BQLang](
	[BQLangID] [int] IDENTITY(1,1) NOT NULL,
	[BQID] [int] NULL,
	[LangID] [int] NULL,
	[BQ] [varchar](255) NULL,
 CONSTRAINT [PK_BQLang] PRIMARY KEY CLUSTERED 
(
	[BQLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BQ]    Script Date: 11/07/2012 21:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BQ](
	[BQID] [int] IDENTITY(1,1) NOT NULL,
	[Internal] [varchar](255) NULL,
	[Type] [int] NULL,
	[ReqLength] [int] NULL,
	[MaxLength] [int] NULL,
	[DefaultVal] [varchar](64) NULL,
	[Comparison] [int] NULL,
	[MeasurementUnit] [varchar](16) NULL,
	[Layout] [int] NULL,
	[Variable] [varchar](16) NULL,
	[InternalAggregate] [nvarchar](64) NULL,
	[Restricted] [int] NULL,
	[IncludeInDemographics] [int] NULL,
 CONSTRAINT [PK_BQ] PRIMARY KEY CLUSTERED 
(
	[BQID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BALang]    Script Date: 11/07/2012 21:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BALang](
	[BALangID] [int] IDENTITY(1,1) NOT NULL,
	[BAID] [int] NULL,
	[LangID] [int] NULL,
	[BA] [varchar](255) NULL,
 CONSTRAINT [PK_BALang] PRIMARY KEY CLUSTERED 
(
	[BALangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BA]    Script Date: 11/07/2012 21:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BA](
	[BAID] [int] IDENTITY(1,1) NOT NULL,
	[BQID] [int] NULL,
	[Internal] [varchar](255) NULL,
	[SortOrder] [int] NULL,
	[Value] [int] NULL,
 CONSTRAINT [PK_BA] PRIMARY KEY CLUSTERED 
(
	[BAID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_BQID] ON [dbo].[BA] 
(
	[BQID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Affiliate]    Script Date: 11/07/2012 21:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Affiliate](
	[AffiliateID] [int] IDENTITY(1,1) NOT NULL,
	[Affiliate] [varchar](64) NULL,
 CONSTRAINT [PK_Affiliate] PRIMARY KEY CLUSTERED 
(
	[AffiliateID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[cf_hourMinutes]    Script Date: 11/07/2012 21:38:18 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[cf_hourMinutes] (@dt AS SmallDateTime)  
RETURNS INT AS  
BEGIN 
RETURN DATEPART(hh,@dt)*60 + DATEPART(mi,@dt)
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_hourMinute]    Script Date: 11/07/2012 21:38:18 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[cf_hourMinute] (@dt AS SmallDateTime)  
RETURNS VARCHAR(10) AS  
BEGIN 
RETURN '' + RIGHT('0' + CAST(DATEPART(hh,@dt) AS VARCHAR(2)),2) + ':' + RIGHT('0' + CAST(DATEPART(mi,@dt) AS VARCHAR(2)),2)
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_sessionMinutes]    Script Date: 11/07/2012 21:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[cf_sessionMinutes]
(
	-- Add the parameters for the function here
	@from DATETIME, @to DATETIME, @auto BIT
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ret INT

	-- Add the T-SQL statements to compute the return value here
	SELECT @ret = DATEDIFF(minute,@from,DATEADD(minute,-110*@auto,@to)) 

	IF @ret < 0 BEGIN
		SELECT @ret = DATEDIFF(minute,@from,DATEADD(minute,-10*@auto,@to)) 
	END

	-- Return the result of the function
	RETURN ISNULL(@ret,0)

END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_monthsSinceRegistration]    Script Date: 11/07/2012 21:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[cf_monthsSinceRegistration]
(
	-- Add the parameters for the function here
	@UserID INT
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @res int

	SELECT @res = MAX(DATEDIFF(month,u.Created,d.DT)) FROM [User] u INNER JOIN Session d ON d.UserID = u.UserID WHERE u.UserID = @UserID

	-- Return the result of the function
	RETURN @res

END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_DepartmentTree]    Script Date: 11/07/2012 21:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_DepartmentTree] (@DepartmentID INT, @Separator VARCHAR(4))
RETURNS VARCHAR(1024) AS  
BEGIN
	DECLARE @Return VARCHAR(1024), @TEMPID INT
	SET @TEMPID = NULL
	SELECT @Return = LTRIM(RTRIM(Department)), @DepartmentID = ParentDepartmentID FROM [Department] WHERE DepartmentID = @DepartmentID
	WHILE @DepartmentID IS NOT NULL AND (@TEMPID IS NULL OR @TEMPID <> @DepartmentID) BEGIN
		SET @TEMPID = @DepartmentID
		SELECT @Return = LTRIM(RTRIM(Department)) + @Separator + @Return, @DepartmentID = ParentDepartmentID FROM [Department] WHERE DepartmentID = @DepartmentID
	END
	IF @TEMPID IS NOT NULL AND @TEMPID = @DepartmentID BEGIN
		RETURN 'ERROR'
	END
	RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_departmentSortString]    Script Date: 11/07/2012 21:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_departmentSortString] (@DepartmentID INT)
RETURNS VARCHAR(256) AS  
BEGIN
	DECLARE @Return VARCHAR(256)
	SELECT @Return = RIGHT('0000000' + CAST(SortOrder AS VARCHAR(8)),8), @DepartmentID = ParentDepartmentID FROM [Department] WHERE DepartmentID = @DepartmentID
	WHILE @DepartmentID IS NOT NULL BEGIN
		SELECT @Return = RIGHT('0000000' + CAST(SortOrder AS VARCHAR(8)),8) + @Return, @DepartmentID = ParentDepartmentID FROM [Department] WHERE DepartmentID = @DepartmentID
	END
	RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_departmentDepth]    Script Date: 11/07/2012 21:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_departmentDepth] (@DepartmentID INT)
RETURNS INT AS
BEGIN
	DECLARE @UnitDepth INT, @ParentDepartmentID INT
	SET @UnitDepth = 0
	SET @ParentDepartmentID = @DepartmentID
	WHILE @ParentDepartmentID IS NOT NULL BEGIN
		SELECT @ParentDepartmentID=ParentDepartmentID FROM [Department] WHERE DepartmentID = @ParentDepartmentID
		SET @UnitDepth = @UnitDepth + 1
	END
	RETURN @UnitDepth
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_daysFromLastLogin]    Script Date: 11/07/2012 21:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_daysFromLastLogin](@UserID INT)
RETURNS INT
AS
BEGIN
	DECLARE @RET INT

	SELECT TOP 1 @RET = DATEDIFF(dd,s.DT,GETDATE()) 
	FROM [Session] s 
	WHERE s.UserID = @UserID
	ORDER BY s.DT DESC

	RETURN @RET
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_UserCreatedReferrer]    Script Date: 11/07/2012 21:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_UserCreatedReferrer](@UserID INT)
RETURNS varchar(256)
AS
BEGIN
	DECLARE @Res varchar(255)

	SELECT TOP 1 @Res = SUBSTRING(s.Referrer,8,CHARINDEX('/',SUBSTRING(s.Referrer,8,LEN(s.Referrer)))) FROM Session s WHERE s.UserID = @UserID ORDER BY s.SessionID ASC

	RETURN @Res

END
GO
/****** Object:  Default [DF_UserToken_UserToken]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[UserToken] ADD  CONSTRAINT [DF_UserToken_UserToken]  DEFAULT (newid()) FOR [UserToken]
GO
/****** Object:  Default [DF_UserProjectRoundUserAnswer_DT]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[UserProjectRoundUserAnswer] ADD  CONSTRAINT [DF_UserProjectRoundUserAnswer_DT]  DEFAULT (getdate()) FOR [DT]
GO
/****** Object:  Default [DF_UserProfile_Created]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_Created]  DEFAULT (getdate()) FOR [Created]
GO
/****** Object:  Default [DF_User_Created]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Created]  DEFAULT (getdate()) FOR [Created]
GO
/****** Object:  Default [DF_User_UserKey]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_UserKey]  DEFAULT (newid()) FOR [UserKey]
GO
/****** Object:  Default [DF_SponsorProjectRoundUnit_SurveyKey]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[SponsorProjectRoundUnit] ADD  CONSTRAINT [DF_SponsorProjectRoundUnit_SurveyKey]  DEFAULT (newid()) FOR [SurveyKey]
GO
/****** Object:  Default [DF_SponsorInvite_InvitationKey]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[SponsorInvite] ADD  CONSTRAINT [DF_SponsorInvite_InvitationKey]  DEFAULT (newid()) FOR [InvitationKey]
GO
/****** Object:  Default [DF_SponsorAdmin_SponsorAdminKey]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[SponsorAdmin] ADD  CONSTRAINT [DF_SponsorAdmin_SponsorAdminKey]  DEFAULT (newid()) FOR [SponsorAdminKey]
GO
/****** Object:  Default [DF__Sponsor__Sponsor__339FAB6E]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[Sponsor] ADD  DEFAULT (newid()) FOR [SponsorKey]
GO
/****** Object:  Default [DF_Sponsor_SponsorApiKey]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[Sponsor] ADD  CONSTRAINT [DF_Sponsor_SponsorApiKey]  DEFAULT (newid()) FOR [SponsorApiKey]
GO
/****** Object:  Default [DF_Session_DT]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[Session] ADD  CONSTRAINT [DF_Session_DT]  DEFAULT (getdate()) FOR [DT]
GO
/****** Object:  Default [DF_Session_AutoEnded]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[Session] ADD  CONSTRAINT [DF_Session_AutoEnded]  DEFAULT ((0)) FOR [AutoEnded]
GO
/****** Object:  Default [DF_Reminder_DT]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[Reminder] ADD  CONSTRAINT [DF_Reminder_DT]  DEFAULT (getdate()) FOR [DT]
GO
/****** Object:  Default [DF_ExerciseStats_DateTime]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[ExerciseStats] ADD  CONSTRAINT [DF_ExerciseStats_DateTime]  DEFAULT (getdate()) FOR [DateTime]
GO
/****** Object:  Default [DF_ExerciseMiracle_DateTime]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[ExerciseMiracle] ADD  CONSTRAINT [DF_ExerciseMiracle_DateTime]  DEFAULT (getdate()) FOR [DateTime]
GO
/****** Object:  Default [DF_ExerciseMiracle_AllowPublish]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[ExerciseMiracle] ADD  CONSTRAINT [DF_ExerciseMiracle_AllowPublish]  DEFAULT ((0)) FOR [AllowPublish]
GO
/****** Object:  Default [DF_ExerciseMiracle_Published]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[ExerciseMiracle] ADD  CONSTRAINT [DF_ExerciseMiracle_Published]  DEFAULT ((0)) FOR [Published]
GO
/****** Object:  Default [DF_ExerciseLang_New]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[ExerciseLang] ADD  CONSTRAINT [DF_ExerciseLang_New]  DEFAULT ((0)) FOR [New]
GO
/****** Object:  Default [DF_Diary_Created]    Script Date: 11/07/2012 21:38:17 ******/
ALTER TABLE [dbo].[Diary] ADD  CONSTRAINT [DF_Diary_Created]  DEFAULT (getdate()) FOR [CreatedDT]
GO

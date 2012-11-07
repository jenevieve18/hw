USE [master]
GO
/****** Object:  Database [eform]    Script Date: 11/07/2012 21:37:44 ******/
CREATE DATABASE [eform] ON  PRIMARY 
( NAME = N'eForm_dat', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\eform.mdf' , SIZE = 2537280KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%), 
 FILEGROUP [AnswerValue] 
( NAME = N'eForm_idx', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\eform_1.ndf' , SIZE = 473152KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'eForm_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\eform_2.ldf' , SIZE = 126720KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
GO
ALTER DATABASE [eform] SET COMPATIBILITY_LEVEL = 80
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [eform].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [eform] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [eform] SET ANSI_NULLS OFF
GO
ALTER DATABASE [eform] SET ANSI_PADDING OFF
GO
ALTER DATABASE [eform] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [eform] SET ARITHABORT OFF
GO
ALTER DATABASE [eform] SET AUTO_CLOSE ON
GO
ALTER DATABASE [eform] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [eform] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [eform] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [eform] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [eform] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [eform] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [eform] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [eform] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [eform] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [eform] SET  DISABLE_BROKER
GO
ALTER DATABASE [eform] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [eform] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [eform] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [eform] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [eform] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [eform] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [eform] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [eform] SET  READ_WRITE
GO
ALTER DATABASE [eform] SET RECOVERY FULL
GO
ALTER DATABASE [eform] SET  MULTI_USER
GO
ALTER DATABASE [eform] SET PAGE_VERIFY TORN_PAGE_DETECTION
GO
ALTER DATABASE [eform] SET DB_CHAINING OFF
GO
USE [eform]
GO
/****** Object:  User [healthWatch]    Script Date: 11/07/2012 21:37:44 ******/
CREATE USER [healthWatch] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [eForm]    Script Date: 11/07/2012 21:37:44 ******/
CREATE USER [eForm] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  UserDefinedFunction [dbo].[cf_isBlank]    Script Date: 11/07/2012 21:37:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_isBlank] (@z AS INT, @x AS VARCHAR(255), @y AS VARCHAR(255))  
RETURNS VARCHAR(255) AS  
BEGIN 
	DECLARE @s VARCHAR(255)
	SET @s = @x
	IF @s IS NULL OR @s = ''
		SET @s = @y
	IF @s IS NULL OR @s = ''
		SET @s = 'Q' + CAST(@z AS VARCHAR(255))
	RETURN @s
END
GO
/****** Object:  Table [dbo].[AnswerValue]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnswerValue](
	[AnswerValue] [int] IDENTITY(1,1) NOT NULL,
	[AnswerID] [int] NULL,
	[QuestionID] [int] NULL,
	[OptionID] [int] NULL,
	[ValueInt] [int] NULL,
	[ValueDecimal] [decimal](18, 5) NULL,
	[ValueDateTime] [smalldatetime] NULL,
	[CreatedDateTime] [smalldatetime] NULL,
	[CreatedSessionID] [int] NULL,
	[DeletedSessionID] [int] NULL,
	[ValueText] [nvarchar](max) NULL,
	[ValueTextJapaneseUnicode] [nvarchar](max) NULL,
 CONSTRAINT [PK_AnswerValue] PRIMARY KEY CLUSTERED 
(
	[AnswerValue] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_AnswerID] ON [dbo].[AnswerValue] 
(
	[AnswerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_AnswerIDQuestionIDOptionID] ON [dbo].[AnswerValue] 
(
	[AnswerID] ASC,
	[QuestionID] ASC,
	[OptionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_AnswerIDQuestionIDOptionIDDeletedSessionID] ON [dbo].[AnswerValue] 
(
	[AnswerID] DESC,
	[QuestionID] ASC,
	[OptionID] ASC,
	[DeletedSessionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [AnswerValue]
GO
CREATE NONCLUSTERED INDEX [IX_OptionIDValueInt] ON [dbo].[AnswerValue] 
(
	[OptionID] ASC,
	[ValueInt] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [AnswerValue]
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answer](
	[AnswerID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectRoundID] [int] NULL,
	[ProjectRoundUnitID] [int] NULL,
	[ProjectRoundUserID] [int] NULL,
	[StartDT] [datetime] NULL,
	[EndDT] [datetime] NULL,
	[AnswerKey] [uniqueidentifier] NULL,
	[ExtendedFirst] [int] NULL,
	[CurrentPage] [int] NULL,
	[FeedbackAlert] [int] NULL,
 CONSTRAINT [PK_Answer] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_AnswerKey] ON [dbo].[Answer] 
(
	[AnswerKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_PRID_END] ON [dbo].[Answer] 
(
	[ProjectRoundID] ASC,
	[EndDT] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ProjectRoundUnitID] ON [dbo].[Answer] 
(
	[ProjectRoundUnitID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ProjectRoundUserID] ON [dbo].[Answer] 
(
	[ProjectRoundUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[cf_yearMonth]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION  [dbo].[cf_yearMonth] (@dt AS SmallDateTime)  
RETURNS INT AS  
BEGIN 
RETURN YEAR(@dt)*12+DATEPART(month,@dt)
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_year6Month]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION  [dbo].[cf_year6Month] (@dt AS SmallDateTime)  
RETURNS INT AS  
BEGIN 
RETURN CEILING((CAST((YEAR(@dt)*12+DATEPART(month,@dt)) AS FLOAT))/6)
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_year3Month]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION  [dbo].[cf_year3Month] (@dt AS SmallDateTime)  
RETURNS INT AS  
BEGIN 
RETURN CEILING((CAST((YEAR(@dt)*12+DATEPART(month,@dt)) AS FLOAT))/3)
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_twoDigit]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[cf_twoDigit] (@x AS TINYINT)  
RETURNS VARCHAR(2) AS  
BEGIN 
	DECLARE @s VARCHAR(2)
	SET @s = CAST(@x AS VARCHAR(2))
	IF LEN(@s) = 1
		SET @s = '0' + @s
	RETURN @s
END
GO
/****** Object:  Table [dbo].[WeightedQuestionOptionLang]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WeightedQuestionOptionLang](
	[WeightedQuestionOptionLangID] [int] IDENTITY(1,1) NOT NULL,
	[WeightedQuestionOptionID] [int] NULL,
	[LangID] [int] NULL,
	[WeightedQuestionOption] [varchar](255) NULL,
	[FeedbackHeader] [varchar](255) NULL,
	[Feedback] [text] NULL,
	[FeedbackRedLow] [text] NULL,
	[FeedbackYellowLow] [text] NULL,
	[FeedbackGreen] [text] NULL,
	[FeedbackYellowHigh] [text] NULL,
	[FeedbackRedHigh] [text] NULL,
	[ActionRedLow] [text] NULL,
	[ActionYellowLow] [text] NULL,
	[ActionGreen] [text] NULL,
	[ActionYellowHigh] [text] NULL,
	[ActionRedHigh] [text] NULL,
	[WeightedQuestionOptionJapaneseUnicode] [nvarchar](max) NULL,
	[FeedbackHeaderJapaneseUnicode] [nvarchar](max) NULL,
	[FeedbackJapaneseUnicode] [nvarchar](max) NULL,
	[FeedbackRedLowJapaneseUnicode] [nvarchar](max) NULL,
	[FeedbackYellowLowJapaneseUnicode] [nvarchar](max) NULL,
	[FeedbackGreenJapaneseUnicode] [nvarchar](max) NULL,
	[FeedbackYellowHighJapaneseUnicode] [nvarchar](max) NULL,
	[FeedbackRedHighJapaneseUnicode] [nvarchar](max) NULL,
	[ActionRedLowJapaneseUnicode] [nvarchar](max) NULL,
	[ActionYellowLowJapaneseUnicode] [nvarchar](max) NULL,
	[ActionGreenJapaneseUnicode] [nvarchar](max) NULL,
	[ActionYellowHighJapaneseUnicode] [nvarchar](max) NULL,
	[ActionRedHighJapaneseUnicode] [nvarchar](max) NULL,
 CONSTRAINT [PK_WeightedQuestionOptionLang] PRIMARY KEY CLUSTERED 
(
	[WeightedQuestionOptionLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WeightedQuestionOption]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WeightedQuestionOption](
	[WeightedQuestionOptionID] [int] IDENTITY(1,1) NOT NULL,
	[Internal] [varchar](255) NULL,
	[QuestionID] [int] NULL,
	[OptionID] [int] NULL,
	[TargetVal] [int] NULL,
	[YellowLow] [int] NULL,
	[GreenLow] [int] NULL,
	[GreenHigh] [int] NULL,
	[YellowHigh] [int] NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_WeightedQuestionOptionID] PRIMARY KEY CLUSTERED 
(
	[WeightedQuestionOptionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserSchedule]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserSchedule](
	[UserScheduleID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[UserProjectRoundUserID] [int] NULL,
	[DT] [smalldatetime] NULL,
	[SponsorReminderID] [int] NULL,
	[Reminder] [int] NULL,
	[Note] [text] NULL,
	[Email] [varchar](255) NULL,
	[SentDT] [smalldatetime] NULL,
	[NoteJapaneseUnicode] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserSchedule] PRIMARY KEY CLUSTERED 
(
	[UserScheduleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserProjectRoundUser]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserProjectRoundUser](
	[UserProjectRoundUserID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[ProjectRoundUserID] [int] NULL,
	[Note] [varchar](255) NULL,
 CONSTRAINT [PK_UserProjectRoundUser] PRIMARY KEY CLUSTERED 
(
	[UserProjectRoundUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserNote]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserNote](
	[UserNoteID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[DT] [smalldatetime] NULL,
	[SponsorAdminID] [int] NULL,
	[Note] [text] NULL,
	[EditSponsorAdminID] [int] NULL,
	[EditDT] [smalldatetime] NULL,
	[NoteJapaneseUnicode] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserNote] PRIMARY KEY CLUSTERED 
(
	[UserNoteID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCategoryLang]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserCategoryLang](
	[UserCategoryLangID] [int] IDENTITY(1,1) NOT NULL,
	[UserCategoryID] [int] NULL,
	[LangID] [int] NULL,
	[Category] [varchar](255) NULL,
	[CategoryJapaneseUnicode] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserCategoryLang] PRIMARY KEY CLUSTERED 
(
	[UserCategoryLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserCategory]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserCategory](
	[UserCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Internal] [varchar](255) NULL,
 CONSTRAINT [PK_UserCategory] PRIMARY KEY CLUSTERED 
(
	[UserCategoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorID] [int] NULL,
	[UserNr] [int] NULL,
	[UserIdent1] [varchar](255) NULL,
	[UserIdent2] [varchar](255) NULL,
	[UserIdent3] [varchar](255) NULL,
	[UserCheck1] [int] NULL,
	[UserCheck2] [int] NULL,
	[UserCheck3] [int] NULL,
	[UserIdent4] [varchar](255) NULL,
	[UserIdent5] [varchar](255) NULL,
	[UserIdent6] [varchar](255) NULL,
	[UserIdent7] [varchar](255) NULL,
	[UserIdent8] [varchar](255) NULL,
	[UserIdent9] [varchar](255) NULL,
	[UserIdent10] [varchar](255) NULL,
	[FeedbackSent] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UnitCategoryLang]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UnitCategoryLang](
	[UnitCategoryLangID] [int] IDENTITY(1,1) NOT NULL,
	[UnitCategoryID] [int] NULL,
	[LangID] [int] NULL,
	[Category] [varchar](255) NULL,
	[CategoryJapaneseUnicode] [nvarchar](max) NULL,
 CONSTRAINT [PK_UnitCategoryLang] PRIMARY KEY CLUSTERED 
(
	[UnitCategoryLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UnitCategory]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UnitCategory](
	[UnitCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Internal] [varchar](255) NULL,
 CONSTRAINT [PK_UnitCategory] PRIMARY KEY CLUSTERED 
(
	[UnitCategoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Unit]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Unit](
	[UnitID] [int] IDENTITY(1,1) NOT NULL,
	[ID] [varchar](16) NULL,
 CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED 
(
	[UnitID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TempReportComponentAnswer]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempReportComponentAnswer](
	[TempReportComponentAnswerID] [int] IDENTITY(1,1) NOT NULL,
	[TempReportComponentID] [int] NULL,
	[AnswerID] [int] NULL,
 CONSTRAINT [PK_TempReportComponentAnswer] PRIMARY KEY CLUSTERED 
(
	[TempReportComponentAnswerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TempReportComponent]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TempReportComponent](
	[TempReportComponentID] [int] IDENTITY(1,1) NOT NULL,
	[TempReportID] [int] NULL,
	[TempReportComponent] [varchar](1024) NULL,
 CONSTRAINT [PK_TempReportComponent] PRIMARY KEY CLUSTERED 
(
	[TempReportComponentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TempReport]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TempReport](
	[TempReportID] [int] IDENTITY(1,1) NOT NULL,
	[TempReport] [varchar](255) NULL,
 CONSTRAINT [PK_TempReport] PRIMARY KEY CLUSTERED 
(
	[TempReportID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SurveyQuestionOptionComponentLang]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyQuestionOptionComponentLang](
	[SurveyQuestionOptionComponentLangID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyQuestionOptionID] [int] NULL,
	[OptionComponentID] [int] NULL,
	[LangID] [int] NULL,
	[Text] [text] NULL,
	[OnClick] [text] NULL,
	[TextJapaneseUnicode] [nvarchar](max) NULL,
	[OnClickJapaneseUnicode] [nvarchar](max) NULL,
 CONSTRAINT [PK_SurveyQuestionOptionComponentLang] PRIMARY KEY CLUSTERED 
(
	[SurveyQuestionOptionComponentLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SurveyQuestionOptionComponent]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyQuestionOptionComponent](
	[SurveyQuestionOptionComponentID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyQuestionOptionID] [int] NULL,
	[OptionComponentID] [int] NULL,
	[Hide] [int] NULL,
	[OnClick] [text] NULL,
 CONSTRAINT [PK_SurveyQuestionOptionComponent] PRIMARY KEY CLUSTERED 
(
	[SurveyQuestionOptionComponentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SurveyQuestionOption]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SurveyQuestionOption](
	[SurveyQuestionOptionID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyQuestionID] [int] NULL,
	[QuestionOptionID] [int] NULL,
	[OptionPlacement] [int] NULL,
	[Variablename] [varchar](256) NULL,
	[Forced] [int] NULL,
	[SortOrder] [int] NULL,
	[Warn] [int] NULL,
	[Height] [int] NULL,
 CONSTRAINT [PK_SurveyQuestionOption] PRIMARY KEY CLUSTERED 
(
	[SurveyQuestionOptionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_QuestionOptionID] ON [dbo].[SurveyQuestionOption] 
(
	[QuestionOptionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_SurveyQuestionID] ON [dbo].[SurveyQuestionOption] 
(
	[SurveyQuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SurveyQuestionLang]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyQuestionLang](
	[SurveyQuestionLangID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyQuestionID] [int] NULL,
	[LangID] [int] NULL,
	[Question] [text] NULL,
	[QuestionJapaneseUnicode] [nvarchar](max) NULL,
 CONSTRAINT [PK_SurveyQuestionLang] PRIMARY KEY CLUSTERED 
(
	[SurveyQuestionLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_LangID] ON [dbo].[SurveyQuestionLang] 
(
	[LangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_SurveyQuestionID] ON [dbo].[SurveyQuestionLang] 
(
	[SurveyQuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_SurveyQuestionIDLangID] ON [dbo].[SurveyQuestionLang] 
(
	[SurveyQuestionID] ASC,
	[LangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SurveyQuestionIf]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyQuestionIf](
	[SurveyQuestionIfID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyID] [int] NULL,
	[SurveyQuestionID] [int] NULL,
	[QuestionID] [int] NULL,
	[OptionID] [int] NULL,
	[OptionComponentID] [int] NULL,
	[ConditionAnd] [int] NULL,
 CONSTRAINT [PK_SurveyQuestionIf] PRIMARY KEY CLUSTERED 
(
	[SurveyQuestionIfID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_QuestionIDOptionIDOptionComponentID] ON [dbo].[SurveyQuestionIf] 
(
	[QuestionID] ASC,
	[OptionID] ASC,
	[OptionComponentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_SurveyQuestionID] ON [dbo].[SurveyQuestionIf] 
(
	[SurveyQuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SurveyQuestion]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SurveyQuestion](
	[SurveyQuestionID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyID] [int] NULL,
	[QuestionID] [int] NULL,
	[OptionsPlacement] [int] NULL,
	[Variablename] [varchar](256) NULL,
	[SortOrder] [int] NULL,
	[NoCount] [int] NULL,
	[RestartCount] [int] NULL,
	[ExtendedFirst] [int] NULL,
	[NoBreak] [int] NULL,
	[BreakAfterQuestion] [int] NULL,
	[PageBreakBeforeQuestion] [int] NULL,
	[FontSize] [int] NULL,
 CONSTRAINT [PK_SurveyQuestion] PRIMARY KEY CLUSTERED 
(
	[SurveyQuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_QuestionID] ON [dbo].[SurveyQuestion] 
(
	[QuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_SurveyID] ON [dbo].[SurveyQuestion] 
(
	[SurveyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SurveyLang]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyLang](
	[SurveyLangID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyID] [int] NULL,
	[LangID] [int] NULL,
 CONSTRAINT [PK_SurveyLang] PRIMARY KEY CLUSTERED 
(
	[SurveyLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Survey]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Survey](
	[SurveyID] [int] IDENTITY(1,1) NOT NULL,
	[Internal] [varchar](256) NULL,
	[SurveyKey] [uniqueidentifier] NULL,
	[Copyright] [text] NULL,
	[FlipFlopBg] [int] NULL,
	[NoTime] [int] NULL,
	[ClearQuestions] [int] NULL,
	[TwoColumns] [int] NULL,
 CONSTRAINT [PK_Survey] PRIMARY KEY CLUSTERED 
(
	[SurveyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorUserCheck]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SponsorUserCheck](
	[SponsorUserCheckID] [int] IDENTITY(1,1) NOT NULL,
	[UserCheckNr] [int] NULL,
	[SponsorID] [int] NULL,
	[Txt] [varchar](255) NULL,
 CONSTRAINT [PK_SponsorUserCheck] PRIMARY KEY CLUSTERED 
(
	[SponsorUserCheckID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorSuperAdminSponsor]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorSuperAdminSponsor](
	[SponsorSuperAdminSponsorID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorSuperAdminID] [int] NULL,
	[SponsorID] [int] NULL,
 CONSTRAINT [PK_SponsorSuperAdminSponsor] PRIMARY KEY CLUSTERED 
(
	[SponsorSuperAdminSponsorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsorSuperAdmin]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorSuperAdmin](
	[SponsorSuperAdminID] [int] IDENTITY(1,1) NOT NULL,
	[DefaultSponsorID] [int] NULL,
	[Username] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
 CONSTRAINT [PK_SponsorSuperAdmin] PRIMARY KEY CLUSTERED 
(
	[SponsorSuperAdminID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsorReminder]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SponsorReminder](
	[SponsorReminderID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorID] [int] NULL,
	[Reminder] [varchar](255) NULL,
	[FromEmail] [varchar](255) NULL,
	[Subject] [varchar](255) NULL,
	[Body] [text] NULL,
 CONSTRAINT [PK_SponsorReminder] PRIMARY KEY CLUSTERED 
(
	[SponsorReminderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorPRU]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorPRU](
	[SponsorPRUID] [int] IDENTITY(1,1) NOT NULL,
	[PRUID] [int] NULL,
	[NoLogout] [int] NULL,
	[SponsorID] [int] NULL,
	[NoSend] [int] NULL,
 CONSTRAINT [PK_SponsorPRU] PRIMARY KEY CLUSTERED 
(
	[SponsorPRUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsorLang]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorLang](
	[SponsorLangID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorID] [int] NULL,
	[LangID] [int] NULL,
 CONSTRAINT [PK_SponsorLang] PRIMARY KEY CLUSTERED 
(
	[SponsorLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SponsorAutoPRU]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SponsorAutoPRU](
	[SponsorAutoPRUID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorID] [int] NULL,
	[PRUID] [int] NULL,
	[Note] [varchar](255) NULL,
 CONSTRAINT [PK_SponsorAutoPRU] PRIMARY KEY CLUSTERED 
(
	[SponsorAutoPRUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorAdmin]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SponsorAdmin](
	[SponsorAdminID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorID] [int] NULL,
	[Username] [varchar](255) NULL,
	[Password] [varchar](255) NULL,
	[Name] [varchar](255) NULL,
	[Email] [varchar](255) NULL,
	[Restricted] [int] NULL,
 CONSTRAINT [PK_SponsorAdmin] PRIMARY KEY CLUSTERED 
(
	[SponsorAdminID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sponsor]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sponsor](
	[SponsorID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NULL,
	[Sponsor] [varchar](255) NULL,
	[UserIdent1] [varchar](255) NULL,
	[UserIdent2] [varchar](255) NULL,
	[UserIdent3] [varchar](255) NULL,
	[UserCheck1] [varchar](255) NULL,
	[UserCheck2] [varchar](255) NULL,
	[UserCheck3] [varchar](255) NULL,
	[UserIdent4] [varchar](255) NULL,
	[UserIdent5] [varchar](255) NULL,
	[UserIdent6] [varchar](255) NULL,
	[UserIdent7] [varchar](255) NULL,
	[UserIdent8] [varchar](255) NULL,
	[UserIdent9] [varchar](255) NULL,
	[UserIdent10] [varchar](255) NULL,
	[ShowUserIdent1] [int] NULL,
	[ShowUserIdent2] [int] NULL,
	[ShowUserIdent3] [int] NULL,
	[ShowUserIdent4] [int] NULL,
	[ShowUserIdent5] [int] NULL,
	[ShowUserIdent6] [int] NULL,
	[ShowUserIdent7] [int] NULL,
	[ShowUserIdent8] [int] NULL,
	[ShowUserIdent9] [int] NULL,
	[ShowUserIdent10] [int] NULL,
	[ShowUserNr] [int] NULL,
	[UserNr] [varchar](255) NULL,
	[EmailIdent] [int] NULL,
	[FirstnameIdent] [int] NULL,
	[LastnameIdent] [int] NULL,
	[ShowUserCheck1] [int] NULL,
	[ShowUserCheck2] [int] NULL,
	[ShowUserCheck3] [int] NULL,
	[NoEmail] [int] NULL,
	[NoLogout] [int] NULL,
	[CustomReportTemplateID] [int] NULL,
	[FeedbackEmailFrom] [nvarchar](255) NULL,
	[FeedbackEmailSubject] [nvarchar](255) NULL,
	[FeedbackEmailBody] [text] NULL,
 CONSTRAINT [PK_Sponsor] PRIMARY KEY CLUSTERED 
(
	[SponsorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReportPartLang]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReportPartLang](
	[ReportPartLangID] [int] IDENTITY(1,1) NOT NULL,
	[ReportPartID] [int] NULL,
	[LangID] [int] NULL,
	[Subject] [varchar](255) NULL,
	[Header] [text] NULL,
	[Footer] [text] NULL,
	[AltText] [text] NULL,
	[SubjectJapaneseUnicode] [nvarchar](max) NULL,
	[HeaderJapaneseUnicode] [nvarchar](max) NULL,
	[FooterJapaneseUnicode] [nvarchar](max) NULL,
	[AltTextJapaneseUnicode] [nvarchar](max) NULL,
 CONSTRAINT [PK_ReportPartLang] PRIMARY KEY CLUSTERED 
(
	[ReportPartLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReportPartComponent]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportPartComponent](
	[ReportPartComponentID] [int] IDENTITY(1,1) NOT NULL,
	[ReportPartID] [int] NULL,
	[IdxID] [int] NULL,
	[WeightedQuestionOptionID] [int] NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_ReportPartComponent] PRIMARY KEY CLUSTERED 
(
	[ReportPartComponentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReportPart]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReportPart](
	[ReportPartID] [int] IDENTITY(1,1) NOT NULL,
	[ReportID] [int] NULL,
	[Internal] [varchar](255) NULL,
	[Type] [int] NULL,
	[QuestionID] [int] NULL,
	[OptionID] [int] NULL,
	[RequiredAnswerCount] [int] NULL,
	[SortOrder] [int] NULL,
	[PartLevel] [int] NULL,
	[GroupingQuestionID] [int] NULL,
	[GroupingOptionID] [int] NULL,
 CONSTRAINT [PK_ReportPage] PRIMARY KEY CLUSTERED 
(
	[ReportPartID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReportLang]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportLang](
	[ReportLangID] [int] IDENTITY(1,1) NOT NULL,
	[ReportID] [int] NULL,
	[LangID] [int] NULL,
	[Feedback] [text] NULL,
	[FeedbackJapaneseUnicode] [nvarchar](max) NULL,
 CONSTRAINT [PK_ReportLang] PRIMARY KEY CLUSTERED 
(
	[ReportLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Report]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Report](
	[ReportID] [int] IDENTITY(1,1) NOT NULL,
	[Internal] [varchar](255) NULL,
	[ReportKey] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Report] PRIMARY KEY CLUSTERED 
(
	[ReportID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuestionOptionRange]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionOptionRange](
	[QuestionOptionRangeID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionOptionID] [int] NULL,
	[StartDT] [datetime] NULL,
	[EndDT] [datetime] NULL,
	[LowVal] [decimal](18, 5) NULL,
	[HighVal] [decimal](18, 5) NULL,
 CONSTRAINT [PK_QuestionOptionRange] PRIMARY KEY CLUSTERED 
(
	[QuestionOptionRangeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionOption]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuestionOption](
	[QuestionOptionID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionID] [int] NULL,
	[OptionID] [int] NULL,
	[OptionPlacement] [int] NULL,
	[SortOrder] [int] NULL,
	[Variablename] [varchar](256) NULL,
	[Forced] [int] NULL,
	[Hide] [int] NULL,
 CONSTRAINT [PK_QuestionOption] PRIMARY KEY CLUSTERED 
(
	[QuestionOptionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_OptionID] ON [dbo].[QuestionOption] 
(
	[OptionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_QuestionID] ON [dbo].[QuestionOption] 
(
	[QuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionLang]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuestionLang](
	[QuestionLangID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionID] [int] NULL,
	[LangID] [int] NULL,
	[Question] [text] NULL,
	[QuestionShort] [varchar](256) NULL,
	[QuestionArea] [varchar](256) NULL,
	[QuestionJapaneseUnicode] [nvarchar](max) NULL,
	[QuestionShortJapaneseUnicode] [nvarchar](max) NULL,
	[QuestionAreaJapaneseUnicode] [nvarchar](max) NULL,
 CONSTRAINT [PK_QuestionLang] PRIMARY KEY CLUSTERED 
(
	[QuestionLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_LangID] ON [dbo].[QuestionLang] 
(
	[LangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_QuestionID] ON [dbo].[QuestionLang] 
(
	[QuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_QuestionIDLangID] ON [dbo].[QuestionLang] 
(
	[QuestionID] ASC,
	[LangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionContainer]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuestionContainer](
	[QuestionContainerID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionContainer] [varchar](255) NULL,
 CONSTRAINT [PK_QuestionContainer] PRIMARY KEY CLUSTERED 
(
	[QuestionContainerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuestionCategoryQuestion]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionCategoryQuestion](
	[QuestionCategoryQuestionID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionCategoryID] [int] NULL,
	[QuestionID] [int] NULL,
 CONSTRAINT [PK_QuestionCategoryQuestion] PRIMARY KEY CLUSTERED 
(
	[QuestionCategoryQuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionCategoryLang]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuestionCategoryLang](
	[QuestionCategoryLangID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionCategoryID] [int] NULL,
	[LangID] [int] NULL,
	[QuestionCategory] [varchar](255) NULL,
	[QuestionCategoryJapaneseUnicode] [nvarchar](max) NULL,
 CONSTRAINT [PK_QuestionCategoryLang] PRIMARY KEY CLUSTERED 
(
	[QuestionCategoryLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuestionCategory]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuestionCategory](
	[QuestionCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Internal] [varchar](255) NULL,
 CONSTRAINT [PK_QuestionCategory] PRIMARY KEY CLUSTERED 
(
	[QuestionCategoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Question]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Question](
	[QuestionID] [int] IDENTITY(1,1) NOT NULL,
	[VariableName] [varchar](256) NULL,
	[OptionsPlacement] [int] NULL,
	[FontFamily] [int] NULL,
	[FontSize] [int] NULL,
	[FontDecoration] [int] NULL,
	[FontColor] [varchar](16) NULL,
	[Underlined] [int] NULL,
	[QuestionContainerID] [int] NULL,
	[Internal] [varchar](256) NULL,
	[Box] [int] NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_QuestionContainerID] ON [dbo].[Question] 
(
	[QuestionContainerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectUserCategory]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectUserCategory](
	[ProjectUserCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NULL,
	[UserCategoryID] [int] NULL,
 CONSTRAINT [PK_ProjectUserCategory] PRIMARY KEY CLUSTERED 
(
	[ProjectUserCategoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectUnitCategory]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectUnitCategory](
	[ProjectUnitCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NULL,
	[UnitCategoryID] [int] NULL,
 CONSTRAINT [PK_ProjectUnitCategory] PRIMARY KEY CLUSTERED 
(
	[ProjectUnitCategoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectSurvey]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectSurvey](
	[ProjectSurveyID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NULL,
	[SurveyID] [int] NULL,
 CONSTRAINT [PK_ProjectSurvey] PRIMARY KEY CLUSTERED 
(
	[ProjectSurveyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ProjectIDSurveyID] ON [dbo].[ProjectSurvey] 
(
	[ProjectID] ASC,
	[SurveyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectRoundUserQO]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectRoundUserQO](
	[ProjectRoundUserQOID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectRoundUserID] [int] NULL,
	[QuestionID] [int] NULL,
	[OptionID] [int] NULL,
	[Answer] [varchar](256) NULL,
 CONSTRAINT [PK_ProjectRoundUserQO] PRIMARY KEY CLUSTERED 
(
	[ProjectRoundUserQOID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_ProjectRoundUserID] ON [dbo].[ProjectRoundUserQO] 
(
	[ProjectRoundUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectRoundUserCompare]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectRoundUserCompare](
	[ProjectRoundUserCompareID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectRoundUserID] [int] NULL,
	[CompareProjectRoundUserID] [int] NULL,
 CONSTRAINT [PK_ProjectRoundUserCompare] PRIMARY KEY CLUSTERED 
(
	[ProjectRoundUserCompareID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectRoundUser]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectRoundUser](
	[ProjectRoundUserID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectRoundID] [int] NULL,
	[ProjectRoundUnitID] [int] NULL,
	[UserKey] [uniqueidentifier] NULL,
	[Email] [varchar](256) NULL,
	[LastSent] [smalldatetime] NULL,
	[SendCount] [int] NULL,
	[ReminderCount] [int] NULL,
	[UserCategoryID] [int] NULL,
	[Name] [varchar](256) NULL,
	[Created] [smalldatetime] NULL,
	[Extended] [int] NULL,
	[Extra] [varchar](256) NULL,
	[ExternalID] [bigint] NULL,
	[NoSend] [int] NULL,
	[Terminated] [int] NULL,
	[FollowupSendCount] [int] NULL,
	[GroupID] [int] NULL,
	[ExtendedTag] [int] NULL,
 CONSTRAINT [PK_ProjectRoundUser] PRIMARY KEY CLUSTERED 
(
	[ProjectRoundUserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_ProjectRoundID] ON [dbo].[ProjectRoundUser] 
(
	[ProjectRoundID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ProjectRoundUnitID] ON [dbo].[ProjectRoundUser] 
(
	[ProjectRoundUnitID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectRoundUnitManager]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectRoundUnitManager](
	[ProjectRoundUnitManagerID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectRoundUnitID] [int] NULL,
	[ProjectRoundUserID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectRoundUnit]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectRoundUnit](
	[ProjectRoundUnitID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectRoundID] [int] NULL,
	[Unit] [varchar](256) NULL,
	[ID] [varchar](16) NULL,
	[ParentProjectRoundUnitID] [int] NULL,
	[SortOrder] [int] NULL,
	[SortString] [varchar](256) NULL,
	[SurveyID] [int] NULL,
	[LangID] [int] NULL,
	[UnitKey] [uniqueidentifier] NULL,
	[UserCount] [int] NULL,
	[UnitCategoryID] [int] NULL,
	[CanHaveUsers] [bit] NULL,
	[ReportID] [int] NULL,
	[Timeframe] [int] NULL,
	[Yellow] [int] NULL,
	[Green] [int] NULL,
	[SurveyIntro] [text] NULL,
	[Terminated] [bit] NULL,
	[IndividualReportID] [int] NULL,
	[UniqueID] [varchar](16) NULL,
	[RequiredAnswerCount] [int] NULL,
 CONSTRAINT [PK_ProjectUnit] PRIMARY KEY CLUSTERED 
(
	[ProjectRoundUnitID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_ParentProjectRoundUnitUD] ON [dbo].[ProjectRoundUnit] 
(
	[ParentProjectRoundUnitID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ProjectRoundID] ON [dbo].[ProjectRoundUnit] 
(
	[ProjectRoundID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_SortOrder] ON [dbo].[ProjectRoundUnit] 
(
	[SortOrder] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_SortString] ON [dbo].[ProjectRoundUnit] 
(
	[SortString] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectRoundQO]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectRoundQO](
	[ProjectRoundQOID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectRoundID] [int] NULL,
	[QuestionID] [int] NULL,
	[OptionID] [int] NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_ProjectRoundQO] PRIMARY KEY CLUSTERED 
(
	[ProjectRoundQOID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ProjectRoundID] ON [dbo].[ProjectRoundQO] 
(
	[ProjectRoundID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectRoundLang]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectRoundLang](
	[ProjectRoundLangID] [int] IDENTITY(1,1) NOT NULL,
	[LangID] [int] NULL,
	[ProjectRoundID] [int] NULL,
	[InvitationSubject] [varchar](255) NULL,
	[InvitationBody] [text] NULL,
	[ReminderSubject] [varchar](255) NULL,
	[ReminderBody] [text] NULL,
	[SurveyName] [varchar](255) NULL,
	[SurveyIntro] [text] NULL,
	[UnitText] [text] NULL,
	[ThankyouText] [text] NULL,
	[ExtraInvitationSubject] [varchar](255) NULL,
	[ExtraInvitationBody] [text] NULL,
	[ExtraReminderSubject] [varchar](255) NULL,
	[ExtraReminderBody] [text] NULL,
	[InvitationSubjectJapaneseUnicode] [nvarchar](max) NULL,
	[InvitationBodyJapaneseUnicode] [nvarchar](max) NULL,
	[ReminderSubjectJapaneseUnicode] [nvarchar](max) NULL,
	[ReminderBodyJapaneseUnicode] [nvarchar](max) NULL,
	[SurveyNameJapaneseUnicode] [nvarchar](max) NULL,
	[SurveyIntroJapaneseUnicode] [nvarchar](max) NULL,
	[UnitTextJapaneseUnicode] [nvarchar](max) NULL,
	[ThankyouTextJapaneseUnicode] [nvarchar](max) NULL,
	[ExtraInvitationSubjectJapaneseUnicode] [nvarchar](max) NULL,
	[ExtraInvitationBodyJapaneseUnicode] [nvarchar](max) NULL,
	[ExtraReminderSubjectJapaneseUnicode] [nvarchar](max) NULL,
	[ExtraReminderBodyJapaneseUnicode] [nvarchar](max) NULL,
 CONSTRAINT [PK_ProjectRoundLang] PRIMARY KEY CLUSTERED 
(
	[ProjectRoundLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_LangIDProjectRoundID] ON [dbo].[ProjectRoundLang] 
(
	[LangID] ASC,
	[ProjectRoundID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectRound]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectRound](
	[ProjectRoundID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NULL,
	[Internal] [varchar](255) NULL,
	[Started] [smalldatetime] NULL,
	[Closed] [smalldatetime] NULL,
	[TransparencyLevel] [int] NULL,
	[RepeatedEntry] [int] NULL,
	[SurveyID] [int] NULL,
	[LangID] [int] NULL,
	[RoundKey] [uniqueidentifier] NULL,
	[EmailFromAddress] [varchar](255) NULL,
	[ReminderInterval] [int] NULL,
	[Layout] [int] NULL,
	[SelfRegistration] [int] NULL,
	[Timeframe] [int] NULL,
	[Yellow] [int] NULL,
	[Green] [int] NULL,
	[IndividualReportID] [int] NULL,
	[ExtendedSurveyID] [int] NULL,
	[ReportID] [int] NULL,
	[Logo] [int] NULL,
	[UseCode] [int] NULL,
	[ConfidentialIndividualReportID] [int] NULL,
	[SendSurveyAsEmail] [int] NULL,
	[SFTPhost] [varchar](128) NULL,
	[SFTPpath] [varchar](128) NULL,
	[SFTPuser] [varchar](16) NULL,
	[SFTPpass] [varchar](16) NULL,
	[SendSurveyAsPdfTo] [varchar](64) NULL,
	[SendSurveyAsPdfToQ] [int] NULL,
	[SendSurveyAsPdfToO] [int] NULL,
 CONSTRAINT [PK_ProjectRound] PRIMARY KEY CLUSTERED 
(
	[ProjectRoundID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_PRID_INTERNAL] ON [dbo].[ProjectRound] 
(
	[ProjectRoundID] ASC,
	[Internal] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ProjectID] ON [dbo].[ProjectRound] 
(
	[ProjectID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Project](
	[ProjectID] [int] IDENTITY(1,1) NOT NULL,
	[Internal] [varchar](255) NULL,
	[Name] [varchar](255) NULL,
	[AppURL] [varchar](255) NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[ProjectID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OptionContainer]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OptionContainer](
	[OptionContainerID] [int] IDENTITY(1,1) NOT NULL,
	[OptionContainer] [varchar](255) NULL,
 CONSTRAINT [PK_OptionContainer] PRIMARY KEY CLUSTERED 
(
	[OptionContainerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OptionComponents]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OptionComponents](
	[OptionComponentsID] [int] IDENTITY(1,1) NOT NULL,
	[OptionComponentID] [int] NULL,
	[OptionID] [int] NULL,
	[ExportValue] [int] NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_OptionPart] PRIMARY KEY CLUSTERED 
(
	[OptionComponentsID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_OptionComponentID] ON [dbo].[OptionComponents] 
(
	[OptionComponentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_OptionComponentIDOptionID] ON [dbo].[OptionComponents] 
(
	[OptionID] ASC,
	[OptionComponentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_OptionID] ON [dbo].[OptionComponents] 
(
	[OptionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OptionComponentLang]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OptionComponentLang](
	[OptionComponentLangID] [int] IDENTITY(1,1) NOT NULL,
	[OptionComponentID] [int] NULL,
	[LangID] [int] NULL,
	[Text] [text] NULL,
	[TextJapaneseUnicode] [nvarchar](max) NULL,
 CONSTRAINT [PK_OptionPartLang] PRIMARY KEY CLUSTERED 
(
	[OptionComponentLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_OptionComponentID] ON [dbo].[OptionComponentLang] 
(
	[OptionComponentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_OptionComponentIDLangID] ON [dbo].[OptionComponentLang] 
(
	[OptionComponentID] ASC,
	[LangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OptionComponentContainer]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OptionComponentContainer](
	[OptionComponentContainerID] [int] IDENTITY(1,1) NOT NULL,
	[OptionComponentContainer] [varchar](255) NULL,
 CONSTRAINT [PK_OptionComponentContainer] PRIMARY KEY CLUSTERED 
(
	[OptionComponentContainerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OptionComponent]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OptionComponent](
	[OptionComponentID] [int] IDENTITY(1,1) NOT NULL,
	[ExportValue] [int] NULL,
	[Internal] [varchar](256) NULL,
	[OptionComponentContainerID] [int] NULL,
 CONSTRAINT [PK_OptionComponent] PRIMARY KEY CLUSTERED 
(
	[OptionComponentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_OptionComponentContainerID] ON [dbo].[OptionComponent] 
(
	[OptionComponentContainerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Option]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Option](
	[OptionID] [int] IDENTITY(1,1) NOT NULL,
	[OptionType] [int] NULL,
	[OptionPlacement] [int] NULL,
	[Variablename] [varchar](256) NULL,
	[Internal] [varchar](256) NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[InnerWidth] [int] NULL,
	[OptionContainerID] [int] NULL,
	[BgColor] [varchar](16) NULL,
	[RangeLow] [decimal](18, 5) NULL,
	[RangeHigh] [decimal](18, 5) NULL,
 CONSTRAINT [PK_Option] PRIMARY KEY CLUSTERED 
(
	[OptionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_OptionContainerID] ON [dbo].[Option] 
(
	[OptionContainerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nav]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Nav](
	[NavID] [int] IDENTITY(1,1) NOT NULL,
	[NavURL] [varchar](255) NULL,
	[NavText] [varchar](255) NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_Nav] PRIMARY KEY CLUSTERED 
(
	[NavID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ManagerProjectRoundUnit]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManagerProjectRoundUnit](
	[ManagerProjectRoundUnitID] [int] IDENTITY(1,1) NOT NULL,
	[ManagerID] [int] NULL,
	[ProjectRoundID] [int] NULL,
	[ProjectRoundUnitID] [int] NULL,
 CONSTRAINT [PK_ManagerProjectRoundUnit] PRIMARY KEY CLUSTERED 
(
	[ManagerProjectRoundUnitID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManagerProjectRound]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ManagerProjectRound](
	[ManagerProjectRoundID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectRoundID] [int] NULL,
	[ManagerID] [int] NULL,
	[ShowAllUnits] [int] NULL,
	[EmailSubject] [varchar](255) NULL,
	[EmailBody] [text] NULL,
	[MPRK] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ManagerProjectRound] PRIMARY KEY CLUSTERED 
(
	[ManagerProjectRoundID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Manager]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Manager](
	[ManagerID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](255) NULL,
	[Password] [varchar](255) NULL,
	[Name] [varchar](255) NULL,
	[Phone] [varchar](255) NULL,
	[AddUser] [int] NULL,
	[SeeAnswer] [int] NULL,
	[ExpandAll] [int] NULL,
	[UseExternalID] [int] NULL,
	[SeeFeedback] [int] NULL,
	[HasFeedback] [int] NULL,
	[SeeUnit] [int] NULL,
	[SeeTerminated] [int] NULL,
	[SeeSurvey] [int] NULL,
 CONSTRAINT [PK_Manager] PRIMARY KEY CLUSTERED 
(
	[ManagerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MailQueue]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MailQueue](
	[MailQueueID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectRoundUserID] [int] NULL,
	[AdrTo] [varchar](250) NULL,
	[AdrFrom] [varchar](250) NULL,
	[Subject] [varchar](250) NULL,
	[Body] [varchar](8000) NULL,
	[Sent] [smalldatetime] NULL,
	[SendType] [int] NULL,
	[ErrorDescription] [varchar](500) NULL,
	[BodyJapaneseUnicode] [nvarchar](max) NULL,
	[SubjectJapaneseUnicode] [nvarchar](max) NULL,
	[LangID] [int] NULL,
 CONSTRAINT [PK_MailQueue] PRIMARY KEY CLUSTERED 
(
	[MailQueueID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Lang]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lang](
	[LangID] [int] IDENTITY(1,1) NOT NULL,
	[Lang] [varchar](256) NULL,
	[LangJapaneseUnicode] [nvarchar](max) NULL,
 CONSTRAINT [PK_Lang] PRIMARY KEY CLUSTERED 
(
	[LangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IdxPartComponent]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdxPartComponent](
	[IdxPartComponentID] [int] IDENTITY(1,1) NOT NULL,
	[IdxPartID] [int] NULL,
	[OptionComponentID] [int] NULL,
	[Val] [int] NULL,
 CONSTRAINT [PK_IdxPartComponent] PRIMARY KEY CLUSTERED 
(
	[IdxPartComponentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_1] ON [dbo].[IdxPartComponent] 
(
	[IdxPartID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IdxPart]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdxPart](
	[IdxPartID] [int] IDENTITY(1,1) NOT NULL,
	[IdxID] [int] NULL,
	[QuestionID] [int] NULL,
	[OptionID] [int] NULL,
	[OtherIdxID] [int] NULL,
	[Multiple] [int] NULL,
 CONSTRAINT [PK_IdxPart] PRIMARY KEY CLUSTERED 
(
	[IdxPartID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_1] ON [dbo].[IdxPart] 
(
	[IdxID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IdxLang]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IdxLang](
	[IdxLangID] [int] IDENTITY(1,1) NOT NULL,
	[IdxID] [int] NULL,
	[LangID] [int] NULL,
	[Idx] [varchar](256) NULL,
	[IdxJapaneseUnicode] [nvarchar](max) NULL,
 CONSTRAINT [PK_IdxLang] PRIMARY KEY CLUSTERED 
(
	[IdxLangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_1] ON [dbo].[IdxLang] 
(
	[IdxID] ASC,
	[LangID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Idx]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Idx](
	[IdxID] [int] IDENTITY(1,1) NOT NULL,
	[Internal] [varchar](256) NULL,
	[RequiredAnswerCount] [int] NULL,
	[AllPartsRequired] [bit] NULL,
	[MaxVal] [int] NULL,
	[SortOrder] [int] NULL,
	[TargetVal] [int] NULL,
	[YellowLow] [int] NULL,
	[GreenLow] [int] NULL,
	[GreenHigh] [int] NULL,
	[YellowHigh] [int] NULL,
	[CX] [int] NULL,
 CONSTRAINT [PK_Idx] PRIMARY KEY CLUSTERED 
(
	[IdxID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_1] ON [dbo].[Idx] 
(
	[IdxID] ASC,
	[MaxVal] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Group](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[GroupDesc] [varchar](255) NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FeedbackQuestion]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedbackQuestion](
	[FeedbackQuestionID] [int] IDENTITY(1,1) NOT NULL,
	[FeedbackID] [int] NULL,
	[QuestionID] [int] NULL,
 CONSTRAINT [PK_FeedbackQuestion] PRIMARY KEY CLUSTERED 
(
	[FeedbackQuestionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Feedback](
	[FeedbackID] [int] IDENTITY(1,1) NOT NULL,
	[Feedback] [varchar](50) NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[f_isoweek]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[f_isoweek](@date datetime)
RETURNS INT
as
BEGIN

RETURN (datepart(DY, datediff(d, 0, @date) / 7 * 7 + 3)+6) / 7
-- replaced code for yet another improvement.
--RETURN (datepart(DY, dateadd(ww, datediff(d, 0, @date) / 7, 3))+6) / 7

END
GO
/****** Object:  Table [dbo].[Debug]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Debug](
	[DebugID] [int] IDENTITY(1,1) NOT NULL,
	[DebugTxt] [nvarchar](max) NULL,
	[DT] [datetime] NULL,
 CONSTRAINT [PK_Debug] PRIMARY KEY CLUSTERED 
(
	[DebugID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomReportRow]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomReportRow](
	[CustomReportRowID] [int] IDENTITY(1,1) NOT NULL,
	[CustomReportID] [int] NULL,
	[Before] [text] NULL,
	[Editable] [text] NULL,
	[After] [text] NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
 CONSTRAINT [PK_CustomReportRow] PRIMARY KEY CLUSTERED 
(
	[CustomReportRowID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_CustomReportID] ON [dbo].[CustomReportRow] 
(
	[CustomReportID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomReport]    Script Date: 11/07/2012 21:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomReport](
	[CustomReportID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[DT] [datetime] NULL,
 CONSTRAINT [PK_CustomReport] PRIMARY KEY CLUSTERED 
(
	[CustomReportID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[cp_sendmail]    Script Date: 11/07/2012 21:37:51 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[cp_sendmail] AS
	SET NOCOUNT ON
	Declare @lang int, @iMsg int, @hr int, @cx int, @ex int, @mqid int, @PRUID int, @SendType tinyint, @temp int
	Declare @From varchar(250), @To varchar(250), @Subject varchar(250), @Body varchar(8000)
	SET @cx = 0
	SET @ex = 0
	SET @mqid = 0
	SET @lang = 0
	
	WHILE @mqid IS NOT NULL AND @cx < 250 AND @ex < 25 BEGIN
		SET @temp = @mqid
		SET @mqid = NULL
		SELECT TOP 1 @lang = LangID, @mqid = MailQueueID, @PRUID = ProjectRoundUserID, @From = AdrFrom, @To = AdrTo, @Subject = ISNULL(SubjectJapaneseUnicode,Subject), @Body = ISNULL(BodyJapaneseUnicode,Body), @SendType = SendType FROM MailQueue WHERE Sent IS NULL AND MailQueueID > @temp ORDER BY MailQueueID ASC

		IF @mqid IS NOT NULL BEGIN
			EXEC @hr = sp_OACreate 'CDO.Message', @iMsg OUT
			IF @hr = 0 BEGIN
				EXEC @hr = sp_OASetProperty @iMsg, 'Configuration.fields("http://schemas.microsoft.com/cdo/configuration/sendusing").Value','2'
			END
			IF @hr = 0 BEGIN
				EXEC @hr = sp_OASetProperty @iMsg, 'Configuration.fields("http://schemas.microsoft.com/cdo/configuration/smtpserver").Value', '212.112.175.151' 
			END
			IF @hr = 0 BEGIN
				EXEC @hr = sp_OAMethod @iMsg, 'Configuration.Fields.Update', NULL
			END
			IF @hr = 0 BEGIN
				EXEC @hr = sp_OASetProperty @iMsg, 'To', @To
			END
			IF @hr = 0 BEGIN
				EXEC @hr = sp_OASetProperty @iMsg, 'From', @From
			END
			IF @hr = 0 BEGIN
				EXEC @hr = sp_OASetProperty @iMsg, 'Subject', @Subject
			END
			IF @hr = 0 BEGIN
				EXEC @hr = sp_OASetProperty @iMsg, 'TextBody', @Body
			END
			IF @hr = 0 BEGIN
				EXEC @hr = sp_OAMethod @iMsg, 'Send', NULL
			END
		
			IF @hr = 0 BEGIN
				SET @cx = @cx + 1
				UPDATE MailQueue SET Sent = GETDATE() WHERE MailQueueID = @mqid
				IF @PRUID IS NOT NULL BEGIN
					IF @SendType = 0 BEGIN
						UPDATE ProjectRoundUser SET LastSent = GETDATE(), SendCount = SendCount + 1 WHERE ProjectRoundUserID = @PRUID
					END ELSE BEGIN
						UPDATE ProjectRoundUser SET LastSent = GETDATE(), ReminderCount = ReminderCount + 1 WHERE ProjectRoundUserID = @PRUID
					END
				END
			END ELSE BEGIN
				SET @ex = @ex + 1
				Declare @source varchar(255)
				Declare @description varchar(500)
				EXEC @hr = sp_OAGetErrorInfo NULL, @source OUT, @description OUT
				IF @hr = 0 BEGIN
					UPDATE MailQueue SET ErrorDescription = @description WHERE MailQueueID = @mqid
				END
			END
		END
	END
	
	If (@iMsg IS NOT NULL)
		BEGIN
			EXEC @hr = sp_OADestroy @iMsg
		END
	RETURN
GO
/****** Object:  StoredProcedure [dbo].[cp_fixStrangeChars]    Script Date: 11/07/2012 21:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[cp_fixStrangeChars]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	update answervalue set valuetext = replace(valuetext,'Ã?','Ä') WHERE ValueText LIKE '%Ã%'
	update answervalue set valuetext = replace(valuetext,'Ã¤','ä') WHERE ValueText LIKE '%Ã%'
	update answervalue set valuetext = replace(valuetext,'Ã¥','å') WHERE ValueText LIKE '%Ã%'
	update answervalue set valuetext = replace(valuetext,'Ã¶','ö') WHERE ValueText LIKE '%Ã%'
	update answervalue set valuetext = replace(valuetext,'Ã©','é') WHERE ValueText LIKE '%Ã%'
	update answervalue set valuetext = replace(valuetext,'ÄÆ?ÄÂ¤','ä') WHERE ValueText LIKE '%Â%'
	update answervalue set valuetext = replace(valuetext,'ÄÆ?ÄÂ¶','ö') WHERE ValueText LIKE '%Â%'
	update answervalue set valuetext = replace(valuetext,'ÄÆ?ÄÂ¥','å') WHERE ValueText LIKE '%Â%'
	update answervalue set valuetext = replace(valuetext,'ÄÂ¤','ä') WHERE ValueText LIKE '%Â%'
	update answervalue set valuetext = replace(valuetext,'ÄÂ¶','å') WHERE ValueText LIKE '%Â%'
	update answervalue set valuetext = replace(valuetext,'ÄÂ¥','å') WHERE ValueText LIKE '%Â%'
	update answervalue set valuetext = replace(valuetext,'â?','"') WHERE ValueText LIKE '%â%'

	update projectround set timeframe = 90, yellow = 20, green = 70 where timeframe is null and yellow is null and green is null

END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_yearWeek]    Script Date: 11/07/2012 21:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION  [dbo].[cf_yearWeek] (@dt AS SmallDateTime)  
RETURNS INT AS  
BEGIN 
RETURN YEAR(@dt)*52+dbo.f_isoweek(@dt)
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_yearMonthDay]    Script Date: 11/07/2012 21:37:51 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[cf_yearMonthDay] (@dt AS SmallDateTime)  
RETURNS VARCHAR(10) AS  
BEGIN 
RETURN '' + CAST(YEAR(@dt) AS VARCHAR(4)) + '-' + dbo.cf_twoDigit(CAST(MONTH(@dt) AS VARCHAR(2))) + '-' + dbo.cf_twoDigit(CAST(DAY(@dt) AS VARCHAR(2)))
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_ProjectUnitTree]    Script Date: 11/07/2012 21:37:51 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_ProjectUnitTree] (@ProjectRoundUnitID INT, @Separator VARCHAR(4))
RETURNS VARCHAR(1024) AS  
BEGIN
	DECLARE @Return VARCHAR(1024)
	SELECT @Return = Unit, @ProjectRoundUnitID = ParentProjectRoundUnitID FROM [ProjectRoundUnit] WHERE ProjectRoundUnitID = @ProjectRoundUnitID
	WHILE @ProjectRoundUnitID IS NOT NULL BEGIN
		SELECT @Return = Unit + @Separator + @Return, @ProjectRoundUnitID = ParentProjectRoundUnitID FROM [ProjectRoundUnit] WHERE ProjectRoundUnitID = @ProjectRoundUnitID
	END
	RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_projectRoundUserCount]    Script Date: 11/07/2012 21:37:51 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_projectRoundUserCount] (@To SMALLDATETIME, @ProjectRoundID INT)  
RETURNS INT AS  
BEGIN 
DECLARE @Return INT
DECLARE @From SMALLDATETIME
IF @To IS NOT NULL 
	BEGIN
		SELECT @From = DATEADD(d,-pr.Timeframe,@To) FROM ProjectRound pr WHERE ProjectRoundID = @ProjectRoundID
		SELECT @To = DATEADD(d,1,@To)
		SELECT @Return = COUNT(*) FROM ProjectRoundUser u WHERE u.Terminated IS NULL AND u.ProjectRoundID = @ProjectRoundID AND u.Created <= @To
	END
ELSE
	BEGIN
		SELECT @Return = COUNT(*) FROM ProjectRoundUser u WHERE u.Terminated IS NULL AND u.ProjectRoundID = @ProjectRoundID
	END
RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_projectRoundFinishedAnswerCount]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_projectRoundFinishedAnswerCount] (@To SMALLDATETIME, @ProjectRoundID INT)  
RETURNS INT AS  
BEGIN 
DECLARE @Return INT
DECLARE @From DATETIME
IF @To IS NOT NULL 
	BEGIN
		SELECT @From = DATEADD(d,-pr.Timeframe,@To) FROM ProjectRound pr WHERE ProjectRoundID = @ProjectRoundID
		SELECT @To = DATEADD(d,1,@To)
		SELECT @Return = COUNT(DISTINCT a.ProjectRoundUserID) FROM Answer a INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID WHERE u.Terminated IS NULL AND a.EndDT IS NOT NULL AND a.ProjectRoundID = @ProjectRoundID AND a.EndDT >= @From AND a.EndDT <= @To
		SELECT @Return = @Return + COUNT(a.AnswerID) FROM Answer a WHERE a.ProjectRoundUserID IS NULL AND a.EndDT IS NOT NULL AND a.ProjectRoundID = @ProjectRoundID AND a.EndDT >= @From AND a.EndDT <= @To
	END
ELSE
	BEGIN
		SELECT @Return = COUNT(DISTINCT a.ProjectRoundUserID) FROM Answer a INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID WHERE u.Terminated IS NULL AND a.EndDT IS NOT NULL AND a.ProjectRoundID = @ProjectRoundID
		SELECT @Return = @Return + COUNT(a.AnswerID) FROM Answer a WHERE a.ProjectRoundUserID IS NULL AND a.EndDT IS NOT NULL AND a.ProjectRoundID = @ProjectRoundID
	END
RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitSurveyID]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[cf_unitSurveyID](@ProjectRoundUnitID INT)
RETURNS INT AS  
BEGIN
	DECLARE @Return INT
	DECLARE @ProjectRoundID INT
	SELECT @ProjectRoundID = u.ProjectRoundID, @Return = u.SurveyID, @ProjectRoundUnitID = u.ParentProjectRoundUnitID FROM [ProjectRoundUnit] u WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	WHILE @ProjectRoundUnitID IS NOT NULL AND @Return = 0 BEGIN
		SELECT @ProjectRoundID = u.ProjectRoundID, @Return = u.SurveyID, @ProjectRoundUnitID = u.ParentProjectRoundUnitID FROM [ProjectRoundUnit] u WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	END
	IF @Return IS NULL OR @Return = 0 BEGIN
		SELECT @Return = r.SurveyID FROM ProjectRound r WHERE r.ProjectRoundID = @ProjectRoundID
	END
	RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitSurvey]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[cf_unitSurvey](@ProjectRoundUnitID INT)
RETURNS VARCHAR(256) AS  
BEGIN
	DECLARE @Return VARCHAR(256)
	DECLARE @ProjectRoundID INT
	SELECT @ProjectRoundID = u.ProjectRoundID, @Return = s.Internal, @ProjectRoundUnitID = u.ParentProjectRoundUnitID FROM [ProjectRoundUnit] u LEFT OUTER JOIN Survey s ON u.SurveyID = s.SurveyID WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	WHILE @ProjectRoundUnitID IS NOT NULL AND @Return IS NULL BEGIN
		SELECT @Return = s.Internal + ' *', @ProjectRoundUnitID = u.ParentProjectRoundUnitID FROM [ProjectRoundUnit] u LEFT OUTER JOIN Survey s ON u.SurveyID = s.SurveyID WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	END
	IF @Return IS NULL BEGIN
		SELECT @Return = s.Internal + ' *' FROM ProjectRound r INNER JOIN Survey s ON r.SurveyID = s.SurveyID WHERE r.ProjectRoundID = @ProjectRoundID
	END
	RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitSortString]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_unitSortString] (@ProjectRoundUnitID INT)
RETURNS VARCHAR(256) AS  
BEGIN
	DECLARE @Return VARCHAR(256)
	SELECT @Return = RIGHT('0000000' + CAST(SortOrder AS VARCHAR(8)),8), @ProjectRoundUnitID = ParentProjectRoundUnitID FROM [ProjectRoundUnit] WHERE ProjectRoundUnitID = @ProjectRoundUnitID
	WHILE @ProjectRoundUnitID IS NOT NULL BEGIN
		SELECT @Return = RIGHT('0000000' + CAST(SortOrder AS VARCHAR(8)),8) + @Return, @ProjectRoundUnitID = ParentProjectRoundUnitID FROM [ProjectRoundUnit] WHERE ProjectRoundUnitID = @ProjectRoundUnitID
	END
	RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitLangID]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[cf_unitLangID](@ProjectRoundUnitID INT)
RETURNS INT AS  
BEGIN
	DECLARE @Return INT
	DECLARE @ProjectRoundID INT
	SELECT @ProjectRoundID = u.ProjectRoundID, @Return = u.LangID, @ProjectRoundUnitID = u.ParentProjectRoundUnitID FROM [ProjectRoundUnit] u WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	WHILE @ProjectRoundUnitID IS NOT NULL AND @Return = 0 BEGIN
		SELECT @ProjectRoundID = u.ProjectRoundID, @Return = u.LangID, @ProjectRoundUnitID = u.ParentProjectRoundUnitID FROM [ProjectRoundUnit] u WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	END
	IF @Return IS NULL OR @Return = 0 BEGIN
		SELECT @Return = r.LangID FROM ProjectRound r WHERE r.ProjectRoundID = @ProjectRoundID
	END
	RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitLang]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_unitLang](@ProjectRoundUnitID INT)
RETURNS VARCHAR(256) AS  
BEGIN
	DECLARE @Return VARCHAR(256)
	DECLARE @ProjectRoundID INT
	SELECT @ProjectRoundID = u.ProjectRoundID, @Return = '<img src="../img/lang/' + CAST(l.LangID AS VARCHAR(2)) + '.gif">', @ProjectRoundUnitID = u.ParentProjectRoundUnitID FROM [ProjectRoundUnit] u LEFT OUTER JOIN Lang l ON u.LangID = l.LangID WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	WHILE @ProjectRoundUnitID IS NOT NULL AND @Return IS NULL BEGIN
		SELECT @ProjectRoundID = u.ProjectRoundID, @Return = '<img src="../img/lang/' + CAST(l.LangID AS VARCHAR(2)) + '.gif"> *', @ProjectRoundUnitID = u.ParentProjectRoundUnitID FROM [ProjectRoundUnit] u LEFT OUTER JOIN Lang l ON u.LangID = l.LangID WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	END
	IF @Return IS NULL BEGIN
		SELECT @Return = '<img src="../img/lang/' + CAST(l.LangID AS VARCHAR(2)) + '.gif"> *' FROM ProjectRound r INNER JOIN Lang l ON r.LangID = l.LangID WHERE r.ProjectRoundID = @ProjectRoundID
	END
	RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitIndividualReportID]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[cf_unitIndividualReportID](@ProjectRoundUnitID INT)
RETURNS INT AS  
BEGIN
	DECLARE @Return INT
	DECLARE @ProjectRoundID INT
	SELECT @ProjectRoundID = u.ProjectRoundID, @Return = u.IndividualReportID, @ProjectRoundUnitID = u.ParentProjectRoundUnitID FROM [ProjectRoundUnit] u WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	WHILE @ProjectRoundUnitID IS NOT NULL AND @Return = 0 BEGIN
		SELECT @ProjectRoundID = u.ProjectRoundID, @Return = u.IndividualReportID, @ProjectRoundUnitID = u.ParentProjectRoundUnitID FROM [ProjectRoundUnit] u WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	END
	IF @Return IS NULL OR @Return = 0 BEGIN
		SELECT @Return = r.IndividualReportID FROM ProjectRound r WHERE r.ProjectRoundID = @ProjectRoundID
	END
	RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitGreen]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_unitGreen](@ProjectRoundUnitID INT)
RETURNS INT AS  
BEGIN
	DECLARE @Return INT
	DECLARE @ProjectRoundID INT
	SELECT @ProjectRoundID = u.ProjectRoundID, @Return = u.Green, @ProjectRoundUnitID = u.ParentProjectRoundUnitID FROM [ProjectRoundUnit] u WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	WHILE @ProjectRoundUnitID IS NOT NULL AND @Return IS NULL BEGIN
		SELECT @ProjectRoundID = u.ProjectRoundID, @Return = u.Green, @ProjectRoundUnitID = u.ParentProjectRoundUnitID FROM [ProjectRoundUnit] u WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	END
	IF @Return IS NULL BEGIN
		SELECT @Return = ISNULL(r.Green,70) FROM ProjectRound r WHERE r.ProjectRoundID = @ProjectRoundID
	END
	RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitExtID]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[cf_unitExtID](@ProjectRoundUnitID INT, @UnitDepth INT, @ExtID VARCHAR(16))
RETURNS VARCHAR(16) AS  
BEGIN
	IF @ExtID = '' BEGIN
		SELECT @ExtID = ID FROM ProjectRoundUnit WHERE ProjectRoundUnitID = @ProjectRoundUnitID
	END
	DECLARE @ProjectRoundID INT
	SELECT @ProjectRoundID = ProjectRoundID FROM ProjectRoundUnit WHERE ProjectRoundUnitID = @ProjectRoundUnitID
	DECLARE @CX INT
	SET @CX = 0
	IF @ExtID <> '' AND @ExtID IS NOT NULL BEGIN
		SELECT @CX = COUNT(*) FROM ProjectRoundUnit WHERE ID = @ExtID AND ProjectRoundUnitID <> @ProjectRoundUnitID AND ProjectRoundID = @ProjectRoundID
	END
	IF @CX > 0 OR @ExtID = '' OR @ExtID IS NULL BEGIN
		DECLARE @BX INT
		SET @BX = 0
		SET @CX = 1
		WHILE @CX > 0 BEGIN
			SET @BX = @BX + 1
			SET @ExtID = 'level' + CAST(@UnitDepth AS VARCHAR(2)) + '-' + CAST(@BX AS VARCHAR(3))
			SELECT @CX = COUNT(*) FROM ProjectRoundUnit WHERE ID = @ExtID AND ProjectRoundUnitID <> @ProjectRoundUnitID AND ProjectRoundID = @ProjectRoundID
		END
	END
	RETURN @ExtID
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitDepth]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[cf_unitDepth] (@ProjectRoundUnitID INT)
RETURNS INT AS
BEGIN
	DECLARE @UnitDepth INT, @ParentProjectRoundUnitID INT
	SET @UnitDepth = 0
	SET @ParentProjectRoundUnitID = @ProjectRoundUnitID
	WHILE @ParentProjectRoundUnitID IS NOT NULL BEGIN
		SELECT @ParentProjectRoundUnitID=ParentProjectRoundUnitID FROM [ProjectRoundUnit] WHERE ProjectRoundUnitID = @ParentProjectRoundUnitID
		SET @UnitDepth = @UnitDepth + 1
	END
	RETURN @UnitDepth
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitAndChildrenUserSendCount]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_unitAndChildrenUserSendCount] (@Timeframe INT, @To SMALLDATETIME, @ProjectRoundUnitID INT)  
RETURNS INT AS  
BEGIN 
DECLARE @Return INT
DECLARE @From SMALLDATETIME
IF @To IS NOT NULL 
	BEGIN
		SELECT @From = DATEADD(d,-@Timeframe,@To)
		SELECT @To = DATEADD(d,1,@To)
		SELECT @Return = COUNT(*) 
		FROM ProjectRoundUser u 
		INNER JOIN ProjectRoundUnit p ON u.ProjectRoundUnitID = p.ProjectRoundUnitID 
		INNER JOIN ProjectRoundUnit pru ON pru.ProjectRoundUnitID = @ProjectRoundUnitID 
		WHERE u.Terminated IS NULL AND u.LastSent IS NOT NULL 
		AND u.LastSent >= @From
		AND LEFT(p.SortString,LEN(pru.SortString)) = pru.SortString
	END
ELSE
	BEGIN
		SELECT @Return = COUNT(*) 
		FROM ProjectRoundUser u 
		INNER JOIN ProjectRoundUnit p ON u.ProjectRoundUnitID = p.ProjectRoundUnitID 
		INNER JOIN ProjectRoundUnit pru ON pru.ProjectRoundUnitID = @ProjectRoundUnitID 
		WHERE u.Terminated IS NULL AND u.LastSent IS NOT NULL 
		AND LEFT(p.SortString,LEN(pru.SortString)) = pru.SortString
	END
RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitAndChildrenUserCount]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_unitAndChildrenUserCount] (@Timeframe INT, @To SMALLDATETIME, @ProjectRoundUnitID INT)  
RETURNS INT AS  
BEGIN 
DECLARE @Return INT
DECLARE @From DATETIME
IF @To IS NOT NULL 
	BEGIN
		SELECT @From = DATEADD(d,-@Timeframe,@To)
		SELECT @To = DATEADD(d,1,@To)
		SELECT @Return = COUNT(*) 
		FROM ProjectRoundUser u 
		INNER JOIN ProjectRoundUnit p ON u.ProjectRoundUnitID = p.ProjectRoundUnitID 
		INNER JOIN ProjectRoundUnit pru ON pru.ProjectRoundUnitID = @ProjectRoundUnitID 
		WHERE u.Terminated IS NULL AND LEFT(p.SortString,LEN(pru.SortString)) = pru.SortString AND u.Created <= @To
	END
ELSE
	BEGIN
		SELECT @Return = COUNT(*) 
		FROM ProjectRoundUser u 
		INNER JOIN ProjectRoundUnit p ON u.ProjectRoundUnitID = p.ProjectRoundUnitID 
		INNER JOIN ProjectRoundUnit pru ON pru.ProjectRoundUnitID = @ProjectRoundUnitID 
		WHERE u.Terminated IS NULL AND LEFT(p.SortString,LEN(pru.SortString)) = pru.SortString
	END
RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitAndChildrenNonfinishedAnswerCount]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_unitAndChildrenNonfinishedAnswerCount] (@Timeframe INT, @To SMALLDATETIME, @ProjectRoundUnitID INT)  
RETURNS INT AS  
BEGIN 
DECLARE @Return INT
DECLARE @From DATETIME
IF @To IS NOT NULL 
	BEGIN
		SELECT @From = DATEADD(d,-@Timeframe,@To)
		SELECT @To = DATEADD(d,1,@To)
		SELECT @Return = COUNT(DISTINCT a.ProjectRoundUserID) 
		FROM Answer a 
		INNER JOIN ProjectRoundUnit p ON a.ProjectRoundUnitID = p.ProjectRoundUnitID 
		INNER JOIN ProjectRoundUnit pru ON pru.ProjectRoundUnitID = @ProjectRoundUnitID
		INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID
		WHERE u.Terminated IS NULL 
		AND a.EndDT IS NULL 
		AND a.StartDT >= @From 
		AND a.StartDT <= @To
		AND LEFT(p.SortString,LEN(pru.SortString)) = pru.SortString
	END
ELSE
	BEGIN
		SELECT @Return = COUNT(DISTINCT a.ProjectRoundUserID) 
		FROM Answer a 
		INNER JOIN ProjectRoundUnit p ON a.ProjectRoundUnitID = p.ProjectRoundUnitID 
		INNER JOIN ProjectRoundUnit pru ON pru.ProjectRoundUnitID = @ProjectRoundUnitID 
		INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID
		WHERE u.Terminated IS NULL 
		AND a.EndDT IS NULL 
		AND LEFT(p.SortString,LEN(pru.SortString)) = pru.SortString
	END
RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitAndChildrenMaxOfAnswerOrUserCount]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[cf_unitAndChildrenMaxOfAnswerOrUserCount] (@ProjectRoundUnitID INT)  
RETURNS INT AS  
BEGIN 
DECLARE @Return INT
DECLARE @SortString VARCHAR(1024)
SELECT @SortString = SortString FROM ProjectRoundUnit WHERE ProjectRoundUnitID = @ProjectRoundUnitID
SELECT @Return = SUM(outerTemp.CX) 
FROM (
	SELECT 
	CASE 
		WHEN tmp.col1 > tmp.col2 
		THEN tmp.col1 
		ELSE tmp.col2 
	END 
	AS CX 
	FROM (
		SELECT COUNT(a.AnswerID) AS col1, (
			SELECT COUNT(*) 
			FROM ProjectRoundUser pr 
			WHERE pr.Terminated IS NULL AND pr.ProjectRoundUnitID = p.ProjectRoundUnitID
		) AS col2 
		FROM ProjectRoundUnit p 
		LEFT OUTER JOIN Answer a ON a.ProjectRoundUnitID = p.ProjectRoundUnitID 
		WHERE (LEFT(p.SortString, LEN(@SortString)) = @SortString) 
		GROUP BY p.ProjectRoundUnitID
	) tmp
) outerTemp 
RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitAndChildrenFinishedAnswerCount]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_unitAndChildrenFinishedAnswerCount](@Timeframe INT, @To SMALLDATETIME, @ProjectRoundUnitID INT)  
RETURNS INT AS  
BEGIN 
DECLARE @Return INT
DECLARE @From DATETIME
IF @To IS NOT NULL 
	BEGIN
		SELECT @From = DATEADD(d,-@Timeframe,@To)
		SELECT @To = DATEADD(d,1,@To)
		SELECT @Return = COUNT(DISTINCT a.ProjectRoundUserID) 
		FROM Answer a 
		INNER JOIN ProjectRoundUnit p ON a.ProjectRoundUnitID = p.ProjectRoundUnitID 
		INNER JOIN ProjectRoundUnit pru ON pru.ProjectRoundUnitID = @ProjectRoundUnitID 
		INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID 
		WHERE u.Terminated IS NULL
		AND a.EndDT IS NOT NULL 
		AND a.EndDT >= @From 
		AND a.EndDT <= @To
		AND LEFT(p.SortString,LEN(pru.SortString)) = pru.SortString

		SELECT @Return = @Return + COUNT(a.AnswerID) 
		FROM Answer a 
		INNER JOIN ProjectRoundUnit p ON a.ProjectRoundUnitID = p.ProjectRoundUnitID 
		INNER JOIN ProjectRoundUnit pru ON pru.ProjectRoundUnitID = @ProjectRoundUnitID 
		WHERE a.ProjectRoundUserID IS NULL 
		AND a.EndDT IS NOT NULL 
		AND a.EndDT >= @From 
		AND a.EndDT <= @To
		AND LEFT(p.SortString,LEN(pru.SortString)) = pru.SortString
	END
ELSE
	BEGIN
		SELECT @Return = COUNT(DISTINCT a.ProjectRoundUserID) 
		FROM Answer a 
		INNER JOIN ProjectRoundUnit p ON a.ProjectRoundUnitID = p.ProjectRoundUnitID 
		INNER JOIN ProjectRoundUnit pru ON pru.ProjectRoundUnitID = @ProjectRoundUnitID 
		INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID 
		WHERE u.Terminated IS NULL
		AND a.EndDT IS NOT NULL 
		AND LEFT(p.SortString,LEN(pru.SortString)) = pru.SortString

		SELECT @Return = @Return + COUNT(a.AnswerID) 
		FROM Answer a 
		INNER JOIN ProjectRoundUnit p ON a.ProjectRoundUnitID = p.ProjectRoundUnitID 
		INNER JOIN ProjectRoundUnit pru ON pru.ProjectRoundUnitID = @ProjectRoundUnitID 
		WHERE a.ProjectRoundUserID IS NULL 
		AND a.EndDT IS NOT NULL 
		AND LEFT(p.SortString,LEN(pru.SortString)) = pru.SortString
	END
RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitAndChildrenEstUserCount]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[cf_unitAndChildrenEstUserCount] (@ProjectRoundUnitID INT) 
RETURNS INT AS  
BEGIN 
DECLARE @Return INT
SELECT @Return = SUM(p.UserCount) 
FROM ProjectRoundUnit p
INNER JOIN ProjectRoundUnit pru ON pru.ProjectRoundUnitID = @ProjectRoundUnitID 
WHERE LEFT(p.SortString,LEN(pru.SortString)) = pru.SortString
RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitAndChildrenAnswerCount]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_unitAndChildrenAnswerCount] (@From SMALLDATETIME, @To SMALLDATETIME, @ProjectRoundUnitID INT)  
RETURNS INT AS  
BEGIN 
DECLARE @Return INT
SELECT @Return = COUNT(*) FROM Answer a INNER JOIN ProjectRoundUnit p ON a.ProjectRoundUnitID = p.ProjectRoundUnitID INNER JOIN ProjectRoundUnit pru ON pru.ProjectRoundUnitID = @ProjectRoundUnitID INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID WHERE u.Terminated IS NULL AND LEFT(p.SortString,LEN(pru.SortString)) = pru.SortString
RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_year2WeekEven]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_year2WeekEven] (@dt AS SmallDateTime)  
RETURNS INT AS  
BEGIN 
RETURN FLOOR((CAST((YEAR(@dt)*52+dbo.f_isoweek(@dt)) AS FLOAT))/2)
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_year2Week]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_year2Week] (@dt AS SmallDateTime)  
RETURNS INT AS  
BEGIN 
RETURN CEILING((CAST((YEAR(@dt)*52+dbo.f_isoweek(@dt)) AS FLOAT))/2)
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitYellow]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_unitYellow](@ProjectRoundUnitID INT)
RETURNS INT AS  
BEGIN
	DECLARE @Return INT
	DECLARE @ProjectRoundID INT
	SELECT @ProjectRoundID = u.ProjectRoundID, @Return = u.Yellow, @ProjectRoundUnitID = u.ParentProjectRoundUnitID FROM [ProjectRoundUnit] u WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	WHILE @ProjectRoundUnitID IS NOT NULL AND @Return IS NULL BEGIN
		SELECT @ProjectRoundID = u.ProjectRoundID, @Return = u.Yellow, @ProjectRoundUnitID = u.ParentProjectRoundUnitID FROM [ProjectRoundUnit] u WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	END
	IF @Return IS NULL BEGIN
		SELECT @Return = ISNULL(r.Yellow,40) FROM ProjectRound r WHERE r.ProjectRoundID = @ProjectRoundID
	END
	RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitUserCount]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_unitUserCount] (@Timeframe INT, @To SMALLDATETIME, @ProjectRoundUnitID INT)  
RETURNS INT AS  
BEGIN 
DECLARE @Return INT
DECLARE @From DATETIME
IF @To IS NOT NULL 
	BEGIN
		SELECT @From = DATEADD(d,-@Timeframe,@To)
		SELECT @To = DATEADD(d,1,@To)
		SELECT @Return = COUNT(*) FROM ProjectRoundUser u WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID AND u.Created <= @To
	END
ELSE
	BEGIN
		SELECT @Return = COUNT(*) FROM ProjectRoundUser u WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	END
RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitTimeframe]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_unitTimeframe](@ProjectRoundUnitID INT)
RETURNS INT AS  
BEGIN
	DECLARE @Return INT
	DECLARE @ProjectRoundID INT
	SELECT @ProjectRoundID = u.ProjectRoundID, @Return = u.Timeframe, @ProjectRoundUnitID = u.ParentProjectRoundUnitID FROM [ProjectRoundUnit] u WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	WHILE @ProjectRoundUnitID IS NOT NULL AND @Return IS NULL BEGIN
		SELECT @ProjectRoundID = u.ProjectRoundID, @Return = u.Timeframe, @ProjectRoundUnitID = u.ParentProjectRoundUnitID FROM [ProjectRoundUnit] u WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	END
	IF @Return IS NULL BEGIN
		SELECT @Return = ISNULL(r.Timeframe,30) FROM ProjectRound r WHERE r.ProjectRoundID = @ProjectRoundID
	END
	RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitSurveyKey]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[cf_unitSurveyKey](@ProjectRoundUnitID INT)
RETURNS VARCHAR(256) AS  
BEGIN
	DECLARE @Return VARCHAR(256)
	DECLARE @ProjectRoundID INT
	SELECT @ProjectRoundID = u.ProjectRoundID, @Return = s.SurveyKey, @ProjectRoundUnitID = u.ParentProjectRoundUnitID FROM [ProjectRoundUnit] u LEFT OUTER JOIN Survey s ON u.SurveyID = s.SurveyID WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	WHILE @ProjectRoundUnitID IS NOT NULL AND @Return IS NULL BEGIN
		SELECT @Return = s.SurveyKey, @ProjectRoundUnitID = u.ParentProjectRoundUnitID FROM [ProjectRoundUnit] u LEFT OUTER JOIN Survey s ON u.SurveyID = s.SurveyID WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	END
	IF @Return IS NULL BEGIN
		SELECT @Return = s.SurveyKey FROM ProjectRound r INNER JOIN Survey s ON r.SurveyID = s.SurveyID WHERE r.ProjectRoundID = @ProjectRoundID
	END
	RETURN @Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[cf_unitSurveyIntro]    Script Date: 11/07/2012 21:37:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_unitSurveyIntro](@ProjectRoundUnitID INT)
RETURNS VARCHAR(8000) AS  
BEGIN
	DECLARE @Return VARCHAR(8000)
	DECLARE @ProjectRoundID INT, @LangID INT
	
	SELECT @LangID = dbo.cf_unitLangID(@ProjectRoundUnitID), @ProjectRoundID = u.ProjectRoundID, @Return = u.SurveyIntro, @ProjectRoundUnitID = u.ParentProjectRoundUnitID 
	FROM [ProjectRoundUnit] u 
	WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	
	WHILE @ProjectRoundUnitID IS NOT NULL AND @Return IS NULL BEGIN
		SELECT @Return = u.SurveyIntro, @ProjectRoundUnitID = u.ParentProjectRoundUnitID 
		FROM [ProjectRoundUnit] u 
		WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID
	END
	IF @Return IS NULL BEGIN
		SELECT @Return = r.SurveyIntro FROM ProjectRoundLang r WHERE r.LangID = @LangID AND r.ProjectRoundID = @ProjectRoundID
	END
	RETURN @Return
END
GO
/****** Object:  Default [DF_AnswerValue_CreatedDateTime]    Script Date: 11/07/2012 21:37:48 ******/
ALTER TABLE [dbo].[AnswerValue] ADD  CONSTRAINT [DF_AnswerValue_CreatedDateTime]  DEFAULT (getdate()) FOR [CreatedDateTime]
GO
/****** Object:  Default [DF_Answer_StartDT]    Script Date: 11/07/2012 21:37:48 ******/
ALTER TABLE [dbo].[Answer] ADD  CONSTRAINT [DF_Answer_StartDT]  DEFAULT (getdate()) FOR [StartDT]
GO
/****** Object:  Default [DF_Answer_AnswerKey]    Script Date: 11/07/2012 21:37:48 ******/
ALTER TABLE [dbo].[Answer] ADD  CONSTRAINT [DF_Answer_AnswerKey]  DEFAULT (newid()) FOR [AnswerKey]
GO
/****** Object:  Default [DF_Survey_SurveyKey]    Script Date: 11/07/2012 21:37:48 ******/
ALTER TABLE [dbo].[Survey] ADD  CONSTRAINT [DF_Survey_SurveyKey]  DEFAULT (newid()) FOR [SurveyKey]
GO
/****** Object:  Default [DF_Report_ReportKey]    Script Date: 11/07/2012 21:37:48 ******/
ALTER TABLE [dbo].[Report] ADD  CONSTRAINT [DF_Report_ReportKey]  DEFAULT (newid()) FOR [ReportKey]
GO
/****** Object:  Default [DF_Question_Box]    Script Date: 11/07/2012 21:37:48 ******/
ALTER TABLE [dbo].[Question] ADD  CONSTRAINT [DF_Question_Box]  DEFAULT (0) FOR [Box]
GO
/****** Object:  Default [DF_ProjectRoundUser_UserKey]    Script Date: 11/07/2012 21:37:48 ******/
ALTER TABLE [dbo].[ProjectRoundUser] ADD  CONSTRAINT [DF_ProjectRoundUser_UserKey]  DEFAULT (newid()) FOR [UserKey]
GO
/****** Object:  Default [DF_ProjectRoundUser_SendCount]    Script Date: 11/07/2012 21:37:48 ******/
ALTER TABLE [dbo].[ProjectRoundUser] ADD  CONSTRAINT [DF_ProjectRoundUser_SendCount]  DEFAULT (0) FOR [SendCount]
GO
/****** Object:  Default [DF_ProjectRoundUser_ReminderCount]    Script Date: 11/07/2012 21:37:48 ******/
ALTER TABLE [dbo].[ProjectRoundUser] ADD  CONSTRAINT [DF_ProjectRoundUser_ReminderCount]  DEFAULT (0) FOR [ReminderCount]
GO
/****** Object:  Default [DF_ProjectRoundUser_Created]    Script Date: 11/07/2012 21:37:48 ******/
ALTER TABLE [dbo].[ProjectRoundUser] ADD  CONSTRAINT [DF_ProjectRoundUser_Created]  DEFAULT (getdate()) FOR [Created]
GO
/****** Object:  Default [DF_ProjectRoundUnit_UnitKey]    Script Date: 11/07/2012 21:37:48 ******/
ALTER TABLE [dbo].[ProjectRoundUnit] ADD  CONSTRAINT [DF_ProjectRoundUnit_UnitKey]  DEFAULT (newid()) FOR [UnitKey]
GO
/****** Object:  Default [DF_ProjectRoundUnit_CanHaveUsers]    Script Date: 11/07/2012 21:37:48 ******/
ALTER TABLE [dbo].[ProjectRoundUnit] ADD  CONSTRAINT [DF_ProjectRoundUnit_CanHaveUsers]  DEFAULT (1) FOR [CanHaveUsers]
GO
/****** Object:  Default [DF_ProjectRound_RoundKey]    Script Date: 11/07/2012 21:37:48 ******/
ALTER TABLE [dbo].[ProjectRound] ADD  CONSTRAINT [DF_ProjectRound_RoundKey]  DEFAULT (newid()) FOR [RoundKey]
GO
/****** Object:  Default [DF_ManagerProjectRound_MPRK]    Script Date: 11/07/2012 21:37:48 ******/
ALTER TABLE [dbo].[ManagerProjectRound] ADD  CONSTRAINT [DF_ManagerProjectRound_MPRK]  DEFAULT (newid()) FOR [MPRK]
GO
/****** Object:  Default [DF_Idx_AllPartsRequired]    Script Date: 11/07/2012 21:37:48 ******/
ALTER TABLE [dbo].[Idx] ADD  CONSTRAINT [DF_Idx_AllPartsRequired]  DEFAULT (0) FOR [AllPartsRequired]
GO
/****** Object:  Default [DF_Idx_MaxVal]    Script Date: 11/07/2012 21:37:48 ******/
ALTER TABLE [dbo].[Idx] ADD  CONSTRAINT [DF_Idx_MaxVal]  DEFAULT (0) FOR [MaxVal]
GO
/****** Object:  Default [DF_Debug_DT]    Script Date: 11/07/2012 21:37:48 ******/
ALTER TABLE [dbo].[Debug] ADD  CONSTRAINT [DF_Debug_DT]  DEFAULT (getdate()) FOR [DT]
GO
/****** Object:  Default [DF_CustomReport_DT]    Script Date: 11/07/2012 21:37:48 ******/
ALTER TABLE [dbo].[CustomReport] ADD  CONSTRAINT [DF_CustomReport_DT]  DEFAULT (getdate()) FOR [DT]
GO

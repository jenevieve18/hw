USE [healthWatch]
GO
/****** Object:  StoredProcedure [dbo].[cp_createSponsorExtendedSurveyDepartments]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[cp_createSponsorExtendedSurveyDepartments]
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO SponsorExtendedSurveyDepartment (SponsorExtendedSurveyID, DepartmentID) SELECT ses.SponsorExtendedSurveyID, d.DepartmentID FROM Department d INNER JOIN SponsorExtendedSurvey ses ON d.SponsorID = ses.SponsorID 
	LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON ses.SponsorExtendedSurveyID = sesd.SponsorExtendedSurveyID AND d.DepartmentID = sesd.DepartmentID
	WHERE sesd.SponsorExtendedSurveyDepartmentID IS NULL

END

GO
/****** Object:  StoredProcedure [dbo].[cp_updateEformExternalID]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[cp_updateEformExternalID]
	@SponsorExtendedSurveyID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ProjectRoundID INT, @SponsorID INT
	SELECT @ProjectRoundID = ses.ProjectRoundID, @SponsorID = ses.SponsorID FROM SponsorExtendedSurvey ses WHERE ses.SponsorExtendedSurveyID = @SponsorExtendedSurveyID
    
	UPDATE eform.dbo.ProjectRoundUnit SET eform.dbo.ProjectRoundUnit.ExternalID = dbo.Department.DepartmentShort 
	FROM dbo.Department INNER JOIN eform.dbo.ProjectRoundUnit ON dbo.Department.SponsorID = @SponsorID 
	AND eform.dbo.ProjectRoundUnit.ProjectRoundID = @ProjectRoundID
	AND dbo.cf_eFormDepartmentTree(dbo.Department.DepartmentID,'>')  COLLATE DATABASE_DEFAULT = eform.dbo.cf_ProjectUnitTree(eform.dbo.ProjectRoundUnit.ProjectRoundUnitID,'>') COLLATE DATABASE_DEFAULT
END

GO
/****** Object:  StoredProcedure [dbo].[cp_updateEformID]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[cp_updateEformID]
	@SponsorExtendedSurveyID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ProjectRoundID INT, @SponsorID INT
	SELECT @ProjectRoundID = ses.ProjectRoundID, @SponsorID = ses.SponsorID FROM SponsorExtendedSurvey ses WHERE ses.SponsorExtendedSurveyID = @SponsorExtendedSurveyID
    
	UPDATE eform.dbo.ProjectRoundUnit SET eform.dbo.ProjectRoundUnit.ID = SUBSTRING(dbo.Department.DepartmentShort,1,64)
	FROM dbo.Department INNER JOIN eform.dbo.ProjectRoundUnit ON dbo.Department.SponsorID = @SponsorID 
	AND eform.dbo.ProjectRoundUnit.ProjectRoundID = @ProjectRoundID
	AND dbo.cf_eFormDepartmentTree(dbo.Department.DepartmentID,'>')  COLLATE DATABASE_DEFAULT = eform.dbo.cf_ProjectUnitTree(eform.dbo.ProjectRoundUnit.ProjectRoundUnitID,'>') COLLATE DATABASE_DEFAULT
END

GO
/****** Object:  StoredProcedure [dbo].[cp_updateShortEformExternalID]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[cp_updateShortEformExternalID]
	@SponsorExtendedSurveyID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ProjectRoundID INT, @SponsorID INT
	SELECT @ProjectRoundID = ses.ProjectRoundID, @SponsorID = ses.SponsorID FROM SponsorExtendedSurvey ses WHERE ses.SponsorExtendedSurveyID = @SponsorExtendedSurveyID
    
	UPDATE eform.dbo.ProjectRoundUnit SET eform.dbo.ProjectRoundUnit.ExternalID = RIGHT(dbo.Department.DepartmentShort,ISNULL(NULLIF(CHARINDEX('_',REVERSE(dbo.Department.DepartmentShort),1)-1,-1),LEN(dbo.Department.DepartmentShort))) 
	FROM dbo.Department INNER JOIN eform.dbo.ProjectRoundUnit ON dbo.Department.SponsorID = @SponsorID 
	AND eform.dbo.ProjectRoundUnit.ProjectRoundID = @ProjectRoundID
	-- AND dbo.cf_eFormDepartmentTree(dbo.Department.DepartmentID,'>')  COLLATE DATABASE_DEFAULT = eform.dbo.cf_ProjectUnitTree(eform.dbo.ProjectRoundUnit.ProjectRoundUnitID,'>') COLLATE DATABASE_DEFAULT
	AND eform.dbo.ProjectRoundUnit.ID COLLATE DATABASE_DEFAULT = dbo.Department.DepartmentShort COLLATE DATABASE_DEFAULT 
END

GO
/****** Object:  StoredProcedure [dbo].[cp_updateSponsorExtendedSurveyAnswers]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[cp_updateSponsorExtendedSurveyAnswers] 
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE SponsorExtendedSurvey SET Answers = 
		ISNULL((
			SELECT COUNT(*)
			FROM SponsorInvite si
			INNER JOIN UserSponsorExtendedSurvey x ON si.UserID = x.UserID AND x.SponsorExtendedSurveyID = SponsorExtendedSurvey.SponsorExtendedSurveyID
			WHERE x.AnswerID IS NOT NULL AND si.SponsorID = SponsorExtendedSurvey.SponsorID
		),0)
		FROM eform..ProjectRound pr
		WHERE SponsorExtendedSurvey.ProjectRoundID = pr.ProjectRoundID AND (pr.Started < GETDATE() AND (pr.Closed IS NULL OR pr.Closed > GETDATE()) OR SponsorExtendedSurvey.Answers IS NULL)

END


GO
/****** Object:  StoredProcedure [dbo].[cp_updateSponsorExtendedSurveyDepartmentAnswers]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[cp_updateSponsorExtendedSurveyDepartmentAnswers] 
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE SponsorExtendedSurveyDepartment SET Answers = 
		ISNULL((
			SELECT COUNT(*)
			FROM SponsorInvite si
			INNER JOIN Department d ON si.DepartmentID = d.DepartmentID 
			INNER JOIN UserSponsorExtendedSurvey x ON si.UserID = x.UserID AND x.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID
			WHERE LEFT(d.SortString,LEN(xd.SortString)) = xd.SortString AND x.AnswerID IS NOT NULL AND si.SponsorID = ses.SponsorID
		),0)
		FROM SponsorExtendedSurveyDepartment xsesd
		INNER JOIN Department xd ON xsesd.DepartmentID = xd.DepartmentID
		INNER JOIN SponsorExtendedSurvey ses ON xsesd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID
		INNER JOIN eform..ProjectRound pr ON ses.ProjectRoundID = pr.ProjectRoundID		
		WHERE SponsorExtendedSurveyDepartmentID = xsesd.SponsorExtendedSurveyDepartmentID AND (pr.Started < GETDATE() AND (pr.Closed IS NULL OR pr.Closed > GETDATE()) OR xsesd.Answers IS NULL)

END


GO
/****** Object:  StoredProcedure [dbo].[cp_updateSponsorExtendedSurveyDepartmentTotal]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[cp_updateSponsorExtendedSurveyDepartmentTotal] 
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE SponsorExtendedSurveyDepartment SET Total = 
		ISNULL((
			SELECT COUNT(*)
			FROM SponsorInvite si
			INNER JOIN Department d ON si.DepartmentID = d.DepartmentID 
			LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON ses.SponsorExtendedSurveyID = sesd.SponsorExtendedSurveyID AND sesd.DepartmentID = si.DepartmentID
			WHERE LEFT(d.SortString,LEN(xd.SortString)) = xd.SortString AND sesd.Hide IS NULL AND si.SponsorID = ses.SponsorID AND (si.StoppedReason IS NULL OR si.StoppedPercent IS NOT NULL)
		),0)
		FROM SponsorExtendedSurveyDepartment xsesd
		INNER JOIN Department xd ON xsesd.DepartmentID = xd.DepartmentID
		INNER JOIN SponsorExtendedSurvey ses ON xsesd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID
		INNER JOIN eform..ProjectRound pr ON ses.ProjectRoundID = pr.ProjectRoundID		
		WHERE SponsorExtendedSurveyDepartmentID = xsesd.SponsorExtendedSurveyDepartmentID AND (pr.Started < GETDATE() AND (pr.Closed IS NULL OR pr.Closed > GETDATE()) OR xsesd.Total IS NULL OR xsesd.Total = 0 AND xsesd.Answers > 0)

END


GO
/****** Object:  StoredProcedure [dbo].[cp_updateSponsorExtendedSurveyTotal]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[cp_updateSponsorExtendedSurveyTotal] 
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE SponsorExtendedSurvey SET Total = 
		ISNULL((
			SELECT COUNT(*)
			FROM SponsorInvite si
			LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON SponsorExtendedSurvey.SponsorExtendedSurveyID = sesd.SponsorExtendedSurveyID AND sesd.DepartmentID = si.DepartmentID
			WHERE sesd.Hide IS NULL AND si.SponsorID = SponsorExtendedSurvey.SponsorID AND (si.StoppedReason IS NULL OR si.StoppedPercent IS NOT NULL)
		),0)
		FROM eform..ProjectRound pr
		WHERE SponsorExtendedSurvey.ProjectRoundID = pr.ProjectRoundID AND (pr.Started < GETDATE() AND (pr.Closed IS NULL OR pr.Closed > GETDATE()) OR SponsorExtendedSurvey.Total IS NULL)

END

GO
/****** Object:  UserDefinedFunction [dbo].[cf_daysFromLastLogin]    Script Date: 2017-06-20 11:15:42 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_departmentDepth]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_departmentDepth] (@DepartmentID INT)
RETURNS INT AS
BEGIN
	DECLARE @UnitDepth INT, @ParentDepartmentID INT, @TmpParentDepartmentID INT
	SET @UnitDepth = 0
	SET @ParentDepartmentID = @DepartmentID
	WHILE @ParentDepartmentID IS NOT NULL BEGIN
		SET @TmpParentDepartmentID = @ParentDepartmentID
		SET @ParentDepartmentID = NULL

		SELECT @ParentDepartmentID=ParentDepartmentID FROM [Department] WHERE DepartmentID = @TmpParentDepartmentID
		SET @UnitDepth = @UnitDepth + 1
	END
	RETURN @UnitDepth
END



GO
/****** Object:  UserDefinedFunction [dbo].[cf_DepartmentShortTree]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_DepartmentShortTree] (@DepartmentID INT, @Separator VARCHAR(4))
RETURNS VARCHAR(1024) AS  
BEGIN
	DECLARE @Return VARCHAR(1024), @TEMPID INT
	SET @TEMPID = NULL
	SELECT @Return = LTRIM(RTRIM(DepartmentShort)), @DepartmentID = ParentDepartmentID FROM [Department] WHERE DepartmentID = @DepartmentID
	WHILE @DepartmentID IS NOT NULL AND (@TEMPID IS NULL OR @TEMPID <> @DepartmentID) BEGIN
		SET @TEMPID = @DepartmentID
		SELECT @Return = LTRIM(RTRIM(DepartmentShort)) + @Separator + @Return, @DepartmentID = ParentDepartmentID FROM [Department] WHERE DepartmentID = @DepartmentID
	END
	IF @TEMPID IS NOT NULL AND @TEMPID = @DepartmentID BEGIN
		RETURN 'ERROR'
	END
	RETURN @Return
END








GO
/****** Object:  UserDefinedFunction [dbo].[cf_departmentSortString]    Script Date: 2017-06-20 11:15:42 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_DepartmentTree]    Script Date: 2017-06-20 11:15:42 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_eFormDepartmentTree]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_eFormDepartmentTree] (@DepartmentID INT, @Separator VARCHAR(4))
RETURNS VARCHAR(1024) AS  
BEGIN
	DECLARE @Return VARCHAR(1024), @TEMPID INT, @SponsorID INT
	SET @TEMPID = NULL
	SELECT @SponsorID = SponsorID, @Return = LTRIM(RTRIM(Department)), @DepartmentID = ParentDepartmentID FROM [Department] WHERE DepartmentID = @DepartmentID
	WHILE @DepartmentID IS NOT NULL AND (@TEMPID IS NULL OR @TEMPID <> @DepartmentID) BEGIN
		SET @TEMPID = @DepartmentID
		SELECT @Return = LTRIM(RTRIM(Department)) + @Separator + @Return, @DepartmentID = ParentDepartmentID FROM [Department] WHERE DepartmentID = @DepartmentID
	END
	IF @TEMPID IS NOT NULL AND @TEMPID = @DepartmentID BEGIN
		RETURN 'ERROR'
	END
	SELECT @Return = LTRIM(RTRIM(Sponsor)) + @Separator + @Return FROM Sponsor WHERE SponsorID = @SponsorID
	RETURN @Return
END








GO
/****** Object:  UserDefinedFunction [dbo].[cf_hourMinute]    Script Date: 2017-06-20 11:15:42 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_hourMinutes]    Script Date: 2017-06-20 11:15:42 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_lastSubmission]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_lastSubmission] 
(
	@ProjectRoundUnitID INT,
	@UserID INT
)
RETURNS DATETIME
AS
BEGIN
	DECLARE @Ret DATETIME

	SELECT TOP 1 @Ret = a.DT FROM UserProjectRoundUserAnswer a INNER JOIN UserProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID WHERE u.ProjectRoundUnitID = @ProjectRoundUnitID AND u.UserID = @UserID ORDER BY a.DT DESC

	IF @Ret IS NULL BEGIN
		SET @Ret = '1970-01-01'
	END

	RETURN @Ret

END

GO
/****** Object:  UserDefinedFunction [dbo].[cf_monthsSinceRegistration]    Script Date: 2017-06-20 11:15:42 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_sessionMinutes]    Script Date: 2017-06-20 11:15:42 ******/
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

	IF @ret < 0 BEGIN
		SELECT @ret = 0
	END

	-- Return the result of the function
	RETURN ISNULL(@ret,0)

END

GO
/****** Object:  UserDefinedFunction [dbo].[cf_UserCreatedReferrer]    Script Date: 2017-06-20 11:15:42 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_yearMonthDay]    Script Date: 2017-06-20 11:15:42 ******/
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
/****** Object:  Table [dbo].[Absence]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Absence](
	[AbsenceID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[Month] [int] NULL,
	[Minutes] [int] NULL,
	[TypeID] [int] NULL,
 CONSTRAINT [PK_Absence] PRIMARY KEY CLUSTERED 
(
	[AbsenceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AdminNews]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminNews](
	[AdminNewsID] [int] IDENTITY(1,1) NOT NULL,
	[DT] [datetime] NULL,
	[News] [nvarchar](max) NULL,
 CONSTRAINT [PK_AdminNews] PRIMARY KEY CLUSTERED 
(
	[AdminNewsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Affiliate]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BA]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BALang]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BQ]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BQLang]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BQvisibility]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CX]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CX](
	[CXID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_CX] PRIMARY KEY CLUSTERED 
(
	[CXID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Department]    Script Date: 2017-06-20 11:15:42 ******/
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
	[DepartmentShort] [varchar](64) NULL,
	[DepartmentAnonymized] [varchar](16) NULL,
	[PreviewExtendedSurveys] [int] NULL,
	[MinUserCountToDisclose] [int] NULL,
	[SortString] [nvarchar](128) NULL,
	[DepartmentKey] [uniqueidentifier] NULL,
	[LoginDays] [int] NULL,
	[LoginWeekday] [int] NULL,
	[SortStringLength]  AS (len([SortString])),
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Diary]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Exercise]    Script Date: 2017-06-20 11:15:42 ******/
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
	[Status] [int] NULL,
	[Script] [text] NULL,
 CONSTRAINT [PK_Exercise] PRIMARY KEY CLUSTERED 
(
	[ExerciseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExerciseArea]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExerciseAreaLang]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExerciseCategory]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExerciseCategoryLang]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExerciseLang]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExerciseMiracle]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExerciseStats]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExerciseType]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExerciseTypeLang]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExerciseVariant]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExerciseVariantLang]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FAQ]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FAQ](
	[FAQID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[FAQID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FAQLang]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FAQLang](
	[FAQLangID] [int] IDENTITY(1,1) NOT NULL,
	[FAQID] [int] NULL,
	[LangID] [int] NULL,
	[Question] [varchar](255) NULL,
	[Answer] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[FAQLangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[FeedbackID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[EmailSubject] [nvarchar](max) NULL,
	[EmailBody] [nvarchar](max) NULL,
	[EmailFrom] [nvarchar](max) NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FeedbackInstance]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedbackInstance](
	[FeedbackInstanceID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[DT] [datetime] NULL,
	[Body] [nvarchar](max) NULL,
	[FeedbackID] [int] NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[ReviewedBy] [nvarchar](max) NULL,
	[Released] [smalldatetime] NULL,
 CONSTRAINT [PK_FeedbackInstance] PRIMARY KEY CLUSTERED 
(
	[FeedbackInstanceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FeedbackPart]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedbackPart](
	[FeedbackPartID] [int] IDENTITY(1,1) NOT NULL,
	[FeedbackID] [int] NULL,
	[MeasureID] [int] NULL,
	[Chart] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[AllOk] [nvarchar](max) NULL,
	[Repeat] [nvarchar](max) NULL,
	[Aggregate] [nvarchar](max) NULL,
	[Required] [int] NULL,
 CONSTRAINT [PK_FeedbackPart] PRIMARY KEY CLUSTERED 
(
	[FeedbackPartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FeedbackPartLevel]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedbackPartLevel](
	[FeedbackPartLevelID] [int] IDENTITY(1,1) NOT NULL,
	[IfAbove] [int] NULL,
	[AlertText] [nvarchar](max) NULL,
	[Also] [nvarchar](max) NULL,
	[NotAlso] [nvarchar](max) NULL,
	[FeedbackPartID] [int] NULL,
 CONSTRAINT [PK_FeedbackPartLevel] PRIMARY KEY CLUSTERED 
(
	[FeedbackPartLevelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FeedbackPartRef]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedbackPartRef](
	[FeedbackPartRefID] [int] IDENTITY(1,1) NOT NULL,
	[FeedbackPartID] [int] NULL,
	[MeasureComponentID] [int] NULL,
 CONSTRAINT [PK_FeedbackPartRef] PRIMARY KEY CLUSTERED 
(
	[FeedbackPartRefID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FeedbackPartRefRow]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedbackPartRefRow](
	[FeedbackPartRefRowID] [int] IDENTITY(1,1) NOT NULL,
	[FeedbackPartRefID] [int] NULL,
	[IfOnOrAbove] [int] NULL,
	[IfOnOrAboveAge] [int] NULL,
	[Val] [int] NULL,
 CONSTRAINT [PK_FeedbackPartRefRow] PRIMARY KEY CLUSTERED 
(
	[FeedbackPartRefRowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FileUpload]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FileUpload](
	[FileUploadID] [int] IDENTITY(1,1) NOT NULL,
	[Filename] [varchar](255) NULL,
	[Organisation] [varchar](255) NULL,
	[Description] [varchar](255) NULL,
 CONSTRAINT [PK_FileUpload] PRIMARY KEY CLUSTERED 
(
	[FileUploadID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Issue]    Script Date: 2017-06-20 11:15:42 ******/
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
	[Status] [int] NULL,
 CONSTRAINT [PK_Issue] PRIMARY KEY CLUSTERED 
(
	[IssueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Lang]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LID]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ManagerFunction]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ManagerFunctionLang]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Measure]    Script Date: 2017-06-20 11:15:42 ******/
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
	[ShowCategoryInList] [int] NULL,
	[CustomChart] [int] NULL,
 CONSTRAINT [PK_Measure] PRIMARY KEY CLUSTERED 
(
	[MeasureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MeasureCategory]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MeasureCategoryLang]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MeasureComponent]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MeasureComponentLang]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MeasureComponentPart]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MeasureLang]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MeasureType]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MeasureTypeLang]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProfileComparison]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProfileComparisonBQ]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reminder]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Session]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sponsor]    Script Date: 2017-06-20 11:15:42 ******/
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
	[MinUserCountToDisclose] [int] NULL,
	[Comment] [text] NULL,
	[EmailFrom] [nvarchar](255) NULL,
	[ExternalID] [int] NULL,
	[TreatmentOfferNoAttach] [int] NULL,
	[DefaultPlotType] [int] NULL,
	[Enable2FA] [int] NULL,
 CONSTRAINT [PK_Sponsor] PRIMARY KEY CLUSTERED 
(
	[SponsorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorAdmin]    Script Date: 2017-06-20 11:15:42 ******/
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
	[ReadOnly] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[SponsorAdmin] ADD [LastName] [varchar](255) NULL
ALTER TABLE [dbo].[SponsorAdmin] ADD [PermanentlyDeleteUsers] [int] NULL
ALTER TABLE [dbo].[SponsorAdmin] ADD [InviteSubject] [text] NULL
ALTER TABLE [dbo].[SponsorAdmin] ADD [InviteTxt] [text] NULL
ALTER TABLE [dbo].[SponsorAdmin] ADD [InviteReminderSubject] [text] NULL
ALTER TABLE [dbo].[SponsorAdmin] ADD [InviteReminderTxt] [text] NULL
ALTER TABLE [dbo].[SponsorAdmin] ADD [AllMessageSubject] [text] NULL
ALTER TABLE [dbo].[SponsorAdmin] ADD [AllMessageBody] [text] NULL
ALTER TABLE [dbo].[SponsorAdmin] ADD [InviteLastSent] [smalldatetime] NULL
ALTER TABLE [dbo].[SponsorAdmin] ADD [InviteReminderLastSent] [smalldatetime] NULL
ALTER TABLE [dbo].[SponsorAdmin] ADD [AllMessageLastSent] [smalldatetime] NULL
ALTER TABLE [dbo].[SponsorAdmin] ADD [LoginLastSent] [smalldatetime] NULL
SET ANSI_PADDING ON
ALTER TABLE [dbo].[SponsorAdmin] ADD [UniqueKey] [varchar](255) NULL
ALTER TABLE [dbo].[SponsorAdmin] ADD [UniqueKeyUsed] [int] NULL
ALTER TABLE [dbo].[SponsorAdmin] ADD [ExternalUserKey] [nvarchar](max) NULL
 CONSTRAINT [PK_SponsorAdmin] PRIMARY KEY CLUSTERED 
(
	[SponsorAdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorAdminDepartment]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorAdminExercise]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorAdminExercise](
	[SponsorAdminExerciseID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[SponsorAdminID] [int] NULL,
	[ExerciseVariantLangID] [int] NULL,
	[Comments] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[SponsorAdminExerciseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorAdminExerciseDataInput]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorAdminExerciseDataInput](
	[SponsorAdminExerciseDataInputID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorAdminExerciseID] [int] NULL,
	[ValueText] [text] NULL,
	[SortOrder] [int] NULL,
	[ValueInt] [int] NULL,
	[Type] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SponsorAdminExerciseDataInputID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorAdminExerciseDataInputComponent]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SponsorAdminExerciseDataInputComponent](
	[SponsorAdminExerciseDataInputComponentID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorAdminExerciseDataInputID] [int] NULL,
	[ValueText] [varchar](255) NULL,
	[SortOrder] [int] NULL,
	[ValueInt] [int] NULL,
	[Class] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[SponsorAdminExerciseDataInputComponentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorAdminExtendedSurvey]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorAdminExtendedSurvey](
	[SponsorAdminExtendedSurveyID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorAdminID] [int] NULL,
	[EmailSubject] [text] NULL,
	[EmailBody] [text] NULL,
	[FinishedEmailSubject] [text] NULL,
	[FinishedEmailBody] [text] NULL,
	[EmailLastSent] [smalldatetime] NULL,
	[FinishedLastSent] [smalldatetime] NULL,
	[SponsorExtendedSurveyID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SponsorAdminExtendedSurveyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorAdminFunction]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorAdminSession]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorAdminSession](
	[SponsorAdminSessionID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorAdminID] [int] NULL,
	[DT] [datetime] NULL,
	[EndDT] [datetime] NULL,
 CONSTRAINT [PK_SponsorAdminSession] PRIMARY KEY CLUSTERED 
(
	[SponsorAdminSessionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorAdminSessionFunction]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorAdminSessionFunction](
	[SponsorAdminSessionFunctionID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorAdminSessionID] [int] NULL,
	[ManagerFunctionID] [int] NULL,
	[DT] [datetime] NULL,
	[EndDT] [datetime] NULL,
 CONSTRAINT [PK_SponsorAdminSessionFunction] PRIMARY KEY CLUSTERED 
(
	[SponsorAdminSessionFunctionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorBQ]    Script Date: 2017-06-20 11:15:42 ******/
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
	[DeriveBQID] [int] NULL,
 CONSTRAINT [PK_SponsorBQ] PRIMARY KEY CLUSTERED 
(
	[SponsorBQID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorBQderive]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorBQderive](
	[SponsorBQderiveID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorID] [int] NULL,
	[BQID] [int] NULL,
	[BAID] [int] NULL,
	[DeriveBAID] [int] NULL,
 CONSTRAINT [PK_SponsorBQderive] PRIMARY KEY CLUSTERED 
(
	[SponsorBQderiveID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorExtendedSurvey]    Script Date: 2017-06-20 11:15:42 ******/
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
	[Total] [int] NULL,
	[Answers] [int] NULL,
 CONSTRAINT [PK_SponsorExtendedSurvey] PRIMARY KEY CLUSTERED 
(
	[SponsorExtendedSurveyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorExtendedSurveyBA]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorExtendedSurveyBQ]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorExtendedSurveyDepartment]    Script Date: 2017-06-20 11:15:42 ******/
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
	[TreatmentOfferEmail] [nvarchar](50) NULL,
	[Total] [int] NULL,
	[Answers] [int] NULL,
 CONSTRAINT [PK_SponsorExtendedSurveyDepartment] PRIMARY KEY CLUSTERED 
(
	[SponsorExtendedSurveyDepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorInvite]    Script Date: 2017-06-20 11:15:42 ******/
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
	[DupeCheck] [nvarchar](max) NULL,
	[ExternalUserKey] [nvarchar](max) NULL,
	[StoppedPercent] [int] NULL,
 CONSTRAINT [PK_SponsorInvite] PRIMARY KEY CLUSTERED 
(
	[SponsorInviteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorInviteAbsence]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorInviteAbsence](
	[SponsorInviteAbsenceID] [int] NULL,
	[SponsorInviteID] [int] NULL,
	[Month] [int] NULL,
	[TypeID] [int] NULL,
	[Minutes] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorInviteBQ]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorInviteTest]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorInviteTest](
	[SponsorInviteTestID] [int] IDENTITY(1,1) NOT NULL,
	[DT] [smalldatetime] NULL,
	[TestData] [nvarchar](max) NULL,
	[TestID] [int] NULL,
	[SponsorInviteID] [int] NULL,
	[TestTypeID] [int] NULL,
 CONSTRAINT [PK_SponsorInviteTest] PRIMARY KEY CLUSTERED 
(
	[SponsorInviteTestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorInviteTestMeasureComponent]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorInviteTestMeasureComponent](
	[SponsorInviteTestMeasureComponentID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorInviteTestID] [int] NULL,
	[MeasureComponentID] [int] NULL,
	[ValInt] [int] NULL,
	[ValDec] [decimal](18, 10) NULL,
	[ValTxt] [text] NULL,
	[UserMeasureID] [int] NULL,
 CONSTRAINT [PK_SponsorInviteTestMeasureComponent] PRIMARY KEY CLUSTERED 
(
	[SponsorInviteTestMeasureComponentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorLang]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorLogo]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorProject]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorProject](
	[SponsorProjectID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorID] [int] NULL,
	[StartDT] [smalldatetime] NULL,
	[EndDT] [smalldatetime] NULL,
	[ProjectName] [nvarchar](max) NULL,
	[ProjectDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_SponsorProjectMeasure] PRIMARY KEY CLUSTERED 
(
	[SponsorProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorProjectMeasure]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorProjectMeasure](
	[SponsorProjectMeasureID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorProjectID] [int] NULL,
	[MeasureID] [int] NULL,
 CONSTRAINT [PK_SponsorProjectMeasure_1] PRIMARY KEY CLUSTERED 
(
	[SponsorProjectMeasureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorProjectRoundUnit]    Script Date: 2017-06-20 11:15:42 ******/
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
	[OnlyEveryDays] [int] NULL,
	[GoToStatistics] [int] NULL,
	[DefaultAggregation] [int] NULL,
 CONSTRAINT [PK_SponsorProjectRoundUnit] PRIMARY KEY CLUSTERED 
(
	[SponsorProjectRoundUnitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorProjectRoundUnitDepartment]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SponsorProjectRoundUnitDepartment](
	[SponsorProjectRoundUnitDepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorProjectRoundUnitID] [int] NULL,
	[DepartmentID] [int] NULL,
	[ReportID] [int] NULL,
 CONSTRAINT [PK_SponsorProjectRoundUnitDepartment] PRIMARY KEY CLUSTERED 
(
	[SponsorProjectRoundUnitDepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorProjectRoundUnitLang]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorPRUBQmap]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorPRUBQmapVal]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SuperAdmin]    Script Date: 2017-06-20 11:15:42 ******/
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
	[HideClosedSponsors] [int] NULL,
 CONSTRAINT [PK_SuperAdmin] PRIMARY KEY CLUSTERED 
(
	[SuperAdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SuperAdminSponsor]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SuperSponsor]    Script Date: 2017-06-20 11:15:42 ******/
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
	[Comment] [varchar](64) NULL,
	[SuperSponsorKey] [uniqueidentifier] NULL,
 CONSTRAINT [PK_SuperSponsor] PRIMARY KEY CLUSTERED 
(
	[SuperSponsorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SuperSponsorLang]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemSettings]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SystemSettingsLang]    Script Date: 2017-06-20 11:15:42 ******/
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
	[ReminderPushTitle] [nvarchar](max) NULL,
	[ReminderPushBody] [nvarchar](max) NULL,
 CONSTRAINT [PK_SystemSettingsLang] PRIMARY KEY CLUSTERED 
(
	[SystemSettingsLangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 2017-06-20 11:15:42 ******/
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
	[DupeCheck] [nvarchar](max) NULL,
	[Consent] [smalldatetime] NULL,
	[ExternalUserKey] [nvarchar](max) NULL,
	[Enable2FA] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserLogin]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserLogin](
	[UserLoginID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[IPAddress] [varchar](255) NULL,
	[LoginAttempt] [datetime] NULL,
	[ResourceID] [varchar](255) NULL,
	[UserToken] [varchar](255) NULL,
	[ActiveLoginAttempt] [int] NULL,
	[Unblocked] [int] NULL,
	[FromWebService] [int] NULL,
	[FromWebsite] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserLoginID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserMeasure]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserMeasureComponent]    Script Date: 2017-06-20 11:15:42 ******/
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
	[Corrected] [int] NULL,
 CONSTRAINT [PK_UserMeasureComponent] PRIMARY KEY CLUSTERED 
(
	[UserMeasureComponentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserProfileBQ]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserProjectRoundUser]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserProjectRoundUserAnswer]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRegistrationID]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRegistrationID](
	[UserRegistrationID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[RegistrationID] [nvarchar](max) NULL,
	[PhoneName] [nvarchar](max) NULL,
	[Inactive] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserRegistrationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserSecret]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserSecret](
	[UserSecretID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[SecretKey] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserSecretID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserSession]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserSession](
	[UserSessionID] [int] IDENTITY(1,1) NOT NULL,
	[UserHostAddress] [varchar](255) NULL,
	[UserAgent] [varchar](255) NULL,
	[LangID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserSessionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserSponsorExtendedSurvey]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserSponsorProject]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSponsorProject](
	[UserSponsorProjectID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[SponsorProjectID] [int] NULL,
	[ConsentDT] [smalldatetime] NULL,
 CONSTRAINT [PK_UserSponsorProject] PRIMARY KEY CLUSTERED 
(
	[UserSponsorProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserToken]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserToken](
	[UserToken] [uniqueidentifier] NOT NULL,
	[UserID] [int] NOT NULL,
	[Expires] [smalldatetime] NOT NULL,
	[SessionID] [int] NULL,
 CONSTRAINT [PK_UserToken] PRIMARY KEY CLUSTERED 
(
	[UserToken] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Wise]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WiseLang]    Script Date: 2017-06-20 11:15:42 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  UserDefinedFunction [dbo].[FindDepartmentWithReminder]    Script Date: 2017-06-20 11:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[FindDepartmentWithReminder](@DepartmentID INTEGER)
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
GO
ALTER TABLE [dbo].[Department] ADD  CONSTRAINT [DF_Department_DepartmentKey]  DEFAULT (newid()) FOR [DepartmentKey]
GO
ALTER TABLE [dbo].[Diary] ADD  CONSTRAINT [DF_Diary_Created]  DEFAULT (getdate()) FOR [CreatedDT]
GO
ALTER TABLE [dbo].[ExerciseLang] ADD  CONSTRAINT [DF_ExerciseLang_New]  DEFAULT ((0)) FOR [New]
GO
ALTER TABLE [dbo].[ExerciseMiracle] ADD  CONSTRAINT [DF_ExerciseMiracle_DateTime]  DEFAULT (getdate()) FOR [DateTime]
GO
ALTER TABLE [dbo].[ExerciseMiracle] ADD  CONSTRAINT [DF_ExerciseMiracle_AllowPublish]  DEFAULT ((0)) FOR [AllowPublish]
GO
ALTER TABLE [dbo].[ExerciseMiracle] ADD  CONSTRAINT [DF_ExerciseMiracle_Published]  DEFAULT ((0)) FOR [Published]
GO
ALTER TABLE [dbo].[ExerciseStats] ADD  CONSTRAINT [DF_ExerciseStats_DateTime]  DEFAULT (getdate()) FOR [DateTime]
GO
ALTER TABLE [dbo].[FeedbackInstance] ADD  CONSTRAINT [DF_FeedbackInstance_DT]  DEFAULT (getdate()) FOR [DT]
GO
ALTER TABLE [dbo].[Reminder] ADD  CONSTRAINT [DF_Reminder_DT]  DEFAULT (getdate()) FOR [DT]
GO
ALTER TABLE [dbo].[Session] ADD  CONSTRAINT [DF_Session_DT]  DEFAULT (getdate()) FOR [DT]
GO
ALTER TABLE [dbo].[Session] ADD  CONSTRAINT [DF_Session_AutoEnded]  DEFAULT ((0)) FOR [AutoEnded]
GO
ALTER TABLE [dbo].[Sponsor] ADD  DEFAULT (newid()) FOR [SponsorKey]
GO
ALTER TABLE [dbo].[Sponsor] ADD  CONSTRAINT [DF_Sponsor_SponsorApiKey]  DEFAULT (newid()) FOR [SponsorApiKey]
GO
ALTER TABLE [dbo].[SponsorAdmin] ADD  CONSTRAINT [DF_SponsorAdmin_SponsorAdminKey]  DEFAULT (newid()) FOR [SponsorAdminKey]
GO
ALTER TABLE [dbo].[SponsorInvite] ADD  CONSTRAINT [DF_SponsorInvite_InvitationKey]  DEFAULT (newid()) FOR [InvitationKey]
GO
ALTER TABLE [dbo].[SponsorProjectRoundUnit] ADD  CONSTRAINT [DF_SponsorProjectRoundUnit_SurveyKey]  DEFAULT (newid()) FOR [SurveyKey]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_UserKey]  DEFAULT (newid()) FOR [UserKey]
GO
ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[UserProjectRoundUserAnswer] ADD  CONSTRAINT [DF_UserProjectRoundUserAnswer_DT]  DEFAULT (getdate()) FOR [DT]
GO
ALTER TABLE [dbo].[UserToken] ADD  CONSTRAINT [DF_UserToken_UserToken]  DEFAULT (newid()) FOR [UserToken]
GO

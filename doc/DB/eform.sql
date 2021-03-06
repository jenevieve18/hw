USE [eForm]
GO
/****** Object:  StoredProcedure [dbo].[cp_fixStrangeChars]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  StoredProcedure [dbo].[cp_MoveUser]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[cp_MoveUser]
	@ProjectRoundUserID INT, @ProjectRoundUnitID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF @ProjectRoundUserID IS NOT NULL BEGIN

	-- get new projectroundid
	DECLARE @NewProjectRoundID INT
	SELECT @NewProjectRoundID = ProjectRoundID FROM ProjectRoundUnit WHERE ProjectRoundUnitID = @ProjectRoundUnitID

	-- decrease usercount on unit where user is on now
	UPDATE dbo.ProjectRoundUnit SET dbo.ProjectRoundUnit.UserCount = dbo.ProjectRoundUnit.UserCount-1
	FROM dbo.ProjectRoundUnit INNER JOIN dbo.ProjectRoundUser ON dbo.ProjectRoundUnit.ProjectRoundUnitID = dbo.ProjectRoundUser.ProjectRoundUnitID 
	WHERE dbo.ProjectRoundUser.ProjectRoundUserID = @ProjectRoundUserID

	-- set old projectroundunitid
	UPDATE ProjectRoundUser SET OldProjectRoundUnitID = ProjectRoundUnitID WHERE ProjectRoundUserID = @ProjectRoundUserID

	-- move user
	UPDATE dbo.ProjectRoundUser SET dbo.ProjectRoundUser.ProjectRoundUnitID = @ProjectRoundUnitID, dbo.ProjectRoundUser.ProjectRoundID = @NewProjectRoundID WHERE dbo.ProjectRoundUser.ProjectRoundUserID = @ProjectRoundUserID

	-- move answer
	UPDATE dbo.Answer SET dbo.Answer.ProjectRoundUnitID = @ProjectRoundUnitID, dbo.Answer.ProjectRoundID = @NewProjectRoundID WHERE dbo.Answer.ProjectRoundUserID = @ProjectRoundUserID

	-- increase usercount on unit where user is on now
	UPDATE dbo.ProjectRoundUnit SET dbo.ProjectRoundUnit.UserCount = dbo.ProjectRoundUnit.UserCount+1
	FROM dbo.ProjectRoundUnit INNER JOIN dbo.ProjectRoundUser ON dbo.ProjectRoundUnit.ProjectRoundUnitID = dbo.ProjectRoundUser.ProjectRoundUnitID 
	WHERE dbo.ProjectRoundUser.ProjectRoundUserID = @ProjectRoundUserID

END

END

GO
/****** Object:  StoredProcedure [dbo].[cp_MoveUserFree]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[cp_MoveUserFree]
	@Free VARCHAR(64), @ProjectRoundUnitID INT, @ProjectRoundID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @ProjectRoundUserID INT
	SELECT TOP 1 @ProjectRoundUserID = ProjectRoundUserID FROM ProjectRoundUser WHERE ProjectRoundID = @ProjectRoundID AND Email LIKE @Free

IF @ProjectRoundUserID IS NOT NULL BEGIN

	-- get new projectroundid
	DECLARE @NewProjectRoundID INT
	SELECT @NewProjectRoundID = ProjectRoundID FROM ProjectRoundUnit WHERE ProjectRoundUnitID = @ProjectRoundUnitID

	-- decrease usercount on unit where user is on now
	UPDATE dbo.ProjectRoundUnit SET dbo.ProjectRoundUnit.UserCount = dbo.ProjectRoundUnit.UserCount-1
	FROM dbo.ProjectRoundUnit INNER JOIN dbo.ProjectRoundUser ON dbo.ProjectRoundUnit.ProjectRoundUnitID = dbo.ProjectRoundUser.ProjectRoundUnitID 
	WHERE dbo.ProjectRoundUser.ProjectRoundUserID = @ProjectRoundUserID

	-- set old projectroundunitid
	UPDATE ProjectRoundUser SET OldProjectRoundUnitID = ProjectRoundUnitID WHERE ProjectRoundUserID = @ProjectRoundUserID

	-- move user
	UPDATE dbo.ProjectRoundUser SET dbo.ProjectRoundUser.ProjectRoundUnitID = @ProjectRoundUnitID, dbo.ProjectRoundUser.ProjectRoundID = @NewProjectRoundID WHERE dbo.ProjectRoundUser.ProjectRoundUserID = @ProjectRoundUserID

	-- move answer
	UPDATE dbo.Answer SET dbo.Answer.ProjectRoundUnitID = @ProjectRoundUnitID, dbo.Answer.ProjectRoundID = @NewProjectRoundID WHERE dbo.Answer.ProjectRoundUserID = @ProjectRoundUserID

	-- increase usercount on unit where user is on now
	UPDATE dbo.ProjectRoundUnit SET dbo.ProjectRoundUnit.UserCount = dbo.ProjectRoundUnit.UserCount+1
	FROM dbo.ProjectRoundUnit INNER JOIN dbo.ProjectRoundUser ON dbo.ProjectRoundUnit.ProjectRoundUnitID = dbo.ProjectRoundUser.ProjectRoundUnitID 
	WHERE dbo.ProjectRoundUser.ProjectRoundUserID = @ProjectRoundUserID

END

END

GO
/****** Object:  StoredProcedure [dbo].[cp_MoveUserUpWhereGroup]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[cp_MoveUserUpWhereGroup]
	@ProjectRoundID INT, @GroupID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

-- UPDATE dbo.ProjectRoundUnit SET dbo.ProjectRoundUnit.UserCount = dbo.ProjectRoundUnit.UserCount-s.CX
-- FROM dbo.ProjectRoundUnit INNER JOIN (SELECT COUNT(*) AS CX, dbo.ProjectRoundUser.ProjectRoundUnitID FROM dbo.ProjectRoundUser WHERE dbo.ProjectRoundUser.ProjectRoundID = @ProjectRoundID AND dbo.ProjectRoundUser.GroupID = @GroupID GROUP BY dbo.ProjectRoundUser.ProjectRoundUnitID) s 
-- ON dbo.ProjectRoundUnit.ProjectRoundUnitID = s.ProjectRoundUnitID

UPDATE dbo.ProjectRoundUser SET dbo.ProjectRoundUser.ProjectRoundUnitID = ISNULL(dbo.ProjectRoundUnit.ParentProjectRoundUnitID,dbo.ProjectRoundUser.ProjectRoundUnitID)
FROM dbo.ProjectRoundUser INNER JOIN dbo.ProjectRoundUnit ON dbo.ProjectRoundUser.ProjectRoundUnitID = dbo.ProjectRoundUnit.ProjectRoundUnitID
WHERE dbo.ProjectRoundUser.GroupID = @GroupID AND dbo.ProjectRoundUnit.ProjectRoundID = @ProjectRoundID

UPDATE dbo.Answer SET dbo.Answer.ProjectRoundUnitID = dbo.ProjectRoundUser.ProjectRoundUnitID
FROM dbo.ProjectRoundUser INNER JOIN dbo.Answer ON dbo.ProjectRoundUser.ProjectRoundUserID = dbo.Answer.ProjectRoundUserID
WHERE dbo.ProjectRoundUser.GroupID = @GroupID AND dbo.ProjectRoundUser.ProjectRoundID = @ProjectRoundID

-- UPDATE dbo.ProjectRoundUnit SET dbo.ProjectRoundUnit.UserCount = dbo.ProjectRoundUnit.UserCount+s.CX
-- FROM dbo.ProjectRoundUnit INNER JOIN (SELECT COUNT(*) AS CX, dbo.ProjectRoundUser.ProjectRoundUnitID FROM dbo.ProjectRoundUser WHERE dbo.ProjectRoundUser.ProjectRoundID = @ProjectRoundID AND dbo.ProjectRoundUser.GroupID = @GroupID GROUP BY dbo.ProjectRoundUser.ProjectRoundUnitID) s 
-- ON dbo.ProjectRoundUnit.ProjectRoundUnitID = s.ProjectRoundUnitID

END

GO
/****** Object:  StoredProcedure [dbo].[cp_sendmail]    Script Date: 2017-06-20 11:06:34 ******/
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
				EXEC @hr = sp_OASetProperty @iMsg, 'Configuration.fields("http://schemas.microsoft.com/cdo/configuration/smtpserver").Value', '212.112.175.150' 
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
/****** Object:  StoredProcedure [dbo].[cp_unitStats]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[cp_unitStats]
	@ProjectRoundUnitID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @SortString NVARCHAR(MAX)
	select @SortString = sortstring from projectroundunit where projectroundunitid = @ProjectRoundUnitID

	SELECT g.GroupDesc,Count(*),'-','-' FROM ProjectRoundUser u inner join [Group] g on g.GroupID = u.GroupID inner join answer a on u.projectrounduserid = a.projectrounduserid and a.enddt is not null inner join projectroundunit r on u.projectroundunitid = r.projectroundunitid WHERE left(r.sortstring,LEN(@SortString)) = @SortString GROUP BY g.GroupDesc
	union all
	select a.un, a.cx, cast(b.bx as nvarchar(max)), concat(round(cast(a.cx as real)/cast(b.bx as real)*100,0),'%') from (
	SELECT 'TOTAL' as un,Count(*) as cx FROM ProjectRoundUser u inner join answer a on u.projectrounduserid = a.projectrounduserid and a.enddt is not null inner join projectroundunit r on u.projectroundunitid = r.projectroundunitid WHERE left(r.sortstring,LEN(@SortString)) = @SortString
	union all
	SELECT (SELECT Unit FROM ProjectRoundUnit WHERE SortString = left(r.sortstring,LEN(@SortString)+8)) as un, Count(*) as cx FROM ProjectRoundUser u inner join answer a on u.projectrounduserid = a.projectrounduserid and a.enddt is not null inner join projectroundunit r on u.projectroundunitid = r.projectroundunitid WHERE LEN(SortString) >= LEN(@SortString)+8 AND left(r.sortstring,LEN(@SortString)) = @SortString GROUP BY left(r.sortstring,LEN(@SortString)+8)
	) a left outer join (
	SELECT 'TOTAL' as un,SUM(usercount) as bx FROM projectroundunit r WHERE left(r.sortstring,LEN(@SortString)) = @SortString
	UNION ALL 
	SELECT (SELECT Unit FROM ProjectRoundUnit WHERE SortString = left(r.sortstring,LEN(@SortString)+8)) as un, sum(r.usercount) as bx FROM projectroundunit r WHERE LEN(r.SortString) >= LEN(@SortString)+8 AND left(r.sortstring,LEN(@SortString)) = @SortString GROUP BY left(r.sortstring,LEN(@SortString)+8)
	) b ON a.un = b.un

END

GO
/****** Object:  StoredProcedure [dbo].[cp_unitStatsID]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[cp_unitStatsID]
	@ProjectRoundUnitID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @SortString NVARCHAR(MAX)
	select @SortString = sortstring from projectroundunit where projectroundunitid = @ProjectRoundUnitID

	SELECT g.GroupDesc,Count(*),'-','-' FROM ProjectRoundUser u inner join [Group] g on g.GroupID = u.GroupID inner join answer a on u.projectrounduserid = a.projectrounduserid and a.enddt is not null inner join projectroundunit r on u.projectroundunitid = r.projectroundunitid WHERE left(r.sortstring,LEN(@SortString)) = @SortString GROUP BY g.GroupDesc
	union all
	select a.un, a.cx, cast(b.bx as nvarchar(max)), concat(round(cast(a.cx as real)/cast(b.bx as real)*100,0),'%') from (
	SELECT 'TOTAL' as un,Count(*) as cx FROM ProjectRoundUser u inner join answer a on u.projectrounduserid = a.projectrounduserid and a.enddt is not null inner join projectroundunit r on u.projectroundunitid = r.projectroundunitid WHERE left(r.sortstring,LEN(@SortString)) = @SortString
	union all
	SELECT (SELECT CAST(ProjectRoundUnitID AS NVARCHAR(max)) FROM ProjectRoundUnit WHERE SortString = left(r.sortstring,LEN(@SortString)+8)) as un, Count(*) as cx FROM ProjectRoundUser u inner join answer a on u.projectrounduserid = a.projectrounduserid and a.enddt is not null inner join projectroundunit r on u.projectroundunitid = r.projectroundunitid WHERE LEN(SortString) >= LEN(@SortString)+8 AND left(r.sortstring,LEN(@SortString)) = @SortString GROUP BY left(r.sortstring,LEN(@SortString)+8)
	) a left outer join (
	SELECT 'TOTAL' as un,SUM(usercount) as bx FROM projectroundunit r WHERE left(r.sortstring,LEN(@SortString)) = @SortString
	UNION ALL 
	SELECT (SELECT CAST(ProjectRoundUnitID AS NVARCHAR(max)) FROM ProjectRoundUnit WHERE SortString = left(r.sortstring,LEN(@SortString)+8)) as un, sum(r.usercount) as bx FROM projectroundunit r WHERE LEN(r.SortString) >= LEN(@SortString)+8 AND left(r.sortstring,LEN(@SortString)) = @SortString GROUP BY left(r.sortstring,LEN(@SortString)+8)
	) b ON a.un = b.un

END

GO
/****** Object:  StoredProcedure [dbo].[cp_unitSubStats]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[cp_unitSubStats]
	@ProjectRoundUnitID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @SortString NVARCHAR(MAX)
	select @SortString = sortstring from projectroundunit where projectroundunitid = @ProjectRoundUnitID

	SELECT g.GroupDesc,Count(*),'-','-' FROM ProjectRoundUser u inner join [Group] g on g.GroupID = u.GroupID inner join answer a on u.projectrounduserid = a.projectrounduserid and a.enddt is not null inner join projectroundunit r on u.projectroundunitid = r.projectroundunitid WHERE left(r.sortstring,LEN(@SortString)) = @SortString GROUP BY g.GroupDesc
	union all
	select a.un, a.cx, cast(b.bx as nvarchar(max)), concat(round(cast(a.cx as real)/cast(b.bx as real)*100,0),'%') from (
	SELECT 'TOTAL' as un,Count(*) as cx FROM ProjectRoundUser u inner join answer a on u.projectrounduserid = a.projectrounduserid and a.enddt is not null inner join projectroundunit r on u.projectroundunitid = r.projectroundunitid WHERE left(r.sortstring,LEN(@SortString)) = @SortString
	union all
	SELECT (SELECT Unit FROM ProjectRoundUnit WHERE SortString = left(r.sortstring,LEN(@SortString)+16)) as un, Count(*) as cx FROM ProjectRoundUser u inner join answer a on u.projectrounduserid = a.projectrounduserid and a.enddt is not null inner join projectroundunit r on u.projectroundunitid = r.projectroundunitid WHERE LEN(SortString) >= LEN(@SortString)+16 AND left(r.sortstring,LEN(@SortString)) = @SortString GROUP BY left(r.sortstring,LEN(@SortString)+16)
	) a left outer join (
	SELECT 'TOTAL' as un,SUM(usercount) as bx FROM projectroundunit r WHERE left(r.sortstring,LEN(@SortString)) = @SortString
	UNION ALL 
	SELECT (SELECT Unit FROM ProjectRoundUnit WHERE SortString = left(r.sortstring,LEN(@SortString)+16)) as un, sum(r.usercount) as bx FROM projectroundunit r WHERE LEN(r.SortString) >= LEN(@SortString)+16 AND left(r.sortstring,LEN(@SortString)) = @SortString GROUP BY left(r.sortstring,LEN(@SortString)+16)
	) b ON a.un = b.un

END

GO
/****** Object:  StoredProcedure [dbo].[cp_updateOptionComponents_OrderValue]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[cp_updateOptionComponents_OrderValue]
AS
BEGIN
	SET NOCOUNT ON;
	Declare @ocs int, @o int, @so int, @temp real, @reverse int
	SET @ocs = 0
	SET @o = 0
	SET @so = 0
	
	UPDATE OptionComponents SET OrderValue = NULL

	WHILE @ocs IS NOT NULL BEGIN
		SET @ocs = NULL
		SELECT TOP 1 @reverse = o.Reverse, @ocs = ocs.OptionComponentsID, @o = ocs.OptionID, @so = ISNULL(ocs.ExportValue,oc.ExportValue) FROM OptionComponents ocs INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID INNER JOIN [Option] o ON ocs.OptionID = o.OptionID WHERE ocs.OrderValue IS NULL AND ocs.NoOrderValue IS NULL

		IF @ocs IS NOT NULL BEGIN
			SELECT @temp = COUNT(*) FROM OptionComponents WHERE OptionID = @o AND NoOrderValue IS NULL
			IF @temp = 1 BEGIN
				SET @temp = 0
			END
			IF @temp > 1 BEGIN
				SET @temp = 100/(@temp-1)
			END
			IF @reverse = 1 BEGIN
				SELECT @temp = COUNT(*)*@temp FROM OptionComponents ocs INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID WHERE ocs.OptionID = @o AND ISNULL(ocs.ExportValue,oc.ExportValue) > @so AND ocs.NoOrderValue IS NULL
			END
			IF @reverse IS NULL BEGIN
				SELECT @temp = COUNT(*)*@temp FROM OptionComponents ocs INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID WHERE ocs.OptionID = @o AND ISNULL(ocs.ExportValue,oc.ExportValue) < @so AND ocs.NoOrderValue IS NULL
			END
			UPDATE OptionComponents SET OrderValue = @temp WHERE OptionComponentsID = @ocs
		END
	END
END

GO
/****** Object:  UserDefinedFunction [dbo].[cf_filePathTree]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_filePathTree] (@ProjectRoundUnitID INT)
RETURNS VARCHAR(1024) AS  
BEGIN
	DECLARE @Return VARCHAR(1024)
	SELECT @Return = ExternalID, @ProjectRoundUnitID = ParentProjectRoundUnitID FROM [ProjectRoundUnit] WHERE ProjectRoundUnitID = @ProjectRoundUnitID
	WHILE @ProjectRoundUnitID IS NOT NULL BEGIN
		SELECT @Return = ExternalID + '\' + @Return, @ProjectRoundUnitID = ParentProjectRoundUnitID FROM [ProjectRoundUnit] WHERE ProjectRoundUnitID = @ProjectRoundUnitID
	END
	RETURN @Return
END

GO
/****** Object:  UserDefinedFunction [dbo].[cf_isBlank]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_orgTree]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_orgTree] (@ProjectRoundUnitID INT)
RETURNS VARCHAR(1024) AS  
BEGIN
	DECLARE @Return VARCHAR(1024)
	SELECT @Return = ExternalID, @ProjectRoundUnitID = ParentProjectRoundUnitID FROM [ProjectRoundUnit] WHERE ProjectRoundUnitID = @ProjectRoundUnitID
	WHILE @ProjectRoundUnitID IS NOT NULL BEGIN
		SELECT @Return = ExternalID + '-' + @Return, @ProjectRoundUnitID = ParentProjectRoundUnitID FROM [ProjectRoundUnit] WHERE ProjectRoundUnitID = @ProjectRoundUnitID
	END
	RETURN @Return
END

GO
/****** Object:  UserDefinedFunction [dbo].[cf_projectRoundFinishedAnswerCount]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_projectRoundUserCount]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_ProjectUnitTree]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_twoDigit]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitAndChildrenAnswerCount]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitAndChildrenEstUserCount]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitAndChildrenFinishedAnswerCount]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitAndChildrenMaxOfAnswerOrUserCount]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitAndChildrenNonfinishedAnswerCount]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitAndChildrenUserCount]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitAndChildrenUserSendCount]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitDepth]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitExtID]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[cf_unitExtID](@ProjectRoundUnitID INT, @UnitDepth INT, @ExtID NVARCHAR(64))
RETURNS NVARCHAR(64) AS  
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitGreen]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitIndividualReportID]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitLang]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitLangID]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitSortString]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitSurvey]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitSurveyID]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitSurveyIntro]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitSurveyKey]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitTimeframe]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitUserCount]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_unitYellow]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_year2Week]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_year2WeekEven]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_year3Month]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_year6Month]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_yearMonth]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_yearMonthDay]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[cf_yearWeek]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  UserDefinedFunction [dbo].[f_isoweek]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE function [dbo].[f_isoweek](@date datetime)
RETURNS INT
as
BEGIN
IF YEAR(@date) = 2011 AND MONTH(@date) = 1 AND DAY(@date) <= 2 BEGIN
	RETURN 1
END
IF YEAR(@date) = 2012 AND MONTH(@date) = 1 AND DAY(@date) = 1 BEGIN
	RETURN 1
END
IF YEAR(@date) = 2012 AND MONTH(@date) = 12 AND DAY(@date) = 31 BEGIN
	RETURN 52
END
 RETURN (datepart(DY, datediff(d, 0, @date) / 7 * 7 + 3)+6) / 7
-- replaced code for yet another improvement.
-- RETURN (datepart(DY, dateadd(ww, datediff(d, 0, @date) / 7, 3))+6) / 7

END
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AnswerValue]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomReport]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomReportRow]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Debug]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Debug](
	[DebugID] [int] IDENTITY(1,1) NOT NULL,
	[DebugTxt] [nvarchar](max) NULL,
	[DT] [datetime] NULL,
 CONSTRAINT [PK_Debug1] PRIMARY KEY CLUSTERED 
(
	[DebugID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Feedback](
	[FeedbackID] [int] IDENTITY(1,1) NOT NULL,
	[Feedback] [varchar](50) NULL,
	[SurveyID] [int] NULL,
	[Compare] [int] NULL,
	[FeedbackTemplateID] [int] NULL,
	[NoHardcodedIdxs] [int] NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FeedbackQuestion]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedbackQuestion](
	[FeedbackQuestionID] [int] IDENTITY(1,1) NOT NULL,
	[FeedbackID] [int] NULL,
	[QuestionID] [int] NULL,
	[Additional] [int] NULL,
	[FeedbackTemplatePageID] [int] NULL,
	[IdxID] [int] NULL,
	[HardcodedIdx] [int] NULL,
	[OptionID] [int] NULL,
	[PartOfChart] [int] NULL,
 CONSTRAINT [PK_FeedbackQuestion] PRIMARY KEY CLUSTERED 
(
	[FeedbackQuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FeedbackRun]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedbackRun](
	[FeedbackRunID] [int] IDENTITY(1,1) NOT NULL,
	[FeedbackRunKey] [uniqueidentifier] NULL,
	[FeedbackID] [int] NULL,
	[Total] [int] NULL,
	[Answer] [int] NULL,
 CONSTRAINT [PK_FeedbackRun] PRIMARY KEY CLUSTERED 
(
	[FeedbackRunID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FeedbackRunRow]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FeedbackRunRow](
	[FeedbackRunRowID] [int] IDENTITY(1,1) NOT NULL,
	[FeedbackRunID] [int] NULL,
	[URL1] [text] NULL,
	[Area1] [varchar](256) NULL,
	[Header1] [varchar](256) NULL,
	[Description1] [text] NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[FeedbackQuestionID] [int] NULL,
	[URL] [nvarchar](max) NULL,
	[Area] [nvarchar](max) NULL,
	[Header] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_FeedbackRunRow] PRIMARY KEY CLUSTERED 
(
	[FeedbackRunRowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FeedbackTemplate]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedbackTemplate](
	[FeedbackTemplateID] [int] IDENTITY(1,1) NOT NULL,
	[FeedbackTemplate] [nvarchar](50) NULL,
	[OrgPH] [nvarchar](50) NULL,
	[DeptPH] [nvarchar](50) NULL,
	[DatePH] [nvarchar](50) NULL,
	[Slide] [nvarchar](50) NULL,
	[DefaultSlide] [nvarchar](50) NULL,
	[DefaultHeaderPH] [nvarchar](50) NULL,
	[DefaultBottomPH] [nvarchar](50) NULL,
	[BG] [nvarchar](50) NULL,
	[DefaultImgPos] [int] NULL,
	[CountSlide] [nvarchar](50) NULL,
	[CountPH] [nvarchar](50) NULL,
	[CountTxt] [nvarchar](max) NULL,
	[NoFontScale] [int] NULL,
 CONSTRAINT [PK_FeedbackTemplate] PRIMARY KEY CLUSTERED 
(
	[FeedbackTemplateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FeedbackTemplatePage]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedbackTemplatePage](
	[FeedbackTemplatePageID] [int] IDENTITY(1,1) NOT NULL,
	[FeedbackTemplateID] [int] NULL,
	[Slide] [nvarchar](50) NULL,
	[HeaderPH] [nvarchar](50) NULL,
	[BottomPH] [nvarchar](50) NULL,
	[ImgPos] [int] NULL,
	[Description] [nvarchar](50) NULL,
	[DoubleImg] [int] NULL,
 CONSTRAINT [PK_FeedbackTemplatePage] PRIMARY KEY CLUSTERED 
(
	[FeedbackTemplatePageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Group]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Idx]    Script Date: 2017-06-20 11:06:34 ******/
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
	[Description] [text] NULL,
 CONSTRAINT [PK_Idx] PRIMARY KEY CLUSTERED 
(
	[IdxID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IdxLang]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IdxPart]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdxPartComponent]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Lang]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MailQueue]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MailSubmission]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailSubmission](
	[MailSubmissionID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Variable] [nvarchar](max) NULL,
	[Processed] [smalldatetime] NULL,
 CONSTRAINT [PK_MailSubmission] PRIMARY KEY CLUSTERED 
(
	[MailSubmissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Manager]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ManagerProjectRound]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ManagerProjectRoundUnit]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Nav]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Option]    Script Date: 2017-06-20 11:06:34 ******/
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
	[Reverse] [int] NULL,
 CONSTRAINT [PK_Option] PRIMARY KEY CLUSTERED 
(
	[OptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OptionComponent]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OptionComponentContainer]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OptionComponentLang]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OptionComponents]    Script Date: 2017-06-20 11:06:34 ******/
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
	[OrderValue] [int] NULL,
	[NoOrderValue] [int] NULL,
 CONSTRAINT [PK_OptionPart] PRIMARY KEY CLUSTERED 
(
	[OptionComponentsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OptionContainer]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PlotType]    Script Date: 2017-06-20 11:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlotType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PlotTypeLang]    Script Date: 2017-06-20 11:06:34 ******/
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
	[ShortName] [nvarchar](255) NULL,
	[SupportsMultipleSeries] [int] NULL,
 CONSTRAINT [PK_PlotTypeLanguage] PRIMARY KEY CLUSTERED 
(
	[PlotTypeLangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Project]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectRound]    Script Date: 2017-06-20 11:06:34 ******/
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
	[AdHocReportCompareWithParent] [int] NULL,
	[FeedbackID] [int] NULL,
	[SOAPURL] [nvarchar](max) NULL,
	[SOAPauth] [nvarchar](64) NULL,
	[SOAPauthMessage] [nvarchar](max) NULL,
	[SOAPauthToken] [nvarchar](64) NULL,
	[SOAPdata] [nvarchar](64) NULL,
	[SOAPdataMessage] [nvarchar](max) NULL,
 CONSTRAINT [PK_ProjectRound] PRIMARY KEY CLUSTERED 
(
	[ProjectRoundID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectRoundLang]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectRoundQO]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectRoundUnit]    Script Date: 2017-06-20 11:06:34 ******/
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
	[OldID] [varchar](16) NULL,
	[ExternalID] [nvarchar](64) NULL,
	[IDold] [nvarchar](32) NULL,
	[ID] [nvarchar](64) NULL,
	[SortStringLength]  AS (len([SortString])),
 CONSTRAINT [PK_ProjectUnit] PRIMARY KEY CLUSTERED 
(
	[ProjectRoundUnitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectRoundUnitManager]    Script Date: 2017-06-20 11:06:34 ******/
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
/****** Object:  Table [dbo].[ProjectRoundUser]    Script Date: 2017-06-20 11:06:34 ******/
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
	[OldProjectRoundUnitID] [int] NULL,
	[OldGroupID] [int] NULL,
 CONSTRAINT [PK_ProjectRoundUser] PRIMARY KEY CLUSTERED 
(
	[ProjectRoundUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectRoundUserCompare]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectRoundUserQO]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectSurvey]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectUnitCategory]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectUserCategory]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Question]    Script Date: 2017-06-20 11:06:34 ******/
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
	[LimitWidth] [int] NULL,
	[FillRemainderWithBgColor] [varchar](8) NULL,
	[Niner] [int] NULL,
	[Grey] [int] NULL,
	[LeftIsVas] [int] NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuestionCategory]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuestionCategoryLang]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuestionCategoryQuestion]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuestionContainer]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuestionLang]    Script Date: 2017-06-20 11:06:34 ******/
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
	[ReportQuestion] [nvarchar](max) NULL,
 CONSTRAINT [PK_QuestionLang] PRIMARY KEY CLUSTERED 
(
	[QuestionLangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuestionOption]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuestionOptionRange]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Report]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReportLang]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReportPart]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReportPartComponent]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReportPartLang]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sponsor]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorAdmin]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorAutoPRU]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorLang]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorPRU]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorReminder]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SponsorSuperAdmin]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorSuperAdminSponsor]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SponsorUserCheck]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Survey]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SurveyLang]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SurveyQuestion]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SurveyQuestionIf]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SurveyQuestionLang]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SurveyQuestionOption]    Script Date: 2017-06-20 11:06:34 ******/
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
	[Width] [int] NULL,
 CONSTRAINT [PK_SurveyQuestionOption] PRIMARY KEY CLUSTERED 
(
	[SurveyQuestionOptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SurveyQuestionOptionComponent]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SurveyQuestionOptionComponentLang]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TempReport]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TempReportComponent]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TempReportComponentAnswer]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Unit]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UnitCategory]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UnitCategoryLang]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserCategory]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserCategoryLang]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserNote]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserProjectRoundUser]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserSchedule]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WeightedQuestionOption]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WeightedQuestionOptionLang]    Script Date: 2017-06-20 11:06:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Answer] ADD  CONSTRAINT [DF_Answer_StartDT]  DEFAULT (getdate()) FOR [StartDT]
GO
ALTER TABLE [dbo].[Answer] ADD  CONSTRAINT [DF_Answer_AnswerKey]  DEFAULT (newid()) FOR [AnswerKey]
GO
ALTER TABLE [dbo].[AnswerValue] ADD  CONSTRAINT [DF_AnswerValue_CreatedDateTime]  DEFAULT (getdate()) FOR [CreatedDateTime]
GO
ALTER TABLE [dbo].[CustomReport] ADD  CONSTRAINT [DF_CustomReport_DT]  DEFAULT (getdate()) FOR [DT]
GO
ALTER TABLE [dbo].[Debug] ADD  CONSTRAINT [DF_Debug_DT1]  DEFAULT (getdate()) FOR [DT]
GO
ALTER TABLE [dbo].[FeedbackRun] ADD  CONSTRAINT [DF_FeedbackRun_FeedbackRunKey]  DEFAULT (newid()) FOR [FeedbackRunKey]
GO
ALTER TABLE [dbo].[Idx] ADD  CONSTRAINT [DF_Idx_AllPartsRequired]  DEFAULT (0) FOR [AllPartsRequired]
GO
ALTER TABLE [dbo].[Idx] ADD  CONSTRAINT [DF_Idx_MaxVal]  DEFAULT (0) FOR [MaxVal]
GO
ALTER TABLE [dbo].[ManagerProjectRound] ADD  CONSTRAINT [DF_ManagerProjectRound_MPRK]  DEFAULT (newid()) FOR [MPRK]
GO
ALTER TABLE [dbo].[ProjectRound] ADD  CONSTRAINT [DF_ProjectRound_RoundKey]  DEFAULT (newid()) FOR [RoundKey]
GO
ALTER TABLE [dbo].[ProjectRoundUnit] ADD  CONSTRAINT [DF_ProjectRoundUnit_UnitKey]  DEFAULT (newid()) FOR [UnitKey]
GO
ALTER TABLE [dbo].[ProjectRoundUnit] ADD  CONSTRAINT [DF_ProjectRoundUnit_CanHaveUsers]  DEFAULT (1) FOR [CanHaveUsers]
GO
ALTER TABLE [dbo].[ProjectRoundUser] ADD  CONSTRAINT [DF_ProjectRoundUser_UserKey]  DEFAULT (newid()) FOR [UserKey]
GO
ALTER TABLE [dbo].[ProjectRoundUser] ADD  CONSTRAINT [DF_ProjectRoundUser_SendCount]  DEFAULT (0) FOR [SendCount]
GO
ALTER TABLE [dbo].[ProjectRoundUser] ADD  CONSTRAINT [DF_ProjectRoundUser_ReminderCount]  DEFAULT (0) FOR [ReminderCount]
GO
ALTER TABLE [dbo].[ProjectRoundUser] ADD  CONSTRAINT [DF_ProjectRoundUser_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[Question] ADD  CONSTRAINT [DF_Question_Box]  DEFAULT (0) FOR [Box]
GO
ALTER TABLE [dbo].[Report] ADD  CONSTRAINT [DF_Report_ReportKey]  DEFAULT (newid()) FOR [ReportKey]
GO
ALTER TABLE [dbo].[Survey] ADD  CONSTRAINT [DF_Survey_SurveyKey]  DEFAULT (newid()) FOR [SurveyKey]
GO

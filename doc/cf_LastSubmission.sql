USE [healthWatch]
GO

/****** Object:  UserDefinedFunction [dbo].[cf_lastSubmission]    Script Date: 2015-05-07 13:38:35 ******/
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


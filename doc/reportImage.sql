declare @reportPartID integer;
declare @langID integer;
declare @idx integer;
declare @sortString varchar(255);
declare @fy integer;
declare @ty integer;
declare @sponsorAdminID integer;
declare @sponsorID integer;

set @reportPartID = 14;
set @langID = 1;
set @idx = 14;
set @sortString = '00000093';
set @fy = 2012;
set @ty = 2013;
set @sponsorID = 101;
set @sponsorAdminID = 658;

use eForm;

SELECT rp.Type,
	(SELECT COUNT(*) FROM ReportPartComponent rpc WHERE rpc.ReportPartID = rp.ReportPartID),
	rp.QuestionID,
	rp.OptionID,
	rp.RequiredAnswerCount,
	rp.PartLevel,
	rp.ReportPartID
FROM ReportPart rp
WHERE rp.ReportPartID = @reportPartID;

SELECT rpc.WeightedQuestionOptionID,
	wqo.YellowLow,
	wqo.GreenLow,
	wqo.GreenHigh,
	wqo.YellowHigh,
	wqo.QuestionID,
	wqo.OptionID
FROM    ReportPartComponent rpc
INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID
WHERE rpc.ReportPartID = @reportPartID
ORDER BY rpc.SortOrder;

SELECT rpc.WeightedQuestionOptionID,
	wqol.WeightedQuestionOption,
	wqo.QuestionID,
	wqo.OptionID
FROM ReportPartComponent rpc
INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID
INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID
	AND wqol.LangID = @langID
WHERE rpc.ReportPartID = @reportPartID
ORDER BY rpc.SortOrder;

SELECT AVG(tmp.AX),
	tmp.Idx,
	tmp.IdxID,
	COUNT(*) AS DX
FROM (
	SELECT 100 * CAST(SUM(ipc.Val * ip.Multiple) AS REAL) / i.MaxVal
		AS AX,
		i.IdxID,
		il.Idx,
		i.CX,
		i.AllPartsRequired,
		COUNT(*) AS BX,
		pru.SortString
	FROM Idx i
	INNER JOIN IdxLang il ON i.IdxID = il.IdxID AND il.LangID = @langID
	INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID
	INNER JOIN IdxPartComponent ipc ON ip.IdxPartID = ipc.IdxPartID
	INNER JOIN AnswerValue av ON ip.QuestionID = av.QuestionID
		AND ip.OptionID = av.OptionID
		AND av.ValueInt = ipc.OptionComponentID
	INNER JOIN Answer a ON av.AnswerID = a.AnswerID
	INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
	WHERE a.EndDT IS NOT NULL
		AND i.IdxID = @idx
		AND LEFT(pru.SortString, LEN(@sortString)) = @sortString
		AND YEAR(a.EndDT) >= @fy
		AND YEAR(a.EndDT) <= @ty
	GROUP BY i.IdxID,
		a.AnswerID,
		i.MaxVal,
		il.Idx,
		i.CX,
		i.AllPartsRequired,
		pru.SortString
) tmp
WHERE tmp.AllPartsRequired = 0 OR tmp.CX = tmp.BX
GROUP BY tmp.IdxID, tmp.Idx;

SELECT tmp.DT,
	AVG(tmp.V),
	COUNT(tmp.V),
	STDEV(tmp.V)
FROM (
	SELECT dbo.cf_year2WeekEven(a.EndDT) AS DT, AVG(av.ValueInt) AS V
	FROM Answer a
	INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
	INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = 911
	INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
	INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, 8) = '00000039'
	INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID
		AND av.QuestionID = 238
		AND av.OptionID = 55
	WHERE a.EndDT IS NOT NULL
	AND YEAR(a.EndDT) >= 2012
	AND YEAR(a.EndDT) <= 2013
	GROUP BY a.ProjectRoundUserID, dbo.cf_year2WeekEven(a.EndDT)
) tmp
GROUP BY tmp.DT
ORDER BY tmp.DT;

use healthWatch;

SELECT d.DepartmentID,
	d.SortString
FROM Department d
INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID
WHERE sad.SponsorAdminID = @sponsorAdminID AND d.SponsorID = @sponsorID
ORDER BY d.SortString;

SELECT d.Department,
	d.DepartmentID,
	d.SortString
FROM Department d
INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID
WHERE sad.SponsorAdminID = 5 
AND d.SponsorID = 4
AND (d.DepartmentID IN (0))
ORDER BY d.SortString;
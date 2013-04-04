declare @sponsorID integer;
declare @langID integer;
declare @projectRoundUnitID integer;

set @sponsorID = 2;
set @langID = 1;
set @projectRoundUnitID = 506;

use healthWatch;

SELECT sprul.LangID,
	spru.ProjectRoundUnitID,
	l.LID,
	l.Language
FROM SponsorProjectRoundUnit spru
LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID
INNER JOIN LID l ON ISNULL(sprul.LangID, 1) = l.LID
WHERE spru.SponsorID = @sponsorID
ORDER BY spru.SortOrder, spru.SponsorProjectRoundUnitID, l.LID;

SELECT ISNULL(sprul.Nav, '?'),
	spru.ProjectRoundUnitID
FROM SponsorProjectRoundUnit spru
LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID
WHERE spru.SponsorID = @sponsorID
AND ISNULL(sprul.LangID, 1) = @langID;

SELECT sbq.BQID,
	BQ.Internal
FROM SponsorBQ sbq
INNER JOIN BQ ON BQ.BQID = sbq.BQID
WHERE (BQ.Comparison = 1 OR sbq.Hidden = 1)
AND BQ.Type IN (1, 7)
AND sbq.SponsorID = @sponsorID;

SELECT d.Department,
	d.DepartmentID,
	d.DepartmentShort,
	dbo.cf_departmentDepth(d.DepartmentID),
	(
		SELECT COUNT(*) FROM Department x
		INNER JOIN SponsorAdminDepartment xx ON x.DepartmentID = xx.DepartmentID AND xx.SponsorAdminID = 0
		WHERE (x.ParentDepartmentID = d.ParentDepartmentID
			OR x.ParentDepartmentID IS NULL
			AND d.ParentDepartmentID IS NULL)
			AND d.SponsorID = x.SponsorID
			AND d.SortString < x.SortString
	),
	sad.SponsorAdminID,
	d.SponsorID
FROM Department d
INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID
WHERE sad.SponsorAdminID = 0 AND d.SponsorID = @sponsorID
ORDER BY d.SortString;

use eForm;

SELECT rp.ReportPartID,
	rpl.Subject,
	rpl.Header,
	rpl.Footer,
	rp.Type,
	pru.ProjectRoundUnitID
FROM ProjectRoundUnit pru
INNER JOIN Report r ON r.ReportID = pru.ReportID
INNER JOIN ReportPart rp ON r.ReportID = rp.ReportID
INNER JOIN ReportPartLang rpl ON rp.ReportPartID = rpl.ReportPartID AND rpl.LangID = @langID
WHERE pru.ProjectRoundUnitID = @projectRoundUnitID
ORDER BY rp.SortOrder;
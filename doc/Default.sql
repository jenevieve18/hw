declare @ANV varchar(255);
declare @LOS varchar(255);

set @ANV = 'Usr2';
set @LOS = 'Pas2';

SELECT s.SponsorID,
	sa.SponsorAdminID, 
	s.Sponsor,
	sa.Anonymized, 
	sa.SeeUsers, 
	NULL,
	sa.ReadOnly, 
	ISNULL(sa.Name,sa.Usr) 
FROM Sponsor s
INNER JOIN SponsorAdmin sa ON sa.SponsorID = s.SponsorID WHERE sa.Usr = @ANV AND sa.Pas = @LOS

UNION ALL
SELECT NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	sa.SuperAdminID,
	NULL,
	sa.Username
FROM SuperAdmin sa
WHERE sa.Username = @ANV
AND sa.Password = @LOS;
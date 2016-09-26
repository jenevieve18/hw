using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlMeasureRepository : BaseSqlRepository<Measure>
	{
		public SqlMeasureRepository()
		{
		}
		
		public UserMeasure ReadByGroup(string groupBy, int yearFrom, int yearTo, string sortString, int monthFrom, int monthTo)
		{
			string query = string.Format(
                @"
SELECT {0}(MAX(a.DT)) - {0}(MIN(a.DT)),
	{0}(MIN(a.DT)),
	{0}(MAX(a.DT))
FROM healthwatch..UserMeasure a
WHERE a.DT IS NOT NULL
AND (YEAR(a.DT) = {1} AND MONTH(a.DT) >= {3} OR YEAR(a.DT) > {1})
AND (YEAR(a.DT) = {2} AND MONTH(a.DT) <= {4} OR YEAR(a.DT) < {2})",
				groupBy,
                yearFrom,
				yearTo,
                monthFrom,
                monthTo
			);
			using (SqlDataReader rs = ExecuteReader(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var a = new UserMeasure();
					a.DummyValue1 = GetInt32(rs, 0, 0);
					a.DummyValue2 = GetInt32(rs, 1, 0);
					a.DummyValue3 = GetInt32(rs, 2, 0);
					return a;
				}
			}
			return null;
		}
		
		public Answer ReadMinMax(string groupBy, int yearFrom, int yearTo, int monthFrom, int monthTo, int aggregation, string departmentIDs, int sponsorID)
		{
			string query = string.Format(
				@"
SELECT MAX(tmp2.VA + tmp2.SD), MIN(tmp2.VA - tmp2.SD)
FROM (
	SELECT AVG(tmp.VA) AS VA, ISNULL(STDEV(tmp.VA), 0) AS SD
	FROM (
		SELECT {0}(um.DT) AS DT, SUM(umc.ValDec) AS V, SUM(umc.ValDec) / {5} AS VA, um.UserID
		FROM healthwatch..UserMeasure um
		INNER JOIN healthwatch..UserMeasureComponent umc ON umc.UserMeasureID = um.UserMeasureID
		INNER JOIN healthwatch..MeasureComponent mc ON mc.MeasureComponentID = umc.MeasureComponentID AND mc.MeasureID = 65
		INNER JOIN healthwatch..UserSponsorProject usp ON usp.UserID = u.UserID AND usp.ConsentDT IS NOT NULL
		INNER JOIN healthwatch..UserProfile up ON up.UserProfileID = um.UserProfileID
		INNER JOIN healthWatch..Department d ON d.DepartmentID = up.DepartmentID AND d.DepartmentID IN ({6})
		INNER JOIN healthwatch..Sponsor s ON s.SponsorID = up.SponsorID AND s.SponsorID = {7}
		INNER JOIN healthwatch..SponsorProject sp ON sp.SponsorID = s.SponsorID AND sp.SponsorProjectID = usp.SponsorProjectID
		WHERE um.DT IS NOT NULL
		AND (YEAR(um.DT) = {1} AND MONTH(um.DT) >= {3} OR YEAR(um.DT) > {1})
		AND (YEAR(um.DT) = {2} AND MONTH(um.DT) <= {4} OR YEAR(um.DT) < {2})
		AND um.DT >= sp.StartDT AND um.DT <= sp.EndDT
		--AND (YEAR(sp.StartDT) = {1} AND MONTH(sp.StartDT) >= {3} OR YEAR(sp.StartDT) > {1})
		--AND (YEAR(sp.EndDT) = {2} AND MONTH(sp.EndDT) <= {4} OR YEAR(sp.EndDT) <= {2})
		GROUP BY {0}(um.DT), um.UserID
	) tmp
	GROUP BY tmp.DT
) tmp2",
				groupBy,
				yearFrom,
				yearTo,
				monthFrom,
				monthTo,
				aggregation,
				departmentIDs,
				sponsorID
			);
			using (SqlDataReader rs = ExecuteReader(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var a = new Answer {
						Max = (float)GetDouble(rs, 0, 100),
						Min = 0
					};
					return a;
				}
			}
			return new Answer();
		}
		
		public Answer ReadMinMax2(string groupBy, int yearFrom, int yearTo, int monthFrom, int monthTo, int aggregation, string departmentIDs, int sponsorID, string join)
		{
			string query = string.Format(
				@"
SELECT MAX(tmp2.VA + tmp2.SD), MIN(tmp2.VA - tmp2.SD)
FROM (
	SELECT AVG(tmp.VA) AS VA, ISNULL(STDEV(tmp.VA), 0) AS SD
	FROM (
		SELECT {0}(um.DT) AS DT, SUM(umc.ValDec) AS V, SUM(umc.ValDec) / {5} AS VA, um.UserID
		FROM healthwatch..UserMeasure um
		INNER JOIN healthwatch..UserMeasureComponent umc ON umc.UserMeasureID = um.UserMeasureID
		INNER JOIN healthwatch..MeasureComponent mc ON mc.MeasureComponentID = umc.MeasureComponentID AND mc.MeasureID = 65
		{8}
		INNER JOIN healthwatch..Sponsor s ON s.SponsorID = up.SponsorID AND s.SponsorID = {7}
		INNER JOIN healthwatch..SponsorProject sp ON sp.SponsorID = s.SponsorID AND sp.SponsorProjectID = usp.SponsorProjectID
		WHERE um.DT IS NOT NULL
		AND (YEAR(um.DT) = {1} AND MONTH(um.DT) >= {3} OR YEAR(um.DT) > {1})
		AND (YEAR(um.DT) = {2} AND MONTH(um.DT) <= {4} OR YEAR(um.DT) < {2})
		AND um.DT >= sp.StartDT AND um.DT <= sp.EndDT
		--AND (YEAR(sp.StartDT) = {1} AND MONTH(sp.StartDT) >= {3} OR YEAR(sp.StartDT) > {1})
		--AND (YEAR(sp.EndDT) = {2} AND MONTH(sp.EndDT) <= {4} OR YEAR(sp.EndDT) <= {2})
		GROUP BY {0}(um.DT), um.UserID
	) tmp
	GROUP BY tmp.DT
) tmp2",
				groupBy,
				yearFrom,
				yearTo,
				monthFrom,
				monthTo,
				aggregation,
				departmentIDs,
				sponsorID,
				join
			);
			using (SqlDataReader rs = ExecuteReader(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var a = new Answer {
						Max = (float)GetDouble(rs, 0, 100),
						Min = 0
					};
					return a;
				}
			}
			return new Answer();
		}
		
//		public SponsorProject ReadSponsorProject(int sponsorProjectID)
//		{
//			string query = string.Format(
//				@"
//SELECT SponsorProjectID,
//	SponsorID,
//	StartDT,
//	EndDT,
//	ProjectName
//FROM SponsorProject
//WHERE SponsorProjectID = @SponsorProjectID"
//			);
//			SponsorProject p = null;
//			using (SqlDataReader rs = ExecuteReader(query, "SqlConnection", new SqlParameter("@SponsorProjectID", sponsorProjectID))) {
//				if (rs.Read()) {
//					p = new SponsorProject {
//						Id = GetInt32(rs, 0),
//						Sponsor = new Sponsor { Id = GetInt32(rs, 1) },
//						StartDate = GetDateTime(rs, 2),
//						EndDate = GetDateTime(rs, 3),
//						Subject = GetString(rs, 4),
//						
//						ReportPart = new ReportPart()
//					};
//				}
//			}
//			return p;
//		}
		
//		public IList<SponsorProject> FindSponsorProjects(int sponsorID)
//		{
//			string query = string.Format(
//				@"
//SELECT SponsorProjectID,
//	SponsorID,
//	StartDT,
//	EndDT,
//	ProjectName
//FROM SponsorProject
//WHERE SponsorID = @SponsorID"
//			);
//			var projects = new List<SponsorProject>();
//			using (SqlDataReader rs = ExecuteReader(query, "SqlConnection", new SqlParameter("@SponsorID", sponsorID))) {
//				while (rs.Read()) {
//					var p = new SponsorProject {
//						Id = GetInt32(rs, 0),
//						Sponsor = new Sponsor { Id = GetInt32(rs, 1) },
//						StartDate = GetDateTime(rs, 2),
//						EndDate = GetDateTime(rs, 3),
//						Subject = GetString(rs, 4)
//					};
//					projects.Add(p);
//				}
//			}
//			return projects;
//		}
		
		public IList<SponsorProjectMeasure> FindMeasures(int sponsorProjectID)
		{
			string query = string.Format(
				@"
SELECT SponsorProjectMeasureID,
	SponsorProjectID,
	MeasureID
FROM SponsorProjectMeasure"
			);
			var measures = new List<SponsorProjectMeasure>();
			using (SqlDataReader rs = ExecuteReader(query)) {
				while (rs.Read()) {
					var m = new SponsorProjectMeasure {
						Id = GetInt32(rs, 0),
						SponsorProject = new SponsorProject { Id = GetInt32(rs, 1) },
						Measure = new Measure { Id = GetInt32(rs, 2) }
					};
					measures.Add(m);
				}
			}
			return measures;
		}
		
		public IList<UserMeasure> FindByQuestionAndOptionJoinedAndGrouped2(string join, string groupBy, int yearFrom, int yearTo, int monthFrom, int monthTo, int aggregation, int sponsorID)
		{
			string query = string.Format(
				@"
SELECT {1}(um.DT) AS DT, SUM(umc.ValDec) AS V, SUM(umc.ValDec) / {6} AS AV, um.UserID
FROM healthwatch..UserMeasureComponent umc
INNER JOIN healthwatch..UserMeasure um ON um.UserMeasureID = umc.UserMeasureID
INNER JOIN healthwatch..MeasureComponent mc ON mc.MeasureComponentID = umc.MeasureComponentID AND mc.MeasureID = 65
{0}
INNER JOIN healthwatch..Sponsor s ON s.SponsorID = up.SponsorID AND s.SponsorID = {7}
INNER JOIN healthwatch..SponsorProject sp ON sp.SponsorID = s.SponsorID AND sp.SponsorProjectID = usp.SponsorProjectID
WHERE um.DT IS NOT NULL
AND (YEAR(um.DT) = {2} AND MONTH(um.DT) >= {4} OR YEAR(um.DT) > {2})
AND (YEAR(um.DT) = {3} AND MONTH(um.DT) <= {5} OR YEAR(um.DT) < {3})
AND um.DT >= sp.StartDT AND um.DT <= sp.EndDT
--AND (YEAR(sp.StartDT) = {2} AND MONTH(sp.StartDT) >= {4} OR YEAR(sp.StartDT) > {2})
--AND (YEAR(sp.EndDT) = {3} AND MONTH(sp.EndDT) <= {5} OR YEAR(sp.EndDT) <= {3})
GROUP BY {1}(um.DT), um.UserID
ORDER BY {1}(um.DT)
--ORDER BY um.DT",
				join,
				groupBy,
				yearFrom,
				yearTo,
				monthFrom,
				monthTo,
				aggregation,
				sponsorID
			);
			var measures = new List<UserMeasure>();
			using (SqlDataReader rs = ExecuteReader(query, "eFormSqlconnection")) {
				if (rs.Read()) {
					bool done = false;
					while (!done) {
						var m = new UserMeasure { };
						do {
							m.DT = GetInt32(rs, 0);
//							m.Components.Add(new UserMeasureComponent { ValueInt = (int)rs.GetDecimal(2) });
							m.Values.Add(new UserMeasureComponent { ValueInt = (int)rs.GetDecimal(2) });
							done = !rs.Read();
						} while (!done && GetInt32(rs, 0) == m.DT);
						measures.Add(m);
					}
				}
			}
			return measures;
		}
	}
}

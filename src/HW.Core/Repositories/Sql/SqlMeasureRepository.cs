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
		
		public SponsorProject ReadSponsorProject(int sponsorProjectID)
		{
			string query = string.Format(
				@"
SELECT SponsorProjectID,
	SponsorID,
	StartDT,
	EndDT,
	ProjectName
FROM SponsorProject
WHERE SponsorProjectID = @SponsorProjectID"
			);
			SponsorProject p = null;
			using (SqlDataReader rs = ExecuteReader(query, "SqlConnection", new SqlParameter("@SponsorProjectID", sponsorProjectID))) {
				if (rs.Read()) {
					p = new SponsorProject {
						Id = GetInt32(rs, 0),
						Sponsor = new Sponsor { Id = GetInt32(rs, 1) },
						StartDate = GetDateTime(rs, 2),
						EndDate = GetDateTime(rs, 3),
//						ProjectName = GetString(rs, 4)
						Subject = GetString(rs, 4)
					};
				}
			}
			return p;
		}
		
		public IList<SponsorProject> FindSponsorProjects(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT SponsorProjectID,
	SponsorID,
	StartDT,
	EndDT,
	ProjectName
FROM SponsorProject
WHERE SponsorID = @SponsorID"
			);
			var projects = new List<SponsorProject>();
			using (SqlDataReader rs = ExecuteReader(query, "SqlConnection", new SqlParameter("@SponsorID", sponsorID))) {
				while (rs.Read()) {
					var p = new SponsorProject {
						Id = GetInt32(rs, 0),
						Sponsor = new Sponsor { Id = GetInt32(rs, 1) },
						StartDate = GetDateTime(rs, 2),
						EndDate = GetDateTime(rs, 3),
//						ProjectName = GetString(rs, 4)
						Subject = GetString(rs, 4)
					};
					projects.Add(p);
				}
			}
			return projects;
		}
		
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
		
		public IList<Answer> FindByQuestionAndOptionJoinedAndGrouped2(string join, string groupBy, int yearFrom, int yearTo, int monthFrom, int monthTo)
		{
			string query = string.Format(
				@"
SELECT {1}(a.EndDT) AS DT, AVG(av.ValueInt) AS V
FROM Answer a
{0}
INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = {2} AND av.OptionID = {3}
WHERE a.EndDT IS NOT NULL
AND (YEAR(a.EndDT) = {4} AND MONTH(a.EndDT) >= {6} OR YEAR(a.EndDT) > {4})
AND (YEAR(a.EndDT) = {5} AND MONTH(a.EndDT) <= {7} OR YEAR(a.EndDT) < {5})
GROUP BY a.ProjectRoundUserID, {1}(a.EndDT)",
				join,
				groupBy,
				238,
				55,
				yearFrom,
				yearTo,
				monthFrom,
				monthTo
			);
			var measures = new List<Answer>();
			using (SqlDataReader rs = ExecuteReader(query, "eFormSqlconnection")) {
				if (rs.Read()) {
					bool done = false;
					while (!done) {
						var m = new Answer { };
						do {
							m.DT = rs.GetInt32(0);
							m.Values.Add(new AnswerValue { ValueInt = rs.GetInt32(1) });
							done = !rs.Read();
						} while (!done && rs.GetInt32(0) == m.DT);
						measures.Add(m);
					}
				}
			}
			return measures;
		}
	}
}

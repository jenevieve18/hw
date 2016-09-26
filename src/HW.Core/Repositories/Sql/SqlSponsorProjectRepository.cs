// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlSponsorProjectRepository : BaseSqlRepository<SponsorProject>
	{
		public SqlSponsorProjectRepository()
		{
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
			using (var rs = ExecuteReader(query, "SqlConnection", new SqlParameter("@SponsorID", sponsorID))) {
				while (rs.Read()) {
					var p = new SponsorProject {
						Id = GetInt32(rs, 0),
						Sponsor = new Sponsor { Id = GetInt32(rs, 1) },
						StartDate = GetDateTime(rs, 2),
						EndDate = GetDateTime(rs, 3),
						Subject = GetString(rs, 4)
					};
					projects.Add(p);
				}
			}
			return projects;
		}
		
		public override SponsorProject Read(int sponsorProjectID)
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
			using (var rs = ExecuteReader(query, "SqlConnection", new SqlParameter("@SponsorProjectID", sponsorProjectID))) {
				if (rs.Read()) {
					p = new SponsorProject {
						Id = GetInt32(rs, 0),
						Sponsor = new Sponsor { Id = GetInt32(rs, 1) },
						StartDate = GetDateTime(rs, 2),
						EndDate = GetDateTime(rs, 3),
						Subject = GetString(rs, 4),
						
						ReportPart = new ReportPart()
					};
				}
			}
			return p;
		}
	}
}

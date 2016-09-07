using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorProjectRepository : BaseSqlRepository<SponsorProject>
	{
		public SqlSponsorProjectRepository()
		{
		}
		
		public override void Save(SponsorProject sponsorProject)
		{
			string query = @"
INSERT INTO SponsorProject(
	SponsorProjectID, 
	SponsorID, 
	StartDT, 
	EndDT, 
	ProjectName
)
VALUES(
	@SponsorProjectID, 
	@SponsorID, 
	@StartDT, 
	@EndDT, 
	@ProjectName
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorProjectID", sponsorProject.SponsorProjectID),
				new SqlParameter("@SponsorID", sponsorProject.SponsorID),
				new SqlParameter("@StartDT", sponsorProject.StartDT),
				new SqlParameter("@EndDT", sponsorProject.EndDT),
				new SqlParameter("@ProjectName", sponsorProject.ProjectName)
			);
		}
		
		public override void Update(SponsorProject sponsorProject, int id)
		{
			string query = @"
UPDATE SponsorProject SET
	SponsorProjectID = @SponsorProjectID,
	SponsorID = @SponsorID,
	StartDT = @StartDT,
	EndDT = @EndDT,
	ProjectName = @ProjectName
WHERE SponsorProjectID = @SponsorProjectID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorProjectID", sponsorProject.SponsorProjectID),
				new SqlParameter("@SponsorID", sponsorProject.SponsorID),
				new SqlParameter("@StartDT", sponsorProject.StartDT),
				new SqlParameter("@EndDT", sponsorProject.EndDT),
				new SqlParameter("@ProjectName", sponsorProject.ProjectName)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorProject
WHERE SponsorProjectID = @SponsorProjectID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorProjectID", id)
			);
		}
		
		public override SponsorProject Read(int id)
		{
			string query = @"
SELECT 	SponsorProjectID, 
	SponsorID, 
	StartDT, 
	EndDT, 
	ProjectName
FROM SponsorProject
WHERE SponsorProjectID = @SponsorProjectID";
			SponsorProject sponsorProject = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorProjectID", id))) {
				if (rs.Read()) {
					sponsorProject = new SponsorProject {
						SponsorProjectID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						StartDT = GetDateTime(rs, 2),
						EndDT = GetDateTime(rs, 3),
						ProjectName = GetString(rs, 4)
					};
				}
			}
			return sponsorProject;
		}
		
		public override IList<SponsorProject> FindAll()
		{
			string query = @"
SELECT 	SponsorProjectID, 
	SponsorID, 
	StartDT, 
	EndDT, 
	ProjectName
FROM SponsorProject";
			var sponsorProjects = new List<SponsorProject>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorProjects.Add(new SponsorProject {
						SponsorProjectID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						StartDT = GetDateTime(rs, 2),
						EndDT = GetDateTime(rs, 3),
						ProjectName = GetString(rs, 4)
					});
				}
			}
			return sponsorProjects;
		}
	}
}

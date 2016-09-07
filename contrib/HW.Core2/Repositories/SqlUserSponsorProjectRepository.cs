using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlUserSponsorProjectRepository : BaseSqlRepository<UserSponsorProject>
	{
		public SqlUserSponsorProjectRepository()
		{
		}
		
		public override void Save(UserSponsorProject userSponsorProject)
		{
			string query = @"
INSERT INTO UserSponsorProject(
	UserSponsorProjectID, 
	UserID, 
	SponsorProjectID, 
	ConsentDT
)
VALUES(
	@UserSponsorProjectID, 
	@UserID, 
	@SponsorProjectID, 
	@ConsentDT
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserSponsorProjectID", userSponsorProject.UserSponsorProjectID),
				new SqlParameter("@UserID", userSponsorProject.UserID),
				new SqlParameter("@SponsorProjectID", userSponsorProject.SponsorProjectID),
				new SqlParameter("@ConsentDT", userSponsorProject.ConsentDT)
			);
		}
		
		public override void Update(UserSponsorProject userSponsorProject, int id)
		{
			string query = @"
UPDATE UserSponsorProject SET
	UserSponsorProjectID = @UserSponsorProjectID,
	UserID = @UserID,
	SponsorProjectID = @SponsorProjectID,
	ConsentDT = @ConsentDT
WHERE UserSponsorProjectID = @UserSponsorProjectID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserSponsorProjectID", userSponsorProject.UserSponsorProjectID),
				new SqlParameter("@UserID", userSponsorProject.UserID),
				new SqlParameter("@SponsorProjectID", userSponsorProject.SponsorProjectID),
				new SqlParameter("@ConsentDT", userSponsorProject.ConsentDT)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM UserSponsorProject
WHERE UserSponsorProjectID = @UserSponsorProjectID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserSponsorProjectID", id)
			);
		}
		
		public override UserSponsorProject Read(int id)
		{
			string query = @"
SELECT 	UserSponsorProjectID, 
	UserID, 
	SponsorProjectID, 
	ConsentDT
FROM UserSponsorProject
WHERE UserSponsorProjectID = @UserSponsorProjectID";
			UserSponsorProject userSponsorProject = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UserSponsorProjectID", id))) {
				if (rs.Read()) {
					userSponsorProject = new UserSponsorProject {
						UserSponsorProjectID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						SponsorProjectID = GetInt32(rs, 2),
						ConsentDT = GetDateTime(rs, 3)
					};
				}
			}
			return userSponsorProject;
		}
		
		public override IList<UserSponsorProject> FindAll()
		{
			string query = @"
SELECT 	UserSponsorProjectID, 
	UserID, 
	SponsorProjectID, 
	ConsentDT
FROM UserSponsorProject";
			var userSponsorProjects = new List<UserSponsorProject>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					userSponsorProjects.Add(new UserSponsorProject {
						UserSponsorProjectID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						SponsorProjectID = GetInt32(rs, 2),
						ConsentDT = GetDateTime(rs, 3)
					});
				}
			}
			return userSponsorProjects;
		}
	}
}

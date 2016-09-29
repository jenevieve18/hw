using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlUserProjectRoundUserRepository : BaseSqlRepository<UserProjectRoundUser>
	{
		public SqlUserProjectRoundUserRepository()
		{
		}
		
		public override void Save(UserProjectRoundUser userProjectRoundUser)
		{
			string query = @"
INSERT INTO UserProjectRoundUser(
	UserProjectRoundUserID, 
	UserID, 
	ProjectRoundUserID, 
	Note
)
VALUES(
	@UserProjectRoundUserID, 
	@UserID, 
	@ProjectRoundUserID, 
	@Note
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserProjectRoundUserID", userProjectRoundUser.UserProjectRoundUserID),
				new SqlParameter("@UserID", userProjectRoundUser.UserID),
				new SqlParameter("@ProjectRoundUserID", userProjectRoundUser.ProjectRoundUserID),
				new SqlParameter("@Note", userProjectRoundUser.Note)
			);
		}
		
		public override void Update(UserProjectRoundUser userProjectRoundUser, int id)
		{
			string query = @"
UPDATE UserProjectRoundUser SET
	UserProjectRoundUserID = @UserProjectRoundUserID,
	UserID = @UserID,
	ProjectRoundUserID = @ProjectRoundUserID,
	Note = @Note
WHERE UserProjectRoundUserID = @UserProjectRoundUserID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserProjectRoundUserID", userProjectRoundUser.UserProjectRoundUserID),
				new SqlParameter("@UserID", userProjectRoundUser.UserID),
				new SqlParameter("@ProjectRoundUserID", userProjectRoundUser.ProjectRoundUserID),
				new SqlParameter("@Note", userProjectRoundUser.Note)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM UserProjectRoundUser
WHERE UserProjectRoundUserID = @UserProjectRoundUserID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserProjectRoundUserID", id)
			);
		}
		
		public override UserProjectRoundUser Read(int id)
		{
			string query = @"
SELECT 	UserProjectRoundUserID, 
	UserID, 
	ProjectRoundUserID, 
	Note
FROM UserProjectRoundUser
WHERE UserProjectRoundUserID = @UserProjectRoundUserID";
			UserProjectRoundUser userProjectRoundUser = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UserProjectRoundUserID", id))) {
				if (rs.Read()) {
					userProjectRoundUser = new UserProjectRoundUser {
						UserProjectRoundUserID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						ProjectRoundUserID = GetInt32(rs, 2),
						Note = GetString(rs, 3)
					};
				}
			}
			return userProjectRoundUser;
		}
		
		public override IList<UserProjectRoundUser> FindAll()
		{
			string query = @"
SELECT 	UserProjectRoundUserID, 
	UserID, 
	ProjectRoundUserID, 
	Note
FROM UserProjectRoundUser";
			var userProjectRoundUsers = new List<UserProjectRoundUser>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					userProjectRoundUsers.Add(new UserProjectRoundUser {
						UserProjectRoundUserID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						ProjectRoundUserID = GetInt32(rs, 2),
						Note = GetString(rs, 3)
					});
				}
			}
			return userProjectRoundUsers;
		}
	}
}

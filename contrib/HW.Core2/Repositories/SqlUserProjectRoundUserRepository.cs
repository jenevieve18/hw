using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
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
	ProjectRoundUnitID, 
	ProjectRoundUserID
)
VALUES(
	@UserProjectRoundUserID, 
	@UserID, 
	@ProjectRoundUnitID, 
	@ProjectRoundUserID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserProjectRoundUserID", userProjectRoundUser.UserProjectRoundUserID),
				new SqlParameter("@UserID", userProjectRoundUser.UserID),
				new SqlParameter("@ProjectRoundUnitID", userProjectRoundUser.ProjectRoundUnitID),
				new SqlParameter("@ProjectRoundUserID", userProjectRoundUser.ProjectRoundUserID)
			);
		}
		
		public override void Update(UserProjectRoundUser userProjectRoundUser, int id)
		{
			string query = @"
UPDATE UserProjectRoundUser SET
	UserProjectRoundUserID = @UserProjectRoundUserID,
	UserID = @UserID,
	ProjectRoundUnitID = @ProjectRoundUnitID,
	ProjectRoundUserID = @ProjectRoundUserID
WHERE UserProjectRoundUserID = @UserProjectRoundUserID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserProjectRoundUserID", userProjectRoundUser.UserProjectRoundUserID),
				new SqlParameter("@UserID", userProjectRoundUser.UserID),
				new SqlParameter("@ProjectRoundUnitID", userProjectRoundUser.ProjectRoundUnitID),
				new SqlParameter("@ProjectRoundUserID", userProjectRoundUser.ProjectRoundUserID)
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
	ProjectRoundUnitID, 
	ProjectRoundUserID
FROM UserProjectRoundUser
WHERE UserProjectRoundUserID = @UserProjectRoundUserID";
			UserProjectRoundUser userProjectRoundUser = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UserProjectRoundUserID", id))) {
				if (rs.Read()) {
					userProjectRoundUser = new UserProjectRoundUser {
						UserProjectRoundUserID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						ProjectRoundUnitID = GetInt32(rs, 2),
						ProjectRoundUserID = GetInt32(rs, 3)
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
	ProjectRoundUnitID, 
	ProjectRoundUserID
FROM UserProjectRoundUser";
			var userProjectRoundUsers = new List<UserProjectRoundUser>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					userProjectRoundUsers.Add(new UserProjectRoundUser {
						UserProjectRoundUserID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						ProjectRoundUnitID = GetInt32(rs, 2),
						ProjectRoundUserID = GetInt32(rs, 3)
					});
				}
			}
			return userProjectRoundUsers;
		}
	}
}

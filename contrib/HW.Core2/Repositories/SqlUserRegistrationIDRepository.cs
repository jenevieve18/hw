using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlUserRegistrationIDRepository : BaseSqlRepository<UserRegistrationID>
	{
		public SqlUserRegistrationIDRepository()
		{
		}
		
		public override void Save(UserRegistrationID userRegistrationID)
		{
			string query = @"
INSERT INTO UserRegistrationID(
	UserRegistrationID, 
	UserID, 
	RegistrationID, 
	Comment
)
VALUES(
	@UserRegistrationID, 
	@UserID, 
	@RegistrationID, 
	@Comment
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserRegistrationID", userRegistrationID.ID),
				new SqlParameter("@UserID", userRegistrationID.UserID),
				new SqlParameter("@RegistrationID", userRegistrationID.RegistrationID),
				new SqlParameter("@Comment", userRegistrationID.Comment)
			);
		}
		
		public override void Update(UserRegistrationID userRegistrationID, int id)
		{
			string query = @"
UPDATE UserRegistrationID SET
	UserRegistrationID = @UserRegistrationID,
	UserID = @UserID,
	RegistrationID = @RegistrationID,
	Comment = @Comment
WHERE UserRegistrationIDID = @UserRegistrationIDID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserRegistrationID", userRegistrationID.ID),
				new SqlParameter("@UserID", userRegistrationID.UserID),
				new SqlParameter("@RegistrationID", userRegistrationID.RegistrationID),
				new SqlParameter("@Comment", userRegistrationID.Comment)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM UserRegistrationID
WHERE UserRegistrationIDID = @UserRegistrationIDID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserRegistrationIDID", id)
			);
		}
		
		public override UserRegistrationID Read(int id)
		{
			string query = @"
SELECT 	UserRegistrationID, 
	UserID, 
	RegistrationID, 
	Comment
FROM UserRegistrationID
WHERE UserRegistrationIDID = @UserRegistrationIDID";
			UserRegistrationID userRegistrationID = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UserRegistrationIDID", id))) {
				if (rs.Read()) {
					userRegistrationID = new UserRegistrationID {
						ID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						RegistrationID = GetString(rs, 2),
						Comment = GetString(rs, 3)
					};
				}
			}
			return userRegistrationID;
		}
		
		public override IList<UserRegistrationID> FindAll()
		{
			string query = @"
SELECT 	UserRegistrationID, 
	UserID, 
	RegistrationID, 
	Comment
FROM UserRegistrationID";
			var userRegistrationIDs = new List<UserRegistrationID>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					userRegistrationIDs.Add(new UserRegistrationID {
						ID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						RegistrationID = GetString(rs, 2),
						Comment = GetString(rs, 3)
					});
				}
			}
			return userRegistrationIDs;
		}
	}
}

using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlUserRepository : BaseSqlRepository<User>
	{
		public SqlUserRepository()
		{
		}
		
		public override void Save(User user)
		{
			string query = @"
INSERT INTO User(
	UserID, 
	SponsorID, 
	UserNr, 
	UserIdent1, 
	UserIdent2, 
	UserIdent3, 
	UserCheck1, 
	UserCheck2, 
	UserCheck3, 
	UserIdent4, 
	UserIdent5, 
	UserIdent6, 
	UserIdent7, 
	UserIdent8, 
	UserIdent9, 
	UserIdent10, 
	FeedbackSent
)
VALUES(
	@UserID, 
	@SponsorID, 
	@UserNr, 
	@UserIdent1, 
	@UserIdent2, 
	@UserIdent3, 
	@UserCheck1, 
	@UserCheck2, 
	@UserCheck3, 
	@UserIdent4, 
	@UserIdent5, 
	@UserIdent6, 
	@UserIdent7, 
	@UserIdent8, 
	@UserIdent9, 
	@UserIdent10, 
	@FeedbackSent
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserID", user.UserID),
				new SqlParameter("@SponsorID", user.SponsorID),
				new SqlParameter("@UserNr", user.UserNr),
				new SqlParameter("@UserIdent1", user.UserIdent1),
				new SqlParameter("@UserIdent2", user.UserIdent2),
				new SqlParameter("@UserIdent3", user.UserIdent3),
				new SqlParameter("@UserCheck1", user.UserCheck1),
				new SqlParameter("@UserCheck2", user.UserCheck2),
				new SqlParameter("@UserCheck3", user.UserCheck3),
				new SqlParameter("@UserIdent4", user.UserIdent4),
				new SqlParameter("@UserIdent5", user.UserIdent5),
				new SqlParameter("@UserIdent6", user.UserIdent6),
				new SqlParameter("@UserIdent7", user.UserIdent7),
				new SqlParameter("@UserIdent8", user.UserIdent8),
				new SqlParameter("@UserIdent9", user.UserIdent9),
				new SqlParameter("@UserIdent10", user.UserIdent10),
				new SqlParameter("@FeedbackSent", user.FeedbackSent)
			);
		}
		
		public override void Update(User user, int id)
		{
			string query = @"
UPDATE User SET
	UserID = @UserID,
	SponsorID = @SponsorID,
	UserNr = @UserNr,
	UserIdent1 = @UserIdent1,
	UserIdent2 = @UserIdent2,
	UserIdent3 = @UserIdent3,
	UserCheck1 = @UserCheck1,
	UserCheck2 = @UserCheck2,
	UserCheck3 = @UserCheck3,
	UserIdent4 = @UserIdent4,
	UserIdent5 = @UserIdent5,
	UserIdent6 = @UserIdent6,
	UserIdent7 = @UserIdent7,
	UserIdent8 = @UserIdent8,
	UserIdent9 = @UserIdent9,
	UserIdent10 = @UserIdent10,
	FeedbackSent = @FeedbackSent
WHERE UserID = @UserID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserID", user.UserID),
				new SqlParameter("@SponsorID", user.SponsorID),
				new SqlParameter("@UserNr", user.UserNr),
				new SqlParameter("@UserIdent1", user.UserIdent1),
				new SqlParameter("@UserIdent2", user.UserIdent2),
				new SqlParameter("@UserIdent3", user.UserIdent3),
				new SqlParameter("@UserCheck1", user.UserCheck1),
				new SqlParameter("@UserCheck2", user.UserCheck2),
				new SqlParameter("@UserCheck3", user.UserCheck3),
				new SqlParameter("@UserIdent4", user.UserIdent4),
				new SqlParameter("@UserIdent5", user.UserIdent5),
				new SqlParameter("@UserIdent6", user.UserIdent6),
				new SqlParameter("@UserIdent7", user.UserIdent7),
				new SqlParameter("@UserIdent8", user.UserIdent8),
				new SqlParameter("@UserIdent9", user.UserIdent9),
				new SqlParameter("@UserIdent10", user.UserIdent10),
				new SqlParameter("@FeedbackSent", user.FeedbackSent)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM User
WHERE UserID = @UserID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserID", id)
			);
		}
		
		public override User Read(int id)
		{
			string query = @"
SELECT 	UserID, 
	SponsorID, 
	UserNr, 
	UserIdent1, 
	UserIdent2, 
	UserIdent3, 
	UserCheck1, 
	UserCheck2, 
	UserCheck3, 
	UserIdent4, 
	UserIdent5, 
	UserIdent6, 
	UserIdent7, 
	UserIdent8, 
	UserIdent9, 
	UserIdent10, 
	FeedbackSent
FROM User
WHERE UserID = @UserID";
			User user = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UserID", id))) {
				if (rs.Read()) {
					user = new User {
						UserID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						UserNr = GetInt32(rs, 2),
						UserIdent1 = GetString(rs, 3),
						UserIdent2 = GetString(rs, 4),
						UserIdent3 = GetString(rs, 5),
						UserCheck1 = GetInt32(rs, 6),
						UserCheck2 = GetInt32(rs, 7),
						UserCheck3 = GetInt32(rs, 8),
						UserIdent4 = GetString(rs, 9),
						UserIdent5 = GetString(rs, 10),
						UserIdent6 = GetString(rs, 11),
						UserIdent7 = GetString(rs, 12),
						UserIdent8 = GetString(rs, 13),
						UserIdent9 = GetString(rs, 14),
						UserIdent10 = GetString(rs, 15),
						FeedbackSent = GetString(rs, 16)
					};
				}
			}
			return user;
		}
		
		public override IList<User> FindAll()
		{
			string query = @"
SELECT 	UserID, 
	SponsorID, 
	UserNr, 
	UserIdent1, 
	UserIdent2, 
	UserIdent3, 
	UserCheck1, 
	UserCheck2, 
	UserCheck3, 
	UserIdent4, 
	UserIdent5, 
	UserIdent6, 
	UserIdent7, 
	UserIdent8, 
	UserIdent9, 
	UserIdent10, 
	FeedbackSent
FROM User";
			var users = new List<User>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					users.Add(new User {
						UserID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						UserNr = GetInt32(rs, 2),
						UserIdent1 = GetString(rs, 3),
						UserIdent2 = GetString(rs, 4),
						UserIdent3 = GetString(rs, 5),
						UserCheck1 = GetInt32(rs, 6),
						UserCheck2 = GetInt32(rs, 7),
						UserCheck3 = GetInt32(rs, 8),
						UserIdent4 = GetString(rs, 9),
						UserIdent5 = GetString(rs, 10),
						UserIdent6 = GetString(rs, 11),
						UserIdent7 = GetString(rs, 12),
						UserIdent8 = GetString(rs, 13),
						UserIdent9 = GetString(rs, 14),
						UserIdent10 = GetString(rs, 15),
						FeedbackSent = GetString(rs, 16)
					});
				}
			}
			return users;
		}
	}
}

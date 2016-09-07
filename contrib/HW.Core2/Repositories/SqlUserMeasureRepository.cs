using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlUserMeasureRepository : BaseSqlRepository<UserMeasure>
	{
		public SqlUserMeasureRepository()
		{
		}
		
		public override void Save(UserMeasure userMeasure)
		{
			string query = @"
INSERT INTO UserMeasure(
	UserMeasureID, 
	UserID, 
	DT, 
	CreatedDT, 
	DeletedDT, 
	UserProfileID
)
VALUES(
	@UserMeasureID, 
	@UserID, 
	@DT, 
	@CreatedDT, 
	@DeletedDT, 
	@UserProfileID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserMeasureID", userMeasure.UserMeasureID),
				new SqlParameter("@UserID", userMeasure.UserID),
				new SqlParameter("@DT", userMeasure.DT),
				new SqlParameter("@CreatedDT", userMeasure.CreatedDT),
				new SqlParameter("@DeletedDT", userMeasure.DeletedDT),
				new SqlParameter("@UserProfileID", userMeasure.UserProfileID)
			);
		}
		
		public override void Update(UserMeasure userMeasure, int id)
		{
			string query = @"
UPDATE UserMeasure SET
	UserMeasureID = @UserMeasureID,
	UserID = @UserID,
	DT = @DT,
	CreatedDT = @CreatedDT,
	DeletedDT = @DeletedDT,
	UserProfileID = @UserProfileID
WHERE UserMeasureID = @UserMeasureID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserMeasureID", userMeasure.UserMeasureID),
				new SqlParameter("@UserID", userMeasure.UserID),
				new SqlParameter("@DT", userMeasure.DT),
				new SqlParameter("@CreatedDT", userMeasure.CreatedDT),
				new SqlParameter("@DeletedDT", userMeasure.DeletedDT),
				new SqlParameter("@UserProfileID", userMeasure.UserProfileID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM UserMeasure
WHERE UserMeasureID = @UserMeasureID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserMeasureID", id)
			);
		}
		
		public override UserMeasure Read(int id)
		{
			string query = @"
SELECT 	UserMeasureID, 
	UserID, 
	DT, 
	CreatedDT, 
	DeletedDT, 
	UserProfileID
FROM UserMeasure
WHERE UserMeasureID = @UserMeasureID";
			UserMeasure userMeasure = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UserMeasureID", id))) {
				if (rs.Read()) {
					userMeasure = new UserMeasure {
						UserMeasureID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						DT = GetDateTime(rs, 2),
						CreatedDT = GetDateTime(rs, 3),
						DeletedDT = GetDateTime(rs, 4),
						UserProfileID = GetInt32(rs, 5)
					};
				}
			}
			return userMeasure;
		}
		
		public override IList<UserMeasure> FindAll()
		{
			string query = @"
SELECT 	UserMeasureID, 
	UserID, 
	DT, 
	CreatedDT, 
	DeletedDT, 
	UserProfileID
FROM UserMeasure";
			var userMeasures = new List<UserMeasure>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					userMeasures.Add(new UserMeasure {
						UserMeasureID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						DT = GetDateTime(rs, 2),
						CreatedDT = GetDateTime(rs, 3),
						DeletedDT = GetDateTime(rs, 4),
						UserProfileID = GetInt32(rs, 5)
					});
				}
			}
			return userMeasures;
		}
	}
}

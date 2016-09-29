using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlUserCategoryRepository : BaseSqlRepository<UserCategory>
	{
		public SqlUserCategoryRepository()
		{
		}
		
		public override void Save(UserCategory userCategory)
		{
			string query = @"
INSERT INTO UserCategory(
	UserCategoryID, 
	Internal
)
VALUES(
	@UserCategoryID, 
	@Internal
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserCategoryID", userCategory.UserCategoryID),
				new SqlParameter("@Internal", userCategory.Internal)
			);
		}
		
		public override void Update(UserCategory userCategory, int id)
		{
			string query = @"
UPDATE UserCategory SET
	UserCategoryID = @UserCategoryID,
	Internal = @Internal
WHERE UserCategoryID = @UserCategoryID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserCategoryID", userCategory.UserCategoryID),
				new SqlParameter("@Internal", userCategory.Internal)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM UserCategory
WHERE UserCategoryID = @UserCategoryID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserCategoryID", id)
			);
		}
		
		public override UserCategory Read(int id)
		{
			string query = @"
SELECT 	UserCategoryID, 
	Internal
FROM UserCategory
WHERE UserCategoryID = @UserCategoryID";
			UserCategory userCategory = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UserCategoryID", id))) {
				if (rs.Read()) {
					userCategory = new UserCategory {
						UserCategoryID = GetInt32(rs, 0),
						Internal = GetString(rs, 1)
					};
				}
			}
			return userCategory;
		}
		
		public override IList<UserCategory> FindAll()
		{
			string query = @"
SELECT 	UserCategoryID, 
	Internal
FROM UserCategory";
			var userCategorys = new List<UserCategory>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					userCategorys.Add(new UserCategory {
						UserCategoryID = GetInt32(rs, 0),
						Internal = GetString(rs, 1)
					});
				}
			}
			return userCategorys;
		}
	}
}

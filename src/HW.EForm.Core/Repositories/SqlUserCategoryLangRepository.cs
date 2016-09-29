using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlUserCategoryLangRepository : BaseSqlRepository<UserCategoryLang>
	{
		public SqlUserCategoryLangRepository()
		{
		}
		
		public override void Save(UserCategoryLang userCategoryLang)
		{
			string query = @"
INSERT INTO UserCategoryLang(
	UserCategoryLangID, 
	UserCategoryID, 
	LangID, 
	Category, 
	CategoryJapaneseUnicode
)
VALUES(
	@UserCategoryLangID, 
	@UserCategoryID, 
	@LangID, 
	@Category, 
	@CategoryJapaneseUnicode
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserCategoryLangID", userCategoryLang.UserCategoryLangID),
				new SqlParameter("@UserCategoryID", userCategoryLang.UserCategoryID),
				new SqlParameter("@LangID", userCategoryLang.LangID),
				new SqlParameter("@Category", userCategoryLang.Category),
				new SqlParameter("@CategoryJapaneseUnicode", userCategoryLang.CategoryJapaneseUnicode)
			);
		}
		
		public override void Update(UserCategoryLang userCategoryLang, int id)
		{
			string query = @"
UPDATE UserCategoryLang SET
	UserCategoryLangID = @UserCategoryLangID,
	UserCategoryID = @UserCategoryID,
	LangID = @LangID,
	Category = @Category,
	CategoryJapaneseUnicode = @CategoryJapaneseUnicode
WHERE UserCategoryLangID = @UserCategoryLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserCategoryLangID", userCategoryLang.UserCategoryLangID),
				new SqlParameter("@UserCategoryID", userCategoryLang.UserCategoryID),
				new SqlParameter("@LangID", userCategoryLang.LangID),
				new SqlParameter("@Category", userCategoryLang.Category),
				new SqlParameter("@CategoryJapaneseUnicode", userCategoryLang.CategoryJapaneseUnicode)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM UserCategoryLang
WHERE UserCategoryLangID = @UserCategoryLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserCategoryLangID", id)
			);
		}
		
		public override UserCategoryLang Read(int id)
		{
			string query = @"
SELECT 	UserCategoryLangID, 
	UserCategoryID, 
	LangID, 
	Category, 
	CategoryJapaneseUnicode
FROM UserCategoryLang
WHERE UserCategoryLangID = @UserCategoryLangID";
			UserCategoryLang userCategoryLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UserCategoryLangID", id))) {
				if (rs.Read()) {
					userCategoryLang = new UserCategoryLang {
						UserCategoryLangID = GetInt32(rs, 0),
						UserCategoryID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Category = GetString(rs, 3),
						CategoryJapaneseUnicode = GetString(rs, 4)
					};
				}
			}
			return userCategoryLang;
		}
		
		public override IList<UserCategoryLang> FindAll()
		{
			string query = @"
SELECT 	UserCategoryLangID, 
	UserCategoryID, 
	LangID, 
	Category, 
	CategoryJapaneseUnicode
FROM UserCategoryLang";
			var userCategoryLangs = new List<UserCategoryLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					userCategoryLangs.Add(new UserCategoryLang {
						UserCategoryLangID = GetInt32(rs, 0),
						UserCategoryID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Category = GetString(rs, 3),
						CategoryJapaneseUnicode = GetString(rs, 4)
					});
				}
			}
			return userCategoryLangs;
		}
	}
}

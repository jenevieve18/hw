using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlUnitCategoryLangRepository : BaseSqlRepository<UnitCategoryLang>
	{
		public SqlUnitCategoryLangRepository()
		{
		}
		
		public override void Save(UnitCategoryLang unitCategoryLang)
		{
			string query = @"
INSERT INTO UnitCategoryLang(
	UnitCategoryLangID, 
	UnitCategoryID, 
	LangID, 
	Category, 
	CategoryJapaneseUnicode
)
VALUES(
	@UnitCategoryLangID, 
	@UnitCategoryID, 
	@LangID, 
	@Category, 
	@CategoryJapaneseUnicode
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UnitCategoryLangID", unitCategoryLang.UnitCategoryLangID),
				new SqlParameter("@UnitCategoryID", unitCategoryLang.UnitCategoryID),
				new SqlParameter("@LangID", unitCategoryLang.LangID),
				new SqlParameter("@Category", unitCategoryLang.Category),
				new SqlParameter("@CategoryJapaneseUnicode", unitCategoryLang.CategoryJapaneseUnicode)
			);
		}
		
		public override void Update(UnitCategoryLang unitCategoryLang, int id)
		{
			string query = @"
UPDATE UnitCategoryLang SET
	UnitCategoryLangID = @UnitCategoryLangID,
	UnitCategoryID = @UnitCategoryID,
	LangID = @LangID,
	Category = @Category,
	CategoryJapaneseUnicode = @CategoryJapaneseUnicode
WHERE UnitCategoryLangID = @UnitCategoryLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UnitCategoryLangID", unitCategoryLang.UnitCategoryLangID),
				new SqlParameter("@UnitCategoryID", unitCategoryLang.UnitCategoryID),
				new SqlParameter("@LangID", unitCategoryLang.LangID),
				new SqlParameter("@Category", unitCategoryLang.Category),
				new SqlParameter("@CategoryJapaneseUnicode", unitCategoryLang.CategoryJapaneseUnicode)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM UnitCategoryLang
WHERE UnitCategoryLangID = @UnitCategoryLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UnitCategoryLangID", id)
			);
		}
		
		public override UnitCategoryLang Read(int id)
		{
			string query = @"
SELECT 	UnitCategoryLangID, 
	UnitCategoryID, 
	LangID, 
	Category, 
	CategoryJapaneseUnicode
FROM UnitCategoryLang
WHERE UnitCategoryLangID = @UnitCategoryLangID";
			UnitCategoryLang unitCategoryLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UnitCategoryLangID", id))) {
				if (rs.Read()) {
					unitCategoryLang = new UnitCategoryLang {
						UnitCategoryLangID = GetInt32(rs, 0),
						UnitCategoryID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Category = GetString(rs, 3),
						CategoryJapaneseUnicode = GetString(rs, 4)
					};
				}
			}
			return unitCategoryLang;
		}
		
		public override IList<UnitCategoryLang> FindAll()
		{
			string query = @"
SELECT 	UnitCategoryLangID, 
	UnitCategoryID, 
	LangID, 
	Category, 
	CategoryJapaneseUnicode
FROM UnitCategoryLang";
			var unitCategoryLangs = new List<UnitCategoryLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					unitCategoryLangs.Add(new UnitCategoryLang {
						UnitCategoryLangID = GetInt32(rs, 0),
						UnitCategoryID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Category = GetString(rs, 3),
						CategoryJapaneseUnicode = GetString(rs, 4)
					});
				}
			}
			return unitCategoryLangs;
		}
	}
}

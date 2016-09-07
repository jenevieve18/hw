using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlMeasureCategoryLangRepository : BaseSqlRepository<MeasureCategoryLang>
	{
		public SqlMeasureCategoryLangRepository()
		{
		}
		
		public override void Save(MeasureCategoryLang measureCategoryLang)
		{
			string query = @"
INSERT INTO MeasureCategoryLang(
	MeasureCategoryLangID, 
	MeasureCategoryID, 
	LangID, 
	MeasureCategory
)
VALUES(
	@MeasureCategoryLangID, 
	@MeasureCategoryID, 
	@LangID, 
	@MeasureCategory
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureCategoryLangID", measureCategoryLang.MeasureCategoryLangID),
				new SqlParameter("@MeasureCategoryID", measureCategoryLang.MeasureCategoryID),
				new SqlParameter("@LangID", measureCategoryLang.LangID),
				new SqlParameter("@MeasureCategory", measureCategoryLang.MeasureCategory)
			);
		}
		
		public override void Update(MeasureCategoryLang measureCategoryLang, int id)
		{
			string query = @"
UPDATE MeasureCategoryLang SET
	MeasureCategoryLangID = @MeasureCategoryLangID,
	MeasureCategoryID = @MeasureCategoryID,
	LangID = @LangID,
	MeasureCategory = @MeasureCategory
WHERE MeasureCategoryLangID = @MeasureCategoryLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureCategoryLangID", measureCategoryLang.MeasureCategoryLangID),
				new SqlParameter("@MeasureCategoryID", measureCategoryLang.MeasureCategoryID),
				new SqlParameter("@LangID", measureCategoryLang.LangID),
				new SqlParameter("@MeasureCategory", measureCategoryLang.MeasureCategory)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM MeasureCategoryLang
WHERE MeasureCategoryLangID = @MeasureCategoryLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureCategoryLangID", id)
			);
		}
		
		public override MeasureCategoryLang Read(int id)
		{
			string query = @"
SELECT 	MeasureCategoryLangID, 
	MeasureCategoryID, 
	LangID, 
	MeasureCategory
FROM MeasureCategoryLang
WHERE MeasureCategoryLangID = @MeasureCategoryLangID";
			MeasureCategoryLang measureCategoryLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@MeasureCategoryLangID", id))) {
				if (rs.Read()) {
					measureCategoryLang = new MeasureCategoryLang {
						MeasureCategoryLangID = GetInt32(rs, 0),
						MeasureCategoryID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						MeasureCategory = GetString(rs, 3)
					};
				}
			}
			return measureCategoryLang;
		}
		
		public override IList<MeasureCategoryLang> FindAll()
		{
			string query = @"
SELECT 	MeasureCategoryLangID, 
	MeasureCategoryID, 
	LangID, 
	MeasureCategory
FROM MeasureCategoryLang";
			var measureCategoryLangs = new List<MeasureCategoryLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					measureCategoryLangs.Add(new MeasureCategoryLang {
						MeasureCategoryLangID = GetInt32(rs, 0),
						MeasureCategoryID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						MeasureCategory = GetString(rs, 3)
					});
				}
			}
			return measureCategoryLangs;
		}
	}
}

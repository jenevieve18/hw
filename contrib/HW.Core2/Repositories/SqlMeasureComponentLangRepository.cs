using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlMeasureComponentLangRepository : BaseSqlRepository<MeasureComponentLang>
	{
		public SqlMeasureComponentLangRepository()
		{
		}
		
		public override void Save(MeasureComponentLang measureComponentLang)
		{
			string query = @"
INSERT INTO MeasureComponentLang(
	MeasureComponentLangID, 
	MeasureComponentID, 
	LangID, 
	MeasureComponent, 
	Unit
)
VALUES(
	@MeasureComponentLangID, 
	@MeasureComponentID, 
	@LangID, 
	@MeasureComponent, 
	@Unit
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureComponentLangID", measureComponentLang.MeasureComponentLangID),
				new SqlParameter("@MeasureComponentID", measureComponentLang.MeasureComponentID),
				new SqlParameter("@LangID", measureComponentLang.LangID),
				new SqlParameter("@MeasureComponent", measureComponentLang.MeasureComponent),
				new SqlParameter("@Unit", measureComponentLang.Unit)
			);
		}
		
		public override void Update(MeasureComponentLang measureComponentLang, int id)
		{
			string query = @"
UPDATE MeasureComponentLang SET
	MeasureComponentLangID = @MeasureComponentLangID,
	MeasureComponentID = @MeasureComponentID,
	LangID = @LangID,
	MeasureComponent = @MeasureComponent,
	Unit = @Unit
WHERE MeasureComponentLangID = @MeasureComponentLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureComponentLangID", measureComponentLang.MeasureComponentLangID),
				new SqlParameter("@MeasureComponentID", measureComponentLang.MeasureComponentID),
				new SqlParameter("@LangID", measureComponentLang.LangID),
				new SqlParameter("@MeasureComponent", measureComponentLang.MeasureComponent),
				new SqlParameter("@Unit", measureComponentLang.Unit)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM MeasureComponentLang
WHERE MeasureComponentLangID = @MeasureComponentLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureComponentLangID", id)
			);
		}
		
		public override MeasureComponentLang Read(int id)
		{
			string query = @"
SELECT 	MeasureComponentLangID, 
	MeasureComponentID, 
	LangID, 
	MeasureComponent, 
	Unit
FROM MeasureComponentLang
WHERE MeasureComponentLangID = @MeasureComponentLangID";
			MeasureComponentLang measureComponentLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@MeasureComponentLangID", id))) {
				if (rs.Read()) {
					measureComponentLang = new MeasureComponentLang {
						MeasureComponentLangID = GetInt32(rs, 0),
						MeasureComponentID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						MeasureComponent = GetString(rs, 3),
						Unit = GetString(rs, 4)
					};
				}
			}
			return measureComponentLang;
		}
		
		public override IList<MeasureComponentLang> FindAll()
		{
			string query = @"
SELECT 	MeasureComponentLangID, 
	MeasureComponentID, 
	LangID, 
	MeasureComponent, 
	Unit
FROM MeasureComponentLang";
			var measureComponentLangs = new List<MeasureComponentLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					measureComponentLangs.Add(new MeasureComponentLang {
						MeasureComponentLangID = GetInt32(rs, 0),
						MeasureComponentID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						MeasureComponent = GetString(rs, 3),
						Unit = GetString(rs, 4)
					});
				}
			}
			return measureComponentLangs;
		}
	}
}

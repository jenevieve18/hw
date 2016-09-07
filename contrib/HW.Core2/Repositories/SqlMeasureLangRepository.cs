using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlMeasureLangRepository : BaseSqlRepository<MeasureLang>
	{
		public SqlMeasureLangRepository()
		{
		}
		
		public override void Save(MeasureLang measureLang)
		{
			string query = @"
INSERT INTO MeasureLang(
	MeasureLangID, 
	MeasureID, 
	LangID, 
	Measure
)
VALUES(
	@MeasureLangID, 
	@MeasureID, 
	@LangID, 
	@Measure
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureLangID", measureLang.MeasureLangID),
				new SqlParameter("@MeasureID", measureLang.MeasureID),
				new SqlParameter("@LangID", measureLang.LangID),
				new SqlParameter("@Measure", measureLang.Measure)
			);
		}
		
		public override void Update(MeasureLang measureLang, int id)
		{
			string query = @"
UPDATE MeasureLang SET
	MeasureLangID = @MeasureLangID,
	MeasureID = @MeasureID,
	LangID = @LangID,
	Measure = @Measure
WHERE MeasureLangID = @MeasureLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureLangID", measureLang.MeasureLangID),
				new SqlParameter("@MeasureID", measureLang.MeasureID),
				new SqlParameter("@LangID", measureLang.LangID),
				new SqlParameter("@Measure", measureLang.Measure)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM MeasureLang
WHERE MeasureLangID = @MeasureLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureLangID", id)
			);
		}
		
		public override MeasureLang Read(int id)
		{
			string query = @"
SELECT 	MeasureLangID, 
	MeasureID, 
	LangID, 
	Measure
FROM MeasureLang
WHERE MeasureLangID = @MeasureLangID";
			MeasureLang measureLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@MeasureLangID", id))) {
				if (rs.Read()) {
					measureLang = new MeasureLang {
						MeasureLangID = GetInt32(rs, 0),
						MeasureID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Measure = GetString(rs, 3)
					};
				}
			}
			return measureLang;
		}
		
		public override IList<MeasureLang> FindAll()
		{
			string query = @"
SELECT 	MeasureLangID, 
	MeasureID, 
	LangID, 
	Measure
FROM MeasureLang";
			var measureLangs = new List<MeasureLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					measureLangs.Add(new MeasureLang {
						MeasureLangID = GetInt32(rs, 0),
						MeasureID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Measure = GetString(rs, 3)
					});
				}
			}
			return measureLangs;
		}
	}
}

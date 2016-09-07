using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlMeasureTypeLangRepository : BaseSqlRepository<MeasureTypeLang>
	{
		public SqlMeasureTypeLangRepository()
		{
		}
		
		public override void Save(MeasureTypeLang measureTypeLang)
		{
			string query = @"
INSERT INTO MeasureTypeLang(
	MeasureTypeLangID, 
	MeasureTypeID, 
	LangID, 
	MeasureType
)
VALUES(
	@MeasureTypeLangID, 
	@MeasureTypeID, 
	@LangID, 
	@MeasureType
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureTypeLangID", measureTypeLang.MeasureTypeLangID),
				new SqlParameter("@MeasureTypeID", measureTypeLang.MeasureTypeID),
				new SqlParameter("@LangID", measureTypeLang.LangID),
				new SqlParameter("@MeasureType", measureTypeLang.MeasureType)
			);
		}
		
		public override void Update(MeasureTypeLang measureTypeLang, int id)
		{
			string query = @"
UPDATE MeasureTypeLang SET
	MeasureTypeLangID = @MeasureTypeLangID,
	MeasureTypeID = @MeasureTypeID,
	LangID = @LangID,
	MeasureType = @MeasureType
WHERE MeasureTypeLangID = @MeasureTypeLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureTypeLangID", measureTypeLang.MeasureTypeLangID),
				new SqlParameter("@MeasureTypeID", measureTypeLang.MeasureTypeID),
				new SqlParameter("@LangID", measureTypeLang.LangID),
				new SqlParameter("@MeasureType", measureTypeLang.MeasureType)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM MeasureTypeLang
WHERE MeasureTypeLangID = @MeasureTypeLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureTypeLangID", id)
			);
		}
		
		public override MeasureTypeLang Read(int id)
		{
			string query = @"
SELECT 	MeasureTypeLangID, 
	MeasureTypeID, 
	LangID, 
	MeasureType
FROM MeasureTypeLang
WHERE MeasureTypeLangID = @MeasureTypeLangID";
			MeasureTypeLang measureTypeLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@MeasureTypeLangID", id))) {
				if (rs.Read()) {
					measureTypeLang = new MeasureTypeLang {
						MeasureTypeLangID = GetInt32(rs, 0),
						MeasureTypeID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						MeasureType = GetString(rs, 3)
					};
				}
			}
			return measureTypeLang;
		}
		
		public override IList<MeasureTypeLang> FindAll()
		{
			string query = @"
SELECT 	MeasureTypeLangID, 
	MeasureTypeID, 
	LangID, 
	MeasureType
FROM MeasureTypeLang";
			var measureTypeLangs = new List<MeasureTypeLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					measureTypeLangs.Add(new MeasureTypeLang {
						MeasureTypeLangID = GetInt32(rs, 0),
						MeasureTypeID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						MeasureType = GetString(rs, 3)
					});
				}
			}
			return measureTypeLangs;
		}
	}
}

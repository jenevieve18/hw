using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlPlotTypeLangRepository : BaseSqlRepository<PlotTypeLang>
	{
		public SqlPlotTypeLangRepository()
		{
		}
		
		public override void Save(PlotTypeLang plotTypeLang)
		{
			string query = @"
INSERT INTO PlotTypeLang(
	PlotTypeLangID, 
	PlotTypeID, 
	LangID, 
	Name, 
	Description, 
	ShortName, 
	SupportsMultipleSeries
)
VALUES(
	@PlotTypeLangID, 
	@PlotTypeID, 
	@LangID, 
	@Name, 
	@Description, 
	@ShortName, 
	@SupportsMultipleSeries
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@PlotTypeLangID", plotTypeLang.PlotTypeLangID),
				new SqlParameter("@PlotTypeID", plotTypeLang.PlotTypeID),
				new SqlParameter("@LangID", plotTypeLang.LangID),
				new SqlParameter("@Name", plotTypeLang.Name),
				new SqlParameter("@Description", plotTypeLang.Description),
				new SqlParameter("@ShortName", plotTypeLang.ShortName),
				new SqlParameter("@SupportsMultipleSeries", plotTypeLang.SupportsMultipleSeries)
			);
		}
		
		public override void Update(PlotTypeLang plotTypeLang, int id)
		{
			string query = @"
UPDATE PlotTypeLang SET
	PlotTypeLangID = @PlotTypeLangID,
	PlotTypeID = @PlotTypeID,
	LangID = @LangID,
	Name = @Name,
	Description = @Description,
	ShortName = @ShortName,
	SupportsMultipleSeries = @SupportsMultipleSeries
WHERE PlotTypeLangID = @PlotTypeLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@PlotTypeLangID", plotTypeLang.PlotTypeLangID),
				new SqlParameter("@PlotTypeID", plotTypeLang.PlotTypeID),
				new SqlParameter("@LangID", plotTypeLang.LangID),
				new SqlParameter("@Name", plotTypeLang.Name),
				new SqlParameter("@Description", plotTypeLang.Description),
				new SqlParameter("@ShortName", plotTypeLang.ShortName),
				new SqlParameter("@SupportsMultipleSeries", plotTypeLang.SupportsMultipleSeries)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM PlotTypeLang
WHERE PlotTypeLangID = @PlotTypeLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@PlotTypeLangID", id)
			);
		}
		
		public override PlotTypeLang Read(int id)
		{
			string query = @"
SELECT 	PlotTypeLangID, 
	PlotTypeID, 
	LangID, 
	Name, 
	Description, 
	ShortName, 
	SupportsMultipleSeries
FROM PlotTypeLang
WHERE PlotTypeLangID = @PlotTypeLangID";
			PlotTypeLang plotTypeLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@PlotTypeLangID", id))) {
				if (rs.Read()) {
					plotTypeLang = new PlotTypeLang {
						PlotTypeLangID = GetInt32(rs, 0),
						PlotTypeID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Name = GetString(rs, 3),
						Description = GetString(rs, 4),
						ShortName = GetString(rs, 5),
						SupportsMultipleSeries = GetInt32(rs, 6)
					};
				}
			}
			return plotTypeLang;
		}
		
		public override IList<PlotTypeLang> FindAll()
		{
			string query = @"
SELECT 	PlotTypeLangID, 
	PlotTypeID, 
	LangID, 
	Name, 
	Description, 
	ShortName, 
	SupportsMultipleSeries
FROM PlotTypeLang";
			var plotTypeLangs = new List<PlotTypeLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					plotTypeLangs.Add(new PlotTypeLang {
						PlotTypeLangID = GetInt32(rs, 0),
						PlotTypeID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Name = GetString(rs, 3),
						Description = GetString(rs, 4),
						ShortName = GetString(rs, 5),
						SupportsMultipleSeries = GetInt32(rs, 6)
					});
				}
			}
			return plotTypeLangs;
		}
	}
}

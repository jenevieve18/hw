using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlPlotTypeRepository : BaseSqlRepository<PlotType>
	{
		public SqlPlotTypeRepository()
		{
		}
		
		public override void Save(PlotType plotType)
		{
			string query = @"
INSERT INTO PlotType(
	Id, 
	Name, 
	Description
)
VALUES(
	@Id, 
	@Name, 
	@Description
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@Id", plotType.Id),
				new SqlParameter("@Name", plotType.Name),
				new SqlParameter("@Description", plotType.Description)
			);
		}
		
		public override void Update(PlotType plotType, int id)
		{
			string query = @"
UPDATE PlotType SET
	Id = @Id,
	Name = @Name,
	Description = @Description
WHERE PlotTypeID = @PlotTypeID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@Id", plotType.Id),
				new SqlParameter("@Name", plotType.Name),
				new SqlParameter("@Description", plotType.Description)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM PlotType
WHERE PlotTypeID = @PlotTypeID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@PlotTypeID", id)
			);
		}
		
		public override PlotType Read(int id)
		{
			string query = @"
SELECT 	Id, 
	Name, 
	Description
FROM PlotType
WHERE PlotTypeID = @PlotTypeID";
			PlotType plotType = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@PlotTypeID", id))) {
				if (rs.Read()) {
					plotType = new PlotType {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1),
						Description = GetString(rs, 2)
					};
				}
			}
			return plotType;
		}
		
		public override IList<PlotType> FindAll()
		{
			string query = @"
SELECT 	Id, 
	Name, 
	Description
FROM PlotType";
			var plotTypes = new List<PlotType>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					plotTypes.Add(new PlotType {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1),
						Description = GetString(rs, 2)
					});
				}
			}
			return plotTypes;
		}
	}
}

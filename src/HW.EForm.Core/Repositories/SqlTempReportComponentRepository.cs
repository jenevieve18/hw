using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlTempReportComponentRepository : BaseSqlRepository<TempReportComponent>
	{
		public SqlTempReportComponentRepository()
		{
		}
		
		public override void Save(TempReportComponent tempReportComponent)
		{
			string query = @"
INSERT INTO TempReportComponent(
	TempReportComponentID, 
	TempReportID, 
	TempReportComponent
)
VALUES(
	@TempReportComponentID, 
	@TempReportID, 
	@TempReportComponent
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@TempReportComponentID", tempReportComponent.TempReportComponentID),
				new SqlParameter("@TempReportID", tempReportComponent.TempReportID),
				new SqlParameter("@TempReportComponent", tempReportComponent.TempReportComponentText)
			);
		}
		
		public override void Update(TempReportComponent tempReportComponent, int id)
		{
			string query = @"
UPDATE TempReportComponent SET
	TempReportComponentID = @TempReportComponentID,
	TempReportID = @TempReportID,
	TempReportComponent = @TempReportComponent
WHERE TempReportComponentID = @TempReportComponentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@TempReportComponentID", tempReportComponent.TempReportComponentID),
				new SqlParameter("@TempReportID", tempReportComponent.TempReportID),
				new SqlParameter("@TempReportComponent", tempReportComponent.TempReportComponentText)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM TempReportComponent
WHERE TempReportComponentID = @TempReportComponentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@TempReportComponentID", id)
			);
		}
		
		public override TempReportComponent Read(int id)
		{
			string query = @"
SELECT 	TempReportComponentID, 
	TempReportID, 
	TempReportComponent
FROM TempReportComponent
WHERE TempReportComponentID = @TempReportComponentID";
			TempReportComponent tempReportComponent = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@TempReportComponentID", id))) {
				if (rs.Read()) {
					tempReportComponent = new TempReportComponent {
						TempReportComponentID = GetInt32(rs, 0),
						TempReportID = GetInt32(rs, 1),
						TempReportComponentText = GetString(rs, 2)
					};
				}
			}
			return tempReportComponent;
		}
		
		public override IList<TempReportComponent> FindAll()
		{
			string query = @"
SELECT 	TempReportComponentID, 
	TempReportID, 
	TempReportComponent
FROM TempReportComponent";
			var tempReportComponents = new List<TempReportComponent>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					tempReportComponents.Add(new TempReportComponent {
						TempReportComponentID = GetInt32(rs, 0),
						TempReportID = GetInt32(rs, 1),
						TempReportComponentText = GetString(rs, 2)
					});
				}
			}
			return tempReportComponents;
		}
	}
}

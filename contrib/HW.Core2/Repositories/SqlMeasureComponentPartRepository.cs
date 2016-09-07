using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlMeasureComponentPartRepository : BaseSqlRepository<MeasureComponentPart>
	{
		public SqlMeasureComponentPartRepository()
		{
		}
		
		public override void Save(MeasureComponentPart measureComponentPart)
		{
			string query = @"
INSERT INTO MeasureComponentPart(
	MeasureComponentPartID, 
	MeasureComponentID, 
	MeasureComponentPart, 
	SortOrder
)
VALUES(
	@MeasureComponentPartID, 
	@MeasureComponentID, 
	@MeasureComponentPart, 
	@SortOrder
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureComponentPartID", measureComponentPart.MeasureComponentPartID),
				new SqlParameter("@MeasureComponentID", measureComponentPart.MeasureComponentID),
				new SqlParameter("@MeasureComponentPart", measureComponentPart.ComponentPart),
				new SqlParameter("@SortOrder", measureComponentPart.SortOrder)
			);
		}
		
		public override void Update(MeasureComponentPart measureComponentPart, int id)
		{
			string query = @"
UPDATE MeasureComponentPart SET
	MeasureComponentPartID = @MeasureComponentPartID,
	MeasureComponentID = @MeasureComponentID,
	MeasureComponentPart = @MeasureComponentPart,
	SortOrder = @SortOrder
WHERE MeasureComponentPartID = @MeasureComponentPartID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureComponentPartID", measureComponentPart.MeasureComponentPartID),
				new SqlParameter("@MeasureComponentID", measureComponentPart.MeasureComponentID),
				new SqlParameter("@MeasureComponentPart", measureComponentPart.ComponentPart),
				new SqlParameter("@SortOrder", measureComponentPart.SortOrder)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM MeasureComponentPart
WHERE MeasureComponentPartID = @MeasureComponentPartID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureComponentPartID", id)
			);
		}
		
		public override MeasureComponentPart Read(int id)
		{
			string query = @"
SELECT 	MeasureComponentPartID, 
	MeasureComponentID, 
	MeasureComponentPart, 
	SortOrder
FROM MeasureComponentPart
WHERE MeasureComponentPartID = @MeasureComponentPartID";
			MeasureComponentPart measureComponentPart = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@MeasureComponentPartID", id))) {
				if (rs.Read()) {
					measureComponentPart = new MeasureComponentPart {
						MeasureComponentPartID = GetInt32(rs, 0),
						MeasureComponentID = GetInt32(rs, 1),
						ComponentPart = GetInt32(rs, 2),
						SortOrder = GetInt32(rs, 3)
					};
				}
			}
			return measureComponentPart;
		}
		
		public override IList<MeasureComponentPart> FindAll()
		{
			string query = @"
SELECT 	MeasureComponentPartID, 
	MeasureComponentID, 
	MeasureComponentPart, 
	SortOrder
FROM MeasureComponentPart";
			var measureComponentParts = new List<MeasureComponentPart>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					measureComponentParts.Add(new MeasureComponentPart {
						MeasureComponentPartID = GetInt32(rs, 0),
						MeasureComponentID = GetInt32(rs, 1),
						ComponentPart = GetInt32(rs, 2),
						SortOrder = GetInt32(rs, 3)
					});
				}
			}
			return measureComponentParts;
		}
	}
}

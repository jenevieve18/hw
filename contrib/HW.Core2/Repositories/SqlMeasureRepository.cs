using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlMeasureRepository : BaseSqlRepository<Measure>
	{
		public SqlMeasureRepository()
		{
		}
		
		public override void Save(Measure measure)
		{
			string query = @"
INSERT INTO Measure(
	MeasureID, 
	Measure, 
	MeasureCategoryID, 
	SortOrder, 
	MoreInfo
)
VALUES(
	@MeasureID, 
	@Measure, 
	@MeasureCategoryID, 
	@SortOrder, 
	@MoreInfo
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureID", measure.MeasureID),
				new SqlParameter("@Measure", measure.MeasureText),
				new SqlParameter("@MeasureCategoryID", measure.MeasureCategoryID),
				new SqlParameter("@SortOrder", measure.SortOrder),
				new SqlParameter("@MoreInfo", measure.MoreInfo)
			);
		}
		
		public override void Update(Measure measure, int id)
		{
			string query = @"
UPDATE Measure SET
	MeasureID = @MeasureID,
	Measure = @Measure,
	MeasureCategoryID = @MeasureCategoryID,
	SortOrder = @SortOrder,
	MoreInfo = @MoreInfo
WHERE MeasureID = @MeasureID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureID", measure.MeasureID),
				new SqlParameter("@Measure", measure.MeasureText),
				new SqlParameter("@MeasureCategoryID", measure.MeasureCategoryID),
				new SqlParameter("@SortOrder", measure.SortOrder),
				new SqlParameter("@MoreInfo", measure.MoreInfo)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Measure
WHERE MeasureID = @MeasureID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureID", id)
			);
		}
		
		public override Measure Read(int id)
		{
			string query = @"
SELECT 	MeasureID, 
	Measure, 
	MeasureCategoryID, 
	SortOrder, 
	MoreInfo
FROM Measure
WHERE MeasureID = @MeasureID";
			Measure measure = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@MeasureID", id))) {
				if (rs.Read()) {
					measure = new Measure {
						MeasureID = GetInt32(rs, 0),
						MeasureText = GetString(rs, 1),
						MeasureCategoryID = GetInt32(rs, 2),
						SortOrder = GetInt32(rs, 3),
						MoreInfo = GetString(rs, 4)
					};
				}
			}
			return measure;
		}
		
		public override IList<Measure> FindAll()
		{
			string query = @"
SELECT 	MeasureID, 
	Measure, 
	MeasureCategoryID, 
	SortOrder, 
	MoreInfo
FROM Measure";
			var measures = new List<Measure>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					measures.Add(new Measure {
						MeasureID = GetInt32(rs, 0),
						MeasureText = GetString(rs, 1),
						MeasureCategoryID = GetInt32(rs, 2),
						SortOrder = GetInt32(rs, 3),
						MoreInfo = GetString(rs, 4)
					});
				}
			}
			return measures;
		}
	}
}

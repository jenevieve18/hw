using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlMeasureTypeRepository : BaseSqlRepository<MeasureType>
	{
		public SqlMeasureTypeRepository()
		{
		}
		
		public override void Save(MeasureType measureType)
		{
			string query = @"
INSERT INTO MeasureType(
	MeasureTypeID, 
	MeasureType, 
	SortOrder, 
	Active
)
VALUES(
	@MeasureTypeID, 
	@MeasureType, 
	@SortOrder, 
	@Active
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureTypeID", measureType.MeasureTypeID),
				new SqlParameter("@MeasureType", measureType.Type),
				new SqlParameter("@SortOrder", measureType.SortOrder),
				new SqlParameter("@Active", measureType.Active)
			);
		}
		
		public override void Update(MeasureType measureType, int id)
		{
			string query = @"
UPDATE MeasureType SET
	MeasureTypeID = @MeasureTypeID,
	MeasureType = @MeasureType,
	SortOrder = @SortOrder,
	Active = @Active
WHERE MeasureTypeID = @MeasureTypeID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureTypeID", measureType.MeasureTypeID),
				new SqlParameter("@MeasureType", measureType.Type),
				new SqlParameter("@SortOrder", measureType.SortOrder),
				new SqlParameter("@Active", measureType.Active)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM MeasureType
WHERE MeasureTypeID = @MeasureTypeID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureTypeID", id)
			);
		}
		
		public override MeasureType Read(int id)
		{
			string query = @"
SELECT 	MeasureTypeID, 
	MeasureType, 
	SortOrder, 
	Active
FROM MeasureType
WHERE MeasureTypeID = @MeasureTypeID";
			MeasureType measureType = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@MeasureTypeID", id))) {
				if (rs.Read()) {
					measureType = new MeasureType {
						MeasureTypeID = GetInt32(rs, 0),
						Type = GetString(rs, 1),
						SortOrder = GetInt32(rs, 2),
						Active = GetInt32(rs, 3)
					};
				}
			}
			return measureType;
		}
		
		public override IList<MeasureType> FindAll()
		{
			string query = @"
SELECT 	MeasureTypeID, 
	MeasureType, 
	SortOrder, 
	Active
FROM MeasureType";
			var measureTypes = new List<MeasureType>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					measureTypes.Add(new MeasureType {
						MeasureTypeID = GetInt32(rs, 0),
						Type = GetString(rs, 1),
						SortOrder = GetInt32(rs, 2),
						Active = GetInt32(rs, 3)
					});
				}
			}
			return measureTypes;
		}
	}
}

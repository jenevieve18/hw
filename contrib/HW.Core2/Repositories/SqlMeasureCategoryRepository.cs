using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlMeasureCategoryRepository : BaseSqlRepository<MeasureCategory>
	{
		public SqlMeasureCategoryRepository()
		{
		}
		
		public override void Save(MeasureCategory measureCategory)
		{
			string query = @"
INSERT INTO MeasureCategory(
	MeasureCategoryID, 
	MeasureCategory, 
	MeasureTypeID, 
	SortOrder, 
	SPRUID
)
VALUES(
	@MeasureCategoryID, 
	@MeasureCategory, 
	@MeasureTypeID, 
	@SortOrder, 
	@SPRUID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureCategoryID", measureCategory.MeasureCategoryID),
				new SqlParameter("@MeasureCategory", measureCategory.Category),
				new SqlParameter("@MeasureTypeID", measureCategory.MeasureTypeID),
				new SqlParameter("@SortOrder", measureCategory.SortOrder),
				new SqlParameter("@SPRUID", measureCategory.SPRUID)
			);
		}
		
		public override void Update(MeasureCategory measureCategory, int id)
		{
			string query = @"
UPDATE MeasureCategory SET
	MeasureCategoryID = @MeasureCategoryID,
	MeasureCategory = @MeasureCategory,
	MeasureTypeID = @MeasureTypeID,
	SortOrder = @SortOrder,
	SPRUID = @SPRUID
WHERE MeasureCategoryID = @MeasureCategoryID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureCategoryID", measureCategory.MeasureCategoryID),
				new SqlParameter("@MeasureCategory", measureCategory.Category),
				new SqlParameter("@MeasureTypeID", measureCategory.MeasureTypeID),
				new SqlParameter("@SortOrder", measureCategory.SortOrder),
				new SqlParameter("@SPRUID", measureCategory.SPRUID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM MeasureCategory
WHERE MeasureCategoryID = @MeasureCategoryID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@MeasureCategoryID", id)
			);
		}
		
		public override MeasureCategory Read(int id)
		{
			string query = @"
SELECT 	MeasureCategoryID, 
	MeasureCategory, 
	MeasureTypeID, 
	SortOrder, 
	SPRUID
FROM MeasureCategory
WHERE MeasureCategoryID = @MeasureCategoryID";
			MeasureCategory measureCategory = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@MeasureCategoryID", id))) {
				if (rs.Read()) {
					measureCategory = new MeasureCategory {
						MeasureCategoryID = GetInt32(rs, 0),
						Category = GetString(rs, 1),
						MeasureTypeID = GetInt32(rs, 2),
						SortOrder = GetInt32(rs, 3),
						SPRUID = GetInt32(rs, 4)
					};
				}
			}
			return measureCategory;
		}
		
		public override IList<MeasureCategory> FindAll()
		{
			string query = @"
SELECT 	MeasureCategoryID, 
	MeasureCategory, 
	MeasureTypeID, 
	SortOrder, 
	SPRUID
FROM MeasureCategory";
			var measureCategorys = new List<MeasureCategory>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					measureCategorys.Add(new MeasureCategory {
						MeasureCategoryID = GetInt32(rs, 0),
						Category = GetString(rs, 1),
						MeasureTypeID = GetInt32(rs, 2),
						SortOrder = GetInt32(rs, 3),
						SPRUID = GetInt32(rs, 4)
					});
				}
			}
			return measureCategorys;
		}
	}
}

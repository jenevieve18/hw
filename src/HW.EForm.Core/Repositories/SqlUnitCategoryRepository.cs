using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlUnitCategoryRepository : BaseSqlRepository<UnitCategory>
	{
		public SqlUnitCategoryRepository()
		{
		}
		
		public override void Save(UnitCategory unitCategory)
		{
			string query = @"
INSERT INTO UnitCategory(
	UnitCategoryID, 
	Internal
)
VALUES(
	@UnitCategoryID, 
	@Internal
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UnitCategoryID", unitCategory.UnitCategoryID),
				new SqlParameter("@Internal", unitCategory.Internal)
			);
		}
		
		public override void Update(UnitCategory unitCategory, int id)
		{
			string query = @"
UPDATE UnitCategory SET
	UnitCategoryID = @UnitCategoryID,
	Internal = @Internal
WHERE UnitCategoryID = @UnitCategoryID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UnitCategoryID", unitCategory.UnitCategoryID),
				new SqlParameter("@Internal", unitCategory.Internal)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM UnitCategory
WHERE UnitCategoryID = @UnitCategoryID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UnitCategoryID", id)
			);
		}
		
		public override UnitCategory Read(int id)
		{
			string query = @"
SELECT 	UnitCategoryID, 
	Internal
FROM UnitCategory
WHERE UnitCategoryID = @UnitCategoryID";
			UnitCategory unitCategory = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UnitCategoryID", id))) {
				if (rs.Read()) {
					unitCategory = new UnitCategory {
						UnitCategoryID = GetInt32(rs, 0),
						Internal = GetString(rs, 1)
					};
				}
			}
			return unitCategory;
		}
		
		public override IList<UnitCategory> FindAll()
		{
			string query = @"
SELECT 	UnitCategoryID, 
	Internal
FROM UnitCategory";
			var unitCategorys = new List<UnitCategory>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					unitCategorys.Add(new UnitCategory {
						UnitCategoryID = GetInt32(rs, 0),
						Internal = GetString(rs, 1)
					});
				}
			}
			return unitCategorys;
		}
	}
}

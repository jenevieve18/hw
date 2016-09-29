using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlProjectUnitCategoryRepository : BaseSqlRepository<ProjectUnitCategory>
	{
		public SqlProjectUnitCategoryRepository()
		{
		}
		
		public override void Save(ProjectUnitCategory projectUnitCategory)
		{
			string query = @"
INSERT INTO ProjectUnitCategory(
	ProjectUnitCategoryID, 
	ProjectID, 
	UnitCategoryID
)
VALUES(
	@ProjectUnitCategoryID, 
	@ProjectID, 
	@UnitCategoryID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectUnitCategoryID", projectUnitCategory.ProjectUnitCategoryID),
				new SqlParameter("@ProjectID", projectUnitCategory.ProjectID),
				new SqlParameter("@UnitCategoryID", projectUnitCategory.UnitCategoryID)
			);
		}
		
		public override void Update(ProjectUnitCategory projectUnitCategory, int id)
		{
			string query = @"
UPDATE ProjectUnitCategory SET
	ProjectUnitCategoryID = @ProjectUnitCategoryID,
	ProjectID = @ProjectID,
	UnitCategoryID = @UnitCategoryID
WHERE ProjectUnitCategoryID = @ProjectUnitCategoryID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectUnitCategoryID", projectUnitCategory.ProjectUnitCategoryID),
				new SqlParameter("@ProjectID", projectUnitCategory.ProjectID),
				new SqlParameter("@UnitCategoryID", projectUnitCategory.UnitCategoryID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ProjectUnitCategory
WHERE ProjectUnitCategoryID = @ProjectUnitCategoryID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectUnitCategoryID", id)
			);
		}
		
		public override ProjectUnitCategory Read(int id)
		{
			string query = @"
SELECT 	ProjectUnitCategoryID, 
	ProjectID, 
	UnitCategoryID
FROM ProjectUnitCategory
WHERE ProjectUnitCategoryID = @ProjectUnitCategoryID";
			ProjectUnitCategory projectUnitCategory = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ProjectUnitCategoryID", id))) {
				if (rs.Read()) {
					projectUnitCategory = new ProjectUnitCategory {
						ProjectUnitCategoryID = GetInt32(rs, 0),
						ProjectID = GetInt32(rs, 1),
						UnitCategoryID = GetInt32(rs, 2)
					};
				}
			}
			return projectUnitCategory;
		}
		
		public override IList<ProjectUnitCategory> FindAll()
		{
			string query = @"
SELECT 	ProjectUnitCategoryID, 
	ProjectID, 
	UnitCategoryID
FROM ProjectUnitCategory";
			var projectUnitCategorys = new List<ProjectUnitCategory>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					projectUnitCategorys.Add(new ProjectUnitCategory {
						ProjectUnitCategoryID = GetInt32(rs, 0),
						ProjectID = GetInt32(rs, 1),
						UnitCategoryID = GetInt32(rs, 2)
					});
				}
			}
			return projectUnitCategorys;
		}
	}
}

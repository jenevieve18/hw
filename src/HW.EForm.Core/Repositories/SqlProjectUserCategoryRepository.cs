using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlProjectUserCategoryRepository : BaseSqlRepository<ProjectUserCategory>
	{
		public SqlProjectUserCategoryRepository()
		{
		}
		
		public override void Save(ProjectUserCategory projectUserCategory)
		{
			string query = @"
INSERT INTO ProjectUserCategory(
	ProjectUserCategoryID, 
	ProjectID, 
	UserCategoryID
)
VALUES(
	@ProjectUserCategoryID, 
	@ProjectID, 
	@UserCategoryID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectUserCategoryID", projectUserCategory.ProjectUserCategoryID),
				new SqlParameter("@ProjectID", projectUserCategory.ProjectID),
				new SqlParameter("@UserCategoryID", projectUserCategory.UserCategoryID)
			);
		}
		
		public override void Update(ProjectUserCategory projectUserCategory, int id)
		{
			string query = @"
UPDATE ProjectUserCategory SET
	ProjectUserCategoryID = @ProjectUserCategoryID,
	ProjectID = @ProjectID,
	UserCategoryID = @UserCategoryID
WHERE ProjectUserCategoryID = @ProjectUserCategoryID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectUserCategoryID", projectUserCategory.ProjectUserCategoryID),
				new SqlParameter("@ProjectID", projectUserCategory.ProjectID),
				new SqlParameter("@UserCategoryID", projectUserCategory.UserCategoryID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ProjectUserCategory
WHERE ProjectUserCategoryID = @ProjectUserCategoryID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectUserCategoryID", id)
			);
		}
		
		public override ProjectUserCategory Read(int id)
		{
			string query = @"
SELECT 	ProjectUserCategoryID, 
	ProjectID, 
	UserCategoryID
FROM ProjectUserCategory
WHERE ProjectUserCategoryID = @ProjectUserCategoryID";
			ProjectUserCategory projectUserCategory = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ProjectUserCategoryID", id))) {
				if (rs.Read()) {
					projectUserCategory = new ProjectUserCategory {
						ProjectUserCategoryID = GetInt32(rs, 0),
						ProjectID = GetInt32(rs, 1),
						UserCategoryID = GetInt32(rs, 2)
					};
				}
			}
			return projectUserCategory;
		}
		
		public override IList<ProjectUserCategory> FindAll()
		{
			string query = @"
SELECT 	ProjectUserCategoryID, 
	ProjectID, 
	UserCategoryID
FROM ProjectUserCategory";
			var projectUserCategorys = new List<ProjectUserCategory>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					projectUserCategorys.Add(new ProjectUserCategory {
						ProjectUserCategoryID = GetInt32(rs, 0),
						ProjectID = GetInt32(rs, 1),
						UserCategoryID = GetInt32(rs, 2)
					});
				}
			}
			return projectUserCategorys;
		}
	}
}

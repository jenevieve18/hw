using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlProjectRoundUserCompareRepository : BaseSqlRepository<ProjectRoundUserCompare>
	{
		public SqlProjectRoundUserCompareRepository()
		{
		}
		
		public override void Save(ProjectRoundUserCompare projectRoundUserCompare)
		{
			string query = @"
INSERT INTO ProjectRoundUserCompare(
	ProjectRoundUserCompareID, 
	ProjectRoundUserID, 
	CompareProjectRoundUserID
)
VALUES(
	@ProjectRoundUserCompareID, 
	@ProjectRoundUserID, 
	@CompareProjectRoundUserID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundUserCompareID", projectRoundUserCompare.ProjectRoundUserCompareID),
				new SqlParameter("@ProjectRoundUserID", projectRoundUserCompare.ProjectRoundUserID),
				new SqlParameter("@CompareProjectRoundUserID", projectRoundUserCompare.CompareProjectRoundUserID)
			);
		}
		
		public override void Update(ProjectRoundUserCompare projectRoundUserCompare, int id)
		{
			string query = @"
UPDATE ProjectRoundUserCompare SET
	ProjectRoundUserCompareID = @ProjectRoundUserCompareID,
	ProjectRoundUserID = @ProjectRoundUserID,
	CompareProjectRoundUserID = @CompareProjectRoundUserID
WHERE ProjectRoundUserCompareID = @ProjectRoundUserCompareID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundUserCompareID", projectRoundUserCompare.ProjectRoundUserCompareID),
				new SqlParameter("@ProjectRoundUserID", projectRoundUserCompare.ProjectRoundUserID),
				new SqlParameter("@CompareProjectRoundUserID", projectRoundUserCompare.CompareProjectRoundUserID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ProjectRoundUserCompare
WHERE ProjectRoundUserCompareID = @ProjectRoundUserCompareID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundUserCompareID", id)
			);
		}
		
		public override ProjectRoundUserCompare Read(int id)
		{
			string query = @"
SELECT 	ProjectRoundUserCompareID, 
	ProjectRoundUserID, 
	CompareProjectRoundUserID
FROM ProjectRoundUserCompare
WHERE ProjectRoundUserCompareID = @ProjectRoundUserCompareID";
			ProjectRoundUserCompare projectRoundUserCompare = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ProjectRoundUserCompareID", id))) {
				if (rs.Read()) {
					projectRoundUserCompare = new ProjectRoundUserCompare {
						ProjectRoundUserCompareID = GetInt32(rs, 0),
						ProjectRoundUserID = GetInt32(rs, 1),
						CompareProjectRoundUserID = GetInt32(rs, 2)
					};
				}
			}
			return projectRoundUserCompare;
		}
		
		public override IList<ProjectRoundUserCompare> FindAll()
		{
			string query = @"
SELECT 	ProjectRoundUserCompareID, 
	ProjectRoundUserID, 
	CompareProjectRoundUserID
FROM ProjectRoundUserCompare";
			var projectRoundUserCompares = new List<ProjectRoundUserCompare>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					projectRoundUserCompares.Add(new ProjectRoundUserCompare {
						ProjectRoundUserCompareID = GetInt32(rs, 0),
						ProjectRoundUserID = GetInt32(rs, 1),
						CompareProjectRoundUserID = GetInt32(rs, 2)
					});
				}
			}
			return projectRoundUserCompares;
		}
	}
}

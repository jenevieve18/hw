using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlProjectRoundQORepository : BaseSqlRepository<ProjectRoundQO>
	{
		public SqlProjectRoundQORepository()
		{
		}
		
		public override void Save(ProjectRoundQO projectRoundQO)
		{
			string query = @"
INSERT INTO ProjectRoundQO(
	ProjectRoundQOID, 
	ProjectRoundID, 
	QuestionID, 
	OptionID, 
	SortOrder
)
VALUES(
	@ProjectRoundQOID, 
	@ProjectRoundID, 
	@QuestionID, 
	@OptionID, 
	@SortOrder
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundQOID", projectRoundQO.ProjectRoundQOID),
				new SqlParameter("@ProjectRoundID", projectRoundQO.ProjectRoundID),
				new SqlParameter("@QuestionID", projectRoundQO.QuestionID),
				new SqlParameter("@OptionID", projectRoundQO.OptionID),
				new SqlParameter("@SortOrder", projectRoundQO.SortOrder)
			);
		}
		
		public override void Update(ProjectRoundQO projectRoundQO, int id)
		{
			string query = @"
UPDATE ProjectRoundQO SET
	ProjectRoundQOID = @ProjectRoundQOID,
	ProjectRoundID = @ProjectRoundID,
	QuestionID = @QuestionID,
	OptionID = @OptionID,
	SortOrder = @SortOrder
WHERE ProjectRoundQOID = @ProjectRoundQOID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundQOID", projectRoundQO.ProjectRoundQOID),
				new SqlParameter("@ProjectRoundID", projectRoundQO.ProjectRoundID),
				new SqlParameter("@QuestionID", projectRoundQO.QuestionID),
				new SqlParameter("@OptionID", projectRoundQO.OptionID),
				new SqlParameter("@SortOrder", projectRoundQO.SortOrder)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ProjectRoundQO
WHERE ProjectRoundQOID = @ProjectRoundQOID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundQOID", id)
			);
		}
		
		public override ProjectRoundQO Read(int id)
		{
			string query = @"
SELECT 	ProjectRoundQOID, 
	ProjectRoundID, 
	QuestionID, 
	OptionID, 
	SortOrder
FROM ProjectRoundQO
WHERE ProjectRoundQOID = @ProjectRoundQOID";
			ProjectRoundQO projectRoundQO = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ProjectRoundQOID", id))) {
				if (rs.Read()) {
					projectRoundQO = new ProjectRoundQO {
						ProjectRoundQOID = GetInt32(rs, 0),
						ProjectRoundID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2),
						OptionID = GetInt32(rs, 3),
						SortOrder = GetInt32(rs, 4)
					};
				}
			}
			return projectRoundQO;
		}
		
		public override IList<ProjectRoundQO> FindAll()
		{
			string query = @"
SELECT 	ProjectRoundQOID, 
	ProjectRoundID, 
	QuestionID, 
	OptionID, 
	SortOrder
FROM ProjectRoundQO";
			var projectRoundQOs = new List<ProjectRoundQO>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					projectRoundQOs.Add(new ProjectRoundQO {
						ProjectRoundQOID = GetInt32(rs, 0),
						ProjectRoundID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2),
						OptionID = GetInt32(rs, 3),
						SortOrder = GetInt32(rs, 4)
					});
				}
			}
			return projectRoundQOs;
		}
	}
}

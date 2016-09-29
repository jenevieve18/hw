using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlProjectRoundUserQORepository : BaseSqlRepository<ProjectRoundUserQO>
	{
		public SqlProjectRoundUserQORepository()
		{
		}
		
		public override void Save(ProjectRoundUserQO projectRoundUserQO)
		{
			string query = @"
INSERT INTO ProjectRoundUserQO(
	ProjectRoundUserQOID, 
	ProjectRoundUserID, 
	QuestionID, 
	OptionID, 
	Answer
)
VALUES(
	@ProjectRoundUserQOID, 
	@ProjectRoundUserID, 
	@QuestionID, 
	@OptionID, 
	@Answer
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundUserQOID", projectRoundUserQO.ProjectRoundUserQOID),
				new SqlParameter("@ProjectRoundUserID", projectRoundUserQO.ProjectRoundUserID),
				new SqlParameter("@QuestionID", projectRoundUserQO.QuestionID),
				new SqlParameter("@OptionID", projectRoundUserQO.OptionID),
				new SqlParameter("@Answer", projectRoundUserQO.Answer)
			);
		}
		
		public override void Update(ProjectRoundUserQO projectRoundUserQO, int id)
		{
			string query = @"
UPDATE ProjectRoundUserQO SET
	ProjectRoundUserQOID = @ProjectRoundUserQOID,
	ProjectRoundUserID = @ProjectRoundUserID,
	QuestionID = @QuestionID,
	OptionID = @OptionID,
	Answer = @Answer
WHERE ProjectRoundUserQOID = @ProjectRoundUserQOID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundUserQOID", projectRoundUserQO.ProjectRoundUserQOID),
				new SqlParameter("@ProjectRoundUserID", projectRoundUserQO.ProjectRoundUserID),
				new SqlParameter("@QuestionID", projectRoundUserQO.QuestionID),
				new SqlParameter("@OptionID", projectRoundUserQO.OptionID),
				new SqlParameter("@Answer", projectRoundUserQO.Answer)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ProjectRoundUserQO
WHERE ProjectRoundUserQOID = @ProjectRoundUserQOID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundUserQOID", id)
			);
		}
		
		public override ProjectRoundUserQO Read(int id)
		{
			string query = @"
SELECT 	ProjectRoundUserQOID, 
	ProjectRoundUserID, 
	QuestionID, 
	OptionID, 
	Answer
FROM ProjectRoundUserQO
WHERE ProjectRoundUserQOID = @ProjectRoundUserQOID";
			ProjectRoundUserQO projectRoundUserQO = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ProjectRoundUserQOID", id))) {
				if (rs.Read()) {
					projectRoundUserQO = new ProjectRoundUserQO {
						ProjectRoundUserQOID = GetInt32(rs, 0),
						ProjectRoundUserID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2),
						OptionID = GetInt32(rs, 3),
						Answer = GetString(rs, 4)
					};
				}
			}
			return projectRoundUserQO;
		}
		
		public override IList<ProjectRoundUserQO> FindAll()
		{
			string query = @"
SELECT 	ProjectRoundUserQOID, 
	ProjectRoundUserID, 
	QuestionID, 
	OptionID, 
	Answer
FROM ProjectRoundUserQO";
			var projectRoundUserQOs = new List<ProjectRoundUserQO>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					projectRoundUserQOs.Add(new ProjectRoundUserQO {
						ProjectRoundUserQOID = GetInt32(rs, 0),
						ProjectRoundUserID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2),
						OptionID = GetInt32(rs, 3),
						Answer = GetString(rs, 4)
					});
				}
			}
			return projectRoundUserQOs;
		}
	}
}

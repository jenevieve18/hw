using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlProjectRepository : BaseSqlRepository<Project>
	{
		public SqlProjectRepository()
		{
		}
		
		public override void Save(Project project)
		{
			string query = @"
INSERT INTO Project(
	Internal, 
	Name, 
	AppURL
)
VALUES(
	@Internal, 
	@Name, 
	@AppURL
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@Internal", project.Internal),
				new SqlParameter("@Name", project.Name),
				new SqlParameter("@AppURL", project.AppURL)
			);
		}
		
		public override void Update(Project project, int id)
		{
			string query = @"
UPDATE Project SET
	ProjectID = @ProjectID,
	Internal = @Internal,
	Name = @Name,
	AppURL = @AppURL
WHERE ProjectID = @ProjectID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectID", project.ProjectID),
				new SqlParameter("@Internal", project.Internal),
				new SqlParameter("@Name", project.Name),
				new SqlParameter("@AppURL", project.AppURL)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Project
WHERE ProjectID = @ProjectID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectID", id)
			);
		}
		
		public override Project Read(int id)
		{
			string query = @"
SELECT 	ProjectID, 
	Internal, 
	Name, 
	AppURL
FROM Project
WHERE ProjectID = @ProjectID";
			Project project = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ProjectID", id))) {
				if (rs.Read()) {
					project = new Project {
						ProjectID = GetInt32(rs, 0),
						Internal = GetString(rs, 1),
						Name = GetString(rs, 2),
						AppURL = GetString(rs, 3)
					};
				}
			}
			return project;
		}
		
		public Project Read(int projectID, int managerID)
		{
			string query = @"
SELECT 	p.ProjectID, 
	p.Internal, 
	p.Name, 
	p.AppURL
FROM Project p
INNER JOIN ProjectRound pr ON pr.ProjectID = p.ProjectID
INNER JOIN ManagerProjectRound mpr ON mpr.ProjectRoundID = pr.ProjectRoundID AND mpr.ManagerID = @ManagerID
WHERE p.ProjectID = @ProjectID";
			Project project = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ProjectID", projectID), new SqlParameter("@ManagerID", managerID))) {
				if (rs.Read()) {
					project = new Project {
						ProjectID = GetInt32(rs, 0),
						Internal = GetString(rs, 1),
						Name = GetString(rs, 2),
						AppURL = GetString(rs, 3)
					};
				}
			}
			return project;
		}
		
		public override IList<Project> FindAll()
		{
			string query = @"
SELECT 	ProjectID, 
	Internal, 
	Name, 
	AppURL
FROM Project";
			var projects = new List<Project>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					projects.Add(new Project {
						ProjectID = GetInt32(rs, 0),
						Internal = GetString(rs, 1),
						Name = GetString(rs, 2),
						AppURL = GetString(rs, 3)
					});
				}
			}
			return projects;
		}
		
		public IList<Project> FindByManager(int managerID)
		{
			string query = @"
SELECT 	p.ProjectID, 
	p.Internal, 
	p.Name, 
	p.AppURL
FROM Project p
INNER JOIN ProjectRound pr ON pr.ProjectID = p.ProjectID
INNER JOIN ManagerProjectRound mpr ON mpr.ProjectRoundID = pr.ProjectRoundID AND mpr.ManagerID = @ManagerID";
			var projects = new List<Project>();
			using (var rs = ExecuteReader(query, new SqlParameter("@ManagerID", managerID))) {
				while (rs.Read()) {
					projects.Add(new Project {
						ProjectID = GetInt32(rs, 0),
						Internal = GetString(rs, 1),
						Name = GetString(rs, 2),
						AppURL = GetString(rs, 3)
					});
				}
			}
			return projects;
		}
	}
}

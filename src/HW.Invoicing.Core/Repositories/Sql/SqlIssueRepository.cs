using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Repositories.Sql
{
	public class SqlIssueRepository : BaseSqlRepository<Issue>
	{
		public void Deactivate(int id)
		{
			string query = @"
UPDATE Issue SET Status = @Deactivated
WHERE Id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Id", id),
                new SqlParameter("@Deactivated", Issue.DEACTIVATED)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Issue
WHERE Id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Id", id)
			);
		}
		
		public override void Save(Issue t)
		{
			string query = @"
INSERT INTO Issue(Title, Description, CreatedAt, Status)
VALUES(@Title, @Description, @CreatedAt, @Status)";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Title", t.Title),
				new SqlParameter("@Description", t.Description),
				new SqlParameter("@CreatedAt", DateTime.Now),
				new SqlParameter("@Status", t.Status)
			);
		}
		
		public override void Update(Issue t, int id)
		{
			string query = @"
UPDATE Issue SET Title = @Title,
	Description = @Description,
	Status = @Status
WHERE Id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Title", t.Title),
				new SqlParameter("@Description", t.Description),
				new SqlParameter("@Id", id),
                new SqlParameter("@Status", t.Status)
			);
		}
		
		public override Issue Read(int id)
		{
			string query = string.Format(
				@"
SELECT Id, Title, Description, Status
FROM Issue
WHERE Id = @Id"
			);
			Issue issue = null;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				while (rs.Read()) {
					issue = new Issue {
						Id = GetInt32(rs, 0),
						Title = GetString(rs, 1),
						Description = GetString(rs, 2),
						Status = GetInt32(rs, 3)
					};
				}
			}
			return issue;
		}
		
		public override IList<Issue> FindAll()
		{
			string query = string.Format(
				@"
SELECT Id,
	Title,
	Description,
	Status
FROM Issue
ORDER BY Status, CreatedAt DESC"
			);
			var issues = new List<Issue>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing")) {
				while (rs.Read()) {
					issues.Add(
						new Issue {
							Id = GetInt32(rs, 0),
							Title = GetString(rs, 1),
							Description = GetString(rs, 2),
							Status = GetInt32(rs, 3)
						}
					);
				}
			}
			return issues;
		}
	}
}

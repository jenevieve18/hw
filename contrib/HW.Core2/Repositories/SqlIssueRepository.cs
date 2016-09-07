using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlIssueRepository : BaseSqlRepository<Issue>
	{
		public SqlIssueRepository()
		{
		}
		
		public override void Save(Issue issue)
		{
			string query = @"
INSERT INTO Issue(
	IssueID, 
	IssueDate, 
	Title, 
	Description, 
	UserID, 
	Status
)
VALUES(
	@IssueID, 
	@IssueDate, 
	@Title, 
	@Description, 
	@UserID, 
	@Status
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@IssueID", issue.IssueID),
				new SqlParameter("@IssueDate", issue.IssueDate),
				new SqlParameter("@Title", issue.Title),
				new SqlParameter("@Description", issue.Description),
				new SqlParameter("@UserID", issue.UserID),
				new SqlParameter("@Status", issue.Status)
			);
		}
		
		public override void Update(Issue issue, int id)
		{
			string query = @"
UPDATE Issue SET
	IssueID = @IssueID,
	IssueDate = @IssueDate,
	Title = @Title,
	Description = @Description,
	UserID = @UserID,
	Status = @Status
WHERE IssueID = @IssueID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@IssueID", issue.IssueID),
				new SqlParameter("@IssueDate", issue.IssueDate),
				new SqlParameter("@Title", issue.Title),
				new SqlParameter("@Description", issue.Description),
				new SqlParameter("@UserID", issue.UserID),
				new SqlParameter("@Status", issue.Status)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Issue
WHERE IssueID = @IssueID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@IssueID", id)
			);
		}
		
		public override Issue Read(int id)
		{
			string query = @"
SELECT 	IssueID, 
	IssueDate, 
	Title, 
	Description, 
	UserID, 
	Status
FROM Issue
WHERE IssueID = @IssueID";
			Issue issue = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@IssueID", id))) {
				if (rs.Read()) {
					issue = new Issue {
						IssueID = GetInt32(rs, 0),
						IssueDate = GetDateTime(rs, 1),
						Title = GetString(rs, 2),
						Description = GetString(rs, 3),
						UserID = GetInt32(rs, 4),
						Status = GetInt32(rs, 5)
					};
				}
			}
			return issue;
		}
		
		public override IList<Issue> FindAll()
		{
			string query = @"
SELECT 	IssueID, 
	IssueDate, 
	Title, 
	Description, 
	UserID, 
	Status
FROM Issue";
			var issues = new List<Issue>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					issues.Add(new Issue {
						IssueID = GetInt32(rs, 0),
						IssueDate = GetDateTime(rs, 1),
						Title = GetString(rs, 2),
						Description = GetString(rs, 3),
						UserID = GetInt32(rs, 4),
						Status = GetInt32(rs, 5)
					});
				}
			}
			return issues;
		}
	}
}

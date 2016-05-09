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
//			string query = @"
//DELETE FROM Issue
//WHERE Id = @Id";
//			ExecuteNonQuery(
//				query,
//				"invoicing",
//				new SqlParameter("@Id", id)
//			);

			string query = @"
UPDATE Issue SET Status = @Status
WHERE Id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Id", id),
				new SqlParameter("@Status", Issue.DELETED)
			);
		}
		
		public override void Save(Issue t)
		{
			string query = @"
INSERT INTO Issue(Title, Description, CreatedAt, Status, MilestoneId, Priority)
VALUES(@Title, @Description, @CreatedAt, @Status, @MilestoneId, @Priority)";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Title", t.Title),
				new SqlParameter("@Description", t.Description),
				new SqlParameter("@CreatedAt", DateTime.Now),
				new SqlParameter("@Status", t.Status),
				new SqlParameter("@MilestoneId", t.Milestone.Id),
				new SqlParameter("@Priority", t.Priority.Id)
			);
		}

		public void SaveComment(IssueComment t, int issueId, int userId)
		{
			string query = @"
INSERT INTO IssueComment(Comments, IssueId, UserId, Date)
VALUES(@Comments, @IssueId, @UserId, GETDATE())";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Comments", t.Comments),
				new SqlParameter("@IssueId", issueId),
				new SqlParameter("@UserId", userId)
			);
		}
		
		public override void Update(Issue t, int id)
		{
			string query = @"
UPDATE Issue SET Title = @Title,
	Description = @Description,
	Status = @Status,
    MilestoneId = @MilestoneId,
    Priority = @Priority
WHERE Id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Title", t.Title),
				new SqlParameter("@Description", t.Description),
				new SqlParameter("@Id", id),
				new SqlParameter("@Status", t.Status),
				new SqlParameter("@MilestoneId", t.Milestone.Id),
				new SqlParameter("@Priority", t.Priority.Id)
			);
		}
		
		public int CountAllIssues()
		{
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM Issue
WHERE Status != @Status"
			);
			int count = (int)ExecuteScalar(query, "invoicing", new SqlParameter("@Status", Issue.DELETED));
			return count;
		}
		
		public override Issue Read(int id)
		{
			string query = string.Format(
				@"
SELECT Id, Title, Description, Status, MilestoneId, ISNULL(Priority, 3)
FROM Issue
WHERE Id = @Id
AND Status = @Status"
			);
			Issue issue = null;
			using (SqlDataReader rs = ExecuteReader(
				query, 
				"invoicing", 
				new SqlParameter("@Id", id), 
				new SqlParameter("@Status", Issue.DELETED))) {
				while (rs.Read()) {
					issue = new Issue {
						Id = GetInt32(rs, 0),
						Title = GetString(rs, 1),
						Description = GetString(rs, 2),
						Status = GetInt32(rs, 3),
						Milestone = new Milestone { Id = GetInt32(rs, 4) },
						Priority = new Priority { Id = GetInt32(rs, 5) }
					};
				}
			}
			if (issue != null) {
				issue.Comments = FindComments(id);
			}
			return issue;
		}

		public IList<IssueComment> FindComments(int id)
		{
			string query = string.Format(
				@"
SELECT c.Comments,
	c.Date,
	c.UserId,
	u.Name
FROM IssueComment c
INNER JOIN [User] u ON u.Id = c.UserId
WHERE c.IssueId = @Id"
			);
			var comments = new List<IssueComment>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				while (rs.Read()) {
					comments.Add(
						new IssueComment
						{
							Comments = GetString(rs, 0),
							Date = GetDateTime(rs, 1),
							User = new User { Id = GetInt32(rs, 2), Name = GetString(rs, 3) }
						}
					);
				}
			}
			return comments;
		}
		
		public override IList<Issue> FindAll()
		{
			string query = string.Format(
				@"
SELECT i.Id,
	i.Title,
	i.Description,
	i.Status,
    i.MilestoneId,
    m.Name,
    ISNULL(i.Priority, 3) Priority
FROM Issue i
INNER JOIN Milestone m ON m.Id = ISNULL(i.MilestoneId, 1)
WHERE i.Status != @Status
ORDER BY i.Status, Priority, i.CreatedAt DESC"
			);
			var issues = new List<Issue>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Status", Issue.DELETED))) {
				while (rs.Read()) {
					issues.Add(
						new Issue {
							Id = GetInt32(rs, 0),
							Title = GetString(rs, 1),
							Description = GetString(rs, 2),
							Status = GetInt32(rs, 3),
							Milestone = new Milestone {
								Id = GetInt32(rs, 4),
								Name = GetString(rs, 5)
							},
							Priority = new Priority {
								Id = GetInt32(rs, 6)
							}
						}
					);
				}
			}
			return issues;
		}
		
		public IList<Issue> FindByOffset(int offset, int limit)
		{
			string query = string.Format(
				@"
WITH Results_CTE AS (
    SELECT i.Id,
		i.Title,
		i.Description,
		i.Status,
	    i.MilestoneId,
	    m.Name,
	    ISNULL(i.Priority, 3) Priority,
        --ROW_NUMBER() OVER (ORDER BY i.Id DESC) AS RowNum
        ROW_NUMBER() OVER (ORDER BY i.Status, Priority, i.CreatedAt DESC) AS RowNum
    FROM Issue i
    INNER JOIN Milestone m ON m.Id = i.MilestoneId
    WHERE i.Status != @Deleted
	--ORDER BY i.Status, Priority, i.CreatedAt DESC
)
SELECT *
FROM Results_CTE
WHERE RowNum >= @Offset
AND RowNum < @Offset + @Limit"
			);
			var issues = new List<Issue>();
			using (SqlDataReader rs = ExecuteReader(
				query,
				"invoicing",
				new SqlParameter("@Offset", offset),
				new SqlParameter("@Limit", limit),
				new SqlParameter("@Deleted", Issue.DELETED))) {
				while (rs.Read()) {
					issues.Add(
						new Issue {
							Id = GetInt32(rs, 0),
							Title = GetString(rs, 1),
							Description = GetString(rs, 2),
							Status = GetInt32(rs, 3),
							Milestone = new Milestone {
								Id = GetInt32(rs, 4),
								Name = GetString(rs, 5)
							},
							Priority = new Priority {
								Id = GetInt32(rs, 6)
							}
						}
					);
				}
			}
			return issues;
		}
	}
}

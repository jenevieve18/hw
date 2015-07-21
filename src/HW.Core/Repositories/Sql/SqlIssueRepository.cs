using System;
using HW.Core.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HW.Core.Repositories.Sql
{
	public class SqlIssueRepository : BaseSqlRepository<Issue>
	{
        public override void Update(Issue t, int id)
        {
            string query = @"
UPDATE Issue SET Title = @Title, Description = @Description, Status = @Status
WHERE IssueID = @IssueID";
            ExecuteNonQuery(
                query,
                "SqlConnection",
                new SqlParameter("@Title", t.Title),
                new SqlParameter("@Description", t.Description),
                new SqlParameter("@IssueID", id),
                new SqlParameter("@Status", t.Status)
            );
        }

        public override Issue Read(int id)
        {
            string query = @"
SELECT IssueID, Title, Description, Status
FROM Issue
WHERE IssueID = @IssueID";
            Issue i = null;
            using (var rs = ExecuteReader(query, "SqlConnection", new SqlParameter("@IssueID", id)))
            {
                if (rs.Read())
                {
                    i = new Issue
                    {
                        Id = GetInt32(rs, 0),
                        Title = GetString(rs, 1),
                        Description = GetString(rs, 2),
                        Status = GetInt32(rs, 3)
                    };
                }
            }
            return i;
        }

        public override void Delete(int id)
        {
            string query = @"
DELETE FROM Issue WHERE IssueID = @IssueID";
            ExecuteNonQuery(query, "SqlConnection", new SqlParameter("@IssueID", id));
        }

        public override void Save(Issue t)
        {
            string query = @"
INSERT INTO Issue(IssueDate, Title, Description, UserID)
VALUES(GETDATE(), @Title, @Description, @UserID)";
            ExecuteNonQuery(
                query,
                "SqlConnection",
                new SqlParameter("@Title", t.Title),
                new SqlParameter("@Description", t.Title),
                new SqlParameter("@UserID", null)
                );
        }

        public override IList<Issue> FindAll()
        {
            string query = @"
SELECT i.Title, i.Description, i.IssueDate, u.Email, i.IssueID, i.Status
FROM Issue i
LEFT OUTER JOIN [User] u ON i.UserID = u.UserID
ORDER BY i.IssueDate DESC";
            var i = new List<Issue>();
            using (var rs = ExecuteReader(query, "SqlConnection"))
            {
                while (rs.Read())
                {
                    i.Add(new Issue
                    {
                        Title = GetString(rs, 0),
                        Description = GetString(rs, 1),
                        Date = GetDateTime(rs, 2),
                        User = new User { Email = GetString(rs, 3) },
                        Id = GetInt32(rs, 4),
                        Status = GetInt32(rs, 5)
                    });
                }
            }
            return i;
        }
	}
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Repositories.Sql
{
	public class SqlMilestoneRepository : BaseSqlRepository<Milestone>
	{
        public override Milestone Read(int id)
        {
            string query = string.Format(
                @"
SELECT Id,
Name
FROM Milestone
WHERE Id = @Id"
            );
            Milestone m = null;
            using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id)))
            {
                if (rs.Read())
                {
                    m = new Milestone
                    {
                        Id = GetInt32(rs, 0),
                        Name = GetString(rs, 1)
                    };
                }
            }
            return m;
        }

		public override void Save(Milestone t)
		{
			string query = string.Format(
				@"
INSERT INTO Milestone(Name)
VALUES(@Name)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", t.Name)
			);
		}
		
		public override void Update(Milestone t, int id)
		{
			string query = string.Format(
				@"
UPDATE Milestone SET Name = @Name
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", t.Name),
				new SqlParameter("@Id", id)
			);
		}
		
		public override IList<Milestone> FindAll()
		{
			string query = string.Format(
				@"
SELECT Id,
Name
FROM Milestone
ORDER BY Name DESC"
			);
			var m = new List<Milestone>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing")) {
				while (rs.Read()) {
					m.Add(
						new Milestone {
							Id = GetInt32(rs, 0),
							Name = GetString(rs, 1)
						}
					);
				}
			}
			return m;
		}
	}
}

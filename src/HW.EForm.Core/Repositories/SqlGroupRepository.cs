using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlGroupRepository : BaseSqlRepository<Group>
	{
		public SqlGroupRepository()
		{
		}
		
		public override void Save(Group group)
		{
			string query = @"
INSERT INTO Group(
	GroupID, 
	GroupDesc
)
VALUES(
	@GroupID, 
	@GroupDesc
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@GroupID", group.GroupID),
				new SqlParameter("@GroupDesc", group.GroupDesc)
			);
		}
		
		public override void Update(Group group, int id)
		{
			string query = @"
UPDATE Group SET
	GroupID = @GroupID,
	GroupDesc = @GroupDesc
WHERE GroupID = @GroupID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@GroupID", group.GroupID),
				new SqlParameter("@GroupDesc", group.GroupDesc)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Group
WHERE GroupID = @GroupID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@GroupID", id)
			);
		}
		
		public override Group Read(int id)
		{
			string query = @"
SELECT 	GroupID, 
	GroupDesc
FROM Group
WHERE GroupID = @GroupID";
			Group group = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@GroupID", id))) {
				if (rs.Read()) {
					group = new Group {
						GroupID = GetInt32(rs, 0),
						GroupDesc = GetString(rs, 1)
					};
				}
			}
			return group;
		}
		
		public override IList<Group> FindAll()
		{
			string query = @"
SELECT 	GroupID, 
	GroupDesc
FROM Group";
			var groups = new List<Group>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					groups.Add(new Group {
						GroupID = GetInt32(rs, 0),
						GroupDesc = GetString(rs, 1)
					});
				}
			}
			return groups;
		}
	}
}

using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSuperSponsorRepository : BaseSqlRepository<SuperSponsor>
	{
		public SqlSuperSponsorRepository()
		{
		}
		
		public override void Save(SuperSponsor superSponsor)
		{
			string query = @"
INSERT INTO SuperSponsor(
	SuperSponsorID, 
	SuperSponsor, 
	Logo, 
	Comment
)
VALUES(
	@SuperSponsorID, 
	@SuperSponsor, 
	@Logo, 
	@Comment
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SuperSponsorID", superSponsor.SuperSponsorID),
				new SqlParameter("@SuperSponsor", superSponsor.SuperSponsorName),
				new SqlParameter("@Logo", superSponsor.Logo),
				new SqlParameter("@Comment", superSponsor.Comment)
			);
		}
		
		public override void Update(SuperSponsor superSponsor, int id)
		{
			string query = @"
UPDATE SuperSponsor SET
	SuperSponsorID = @SuperSponsorID,
	SuperSponsor = @SuperSponsor,
	Logo = @Logo,
	Comment = @Comment
WHERE SuperSponsorID = @SuperSponsorID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SuperSponsorID", superSponsor.SuperSponsorID),
				new SqlParameter("@SuperSponsor", superSponsor.SuperSponsorName),
				new SqlParameter("@Logo", superSponsor.Logo),
				new SqlParameter("@Comment", superSponsor.Comment)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SuperSponsor
WHERE SuperSponsorID = @SuperSponsorID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SuperSponsorID", id)
			);
		}
		
		public override SuperSponsor Read(int id)
		{
			string query = @"
SELECT 	SuperSponsorID, 
	SuperSponsor, 
	Logo, 
	Comment
FROM SuperSponsor
WHERE SuperSponsorID = @SuperSponsorID";
			SuperSponsor superSponsor = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SuperSponsorID", id))) {
				if (rs.Read()) {
					superSponsor = new SuperSponsor {
						SuperSponsorID = GetInt32(rs, 0),
						SuperSponsorName = GetString(rs, 1),
						Logo = GetInt32(rs, 2),
						Comment = GetString(rs, 3)
					};
				}
			}
			return superSponsor;
		}
		
		public override IList<SuperSponsor> FindAll()
		{
			string query = @"
SELECT 	SuperSponsorID, 
	SuperSponsor, 
	Logo, 
	Comment
FROM SuperSponsor";
			var superSponsors = new List<SuperSponsor>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					superSponsors.Add(new SuperSponsor {
						SuperSponsorID = GetInt32(rs, 0),
						SuperSponsorName = GetString(rs, 1),
						Logo = GetInt32(rs, 2),
						Comment = GetString(rs, 3)
					});
				}
			}
			return superSponsors;
		}
	}
}

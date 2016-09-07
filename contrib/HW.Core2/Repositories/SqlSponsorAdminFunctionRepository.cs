using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorAdminFunctionRepository : BaseSqlRepository<SponsorAdminFunction>
	{
		public SqlSponsorAdminFunctionRepository()
		{
		}
		
		public override void Save(SponsorAdminFunction sponsorAdminFunction)
		{
			string query = @"
INSERT INTO SponsorAdminFunction(
	SponsorAdminFunctionID, 
	ManagerFunctionID, 
	SponsorAdminID
)
VALUES(
	@SponsorAdminFunctionID, 
	@ManagerFunctionID, 
	@SponsorAdminID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminFunctionID", sponsorAdminFunction.SponsorAdminFunctionID),
				new SqlParameter("@ManagerFunctionID", sponsorAdminFunction.ManagerFunctionID),
				new SqlParameter("@SponsorAdminID", sponsorAdminFunction.SponsorAdminID)
			);
		}
		
		public override void Update(SponsorAdminFunction sponsorAdminFunction, int id)
		{
			string query = @"
UPDATE SponsorAdminFunction SET
	SponsorAdminFunctionID = @SponsorAdminFunctionID,
	ManagerFunctionID = @ManagerFunctionID,
	SponsorAdminID = @SponsorAdminID
WHERE SponsorAdminFunctionID = @SponsorAdminFunctionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminFunctionID", sponsorAdminFunction.SponsorAdminFunctionID),
				new SqlParameter("@ManagerFunctionID", sponsorAdminFunction.ManagerFunctionID),
				new SqlParameter("@SponsorAdminID", sponsorAdminFunction.SponsorAdminID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorAdminFunction
WHERE SponsorAdminFunctionID = @SponsorAdminFunctionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminFunctionID", id)
			);
		}
		
		public override SponsorAdminFunction Read(int id)
		{
			string query = @"
SELECT 	SponsorAdminFunctionID, 
	ManagerFunctionID, 
	SponsorAdminID
FROM SponsorAdminFunction
WHERE SponsorAdminFunctionID = @SponsorAdminFunctionID";
			SponsorAdminFunction sponsorAdminFunction = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorAdminFunctionID", id))) {
				if (rs.Read()) {
					sponsorAdminFunction = new SponsorAdminFunction {
						SponsorAdminFunctionID = GetInt32(rs, 0),
						ManagerFunctionID = GetInt32(rs, 1),
						SponsorAdminID = GetInt32(rs, 2)
					};
				}
			}
			return sponsorAdminFunction;
		}
		
		public override IList<SponsorAdminFunction> FindAll()
		{
			string query = @"
SELECT 	SponsorAdminFunctionID, 
	ManagerFunctionID, 
	SponsorAdminID
FROM SponsorAdminFunction";
			var sponsorAdminFunctions = new List<SponsorAdminFunction>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorAdminFunctions.Add(new SponsorAdminFunction {
						SponsorAdminFunctionID = GetInt32(rs, 0),
						ManagerFunctionID = GetInt32(rs, 1),
						SponsorAdminID = GetInt32(rs, 2)
					});
				}
			}
			return sponsorAdminFunctions;
		}
	}
}

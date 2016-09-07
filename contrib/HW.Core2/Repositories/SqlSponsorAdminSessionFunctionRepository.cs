using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorAdminSessionFunctionRepository : BaseSqlRepository<SponsorAdminSessionFunction>
	{
		public SqlSponsorAdminSessionFunctionRepository()
		{
		}
		
		public override void Save(SponsorAdminSessionFunction sponsorAdminSessionFunction)
		{
			string query = @"
INSERT INTO SponsorAdminSessionFunction(
	SponsorAdminSessionFunctionID, 
	ManagerFunctionID, 
	DT, 
	SponsorAdminSessionID
)
VALUES(
	@SponsorAdminSessionFunctionID, 
	@ManagerFunctionID, 
	@DT, 
	@SponsorAdminSessionID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminSessionFunctionID", sponsorAdminSessionFunction.SponsorAdminSessionFunctionID),
				new SqlParameter("@ManagerFunctionID", sponsorAdminSessionFunction.ManagerFunctionID),
				new SqlParameter("@DT", sponsorAdminSessionFunction.DT),
				new SqlParameter("@SponsorAdminSessionID", sponsorAdminSessionFunction.SponsorAdminSessionID)
			);
		}
		
		public override void Update(SponsorAdminSessionFunction sponsorAdminSessionFunction, int id)
		{
			string query = @"
UPDATE SponsorAdminSessionFunction SET
	SponsorAdminSessionFunctionID = @SponsorAdminSessionFunctionID,
	ManagerFunctionID = @ManagerFunctionID,
	DT = @DT,
	SponsorAdminSessionID = @SponsorAdminSessionID
WHERE SponsorAdminSessionFunctionID = @SponsorAdminSessionFunctionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminSessionFunctionID", sponsorAdminSessionFunction.SponsorAdminSessionFunctionID),
				new SqlParameter("@ManagerFunctionID", sponsorAdminSessionFunction.ManagerFunctionID),
				new SqlParameter("@DT", sponsorAdminSessionFunction.DT),
				new SqlParameter("@SponsorAdminSessionID", sponsorAdminSessionFunction.SponsorAdminSessionID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorAdminSessionFunction
WHERE SponsorAdminSessionFunctionID = @SponsorAdminSessionFunctionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminSessionFunctionID", id)
			);
		}
		
		public override SponsorAdminSessionFunction Read(int id)
		{
			string query = @"
SELECT 	SponsorAdminSessionFunctionID, 
	ManagerFunctionID, 
	DT, 
	SponsorAdminSessionID
FROM SponsorAdminSessionFunction
WHERE SponsorAdminSessionFunctionID = @SponsorAdminSessionFunctionID";
			SponsorAdminSessionFunction sponsorAdminSessionFunction = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorAdminSessionFunctionID", id))) {
				if (rs.Read()) {
					sponsorAdminSessionFunction = new SponsorAdminSessionFunction {
						SponsorAdminSessionFunctionID = GetInt32(rs, 0),
						ManagerFunctionID = GetInt32(rs, 1),
						DT = GetString(rs, 2),
						SponsorAdminSessionID = GetInt32(rs, 3)
					};
				}
			}
			return sponsorAdminSessionFunction;
		}
		
		public override IList<SponsorAdminSessionFunction> FindAll()
		{
			string query = @"
SELECT 	SponsorAdminSessionFunctionID, 
	ManagerFunctionID, 
	DT, 
	SponsorAdminSessionID
FROM SponsorAdminSessionFunction";
			var sponsorAdminSessionFunctions = new List<SponsorAdminSessionFunction>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorAdminSessionFunctions.Add(new SponsorAdminSessionFunction {
						SponsorAdminSessionFunctionID = GetInt32(rs, 0),
						ManagerFunctionID = GetInt32(rs, 1),
						DT = GetString(rs, 2),
						SponsorAdminSessionID = GetInt32(rs, 3)
					});
				}
			}
			return sponsorAdminSessionFunctions;
		}
	}
}

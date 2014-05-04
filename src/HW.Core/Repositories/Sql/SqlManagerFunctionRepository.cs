using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlManagerFunctionRepository : BaseSqlRepository<ManagerFunction>//, IManagerFunctionRepository
	{
		public ManagerFunction ReadFirstFunctionBySponsorAdmin(int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT TOP (1) mf.ManagerFunction,
	mf.URL,
	mf.Expl
FROM ManagerFunction AS mf {0}",
				sponsorAdminID != -1 ? "INNER JOIN SponsorAdminFunction AS s ON s.ManagerFunctionID = mf.ManagerFunctionID WHERE s.SponsorAdminID = " + sponsorAdminID : ""
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var f = new ManagerFunction();
					f.Function = rs.GetString(0);
					f.URL = rs.GetString(1);
					f.Expl = rs.GetString(2);
					return f;
				}
			}
			return null;
		}
		
		public override IList<ManagerFunction> FindAll()
		{
			string query = string.Format(
				@"
SELECT ManagerFunctionID, ManagerFunction, Expl FROM ManagerFunction"
			);
			var functions = new List<ManagerFunction>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var f = new ManagerFunction {
						Id = rs.GetInt32(0),
						Function = rs.GetString(1),
						Expl = rs.GetString(2)
					};
					functions.Add(f);
				}
			}
			return functions;
		}
		
		public IList<ManagerFunction> FindBySponsorAdmin(int sponsorAdminID)
		{
			string q = sponsorAdminID != -1 ?
				string.Format(@"
INNER JOIN SponsorAdminFunction saf ON saf.ManagerFunctionID = mf.ManagerFunctionID
WHERE saf.SponsorAdminID = {0}", sponsorAdminID)
				: "";
			string query = string.Format(
				@"
SELECT mf.ManagerFunction,
mf.URL,
mf.Expl
FROM ManagerFunction mf
{0}
ORDER BY mf.ManagerFunctionID",
				q
			);
//			string query = string.Format(
//				@"
//SELECT mf.ManagerFunction,
//mf.URL,
//mf.Expl
//FROM SponsorAdminFunction saf
//INNER JOIN ManagerFunction mf ON saf.ManagerFunctionID = mf.ManagerFunctionID
//WHERE saf.SponsorAdminID = {0}
//ORDER BY mf.ManagerFunctionID",
//				sponsorAdminID
//			);
			var functions = new List<ManagerFunction>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var f = new ManagerFunction {
						Function = rs.GetString(0),
						URL = rs.GetString(1),
						Expl = rs.GetString(2)
					};
					functions.Add(f);
				}
			}
			return functions;
		}
	}
}

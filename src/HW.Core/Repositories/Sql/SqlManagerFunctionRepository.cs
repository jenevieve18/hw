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
SELECT ManagerFunctionID,
	ManagerFunction,
	Expl,
	URL
FROM ManagerFunction"
			);
			var functions = new List<ManagerFunction>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var f = new ManagerFunction {
						Id = GetInt32(rs, 0),
						Function = GetString(rs, 1),
						Expl = GetString(rs, 2),
						URL = GetString(rs, 3)
					};
					functions.Add(f);
				}
			}
			return functions;
		}
		
//		public IList<SponsorAdminFunction> FindBySponsorAdmin2(int sponsorAdminID)
//		{
//			string q = sponsorAdminID != -1 ?
//				string.Format(@"
//INNER JOIN SponsorAdminFunction saf ON saf.ManagerFunctionID = mf.ManagerFunctionID
//WHERE saf.SponsorAdminID = {0}", sponsorAdminID)
//				: "";
//			string query = string.Format(
//				@"
//SELECT mf.ManagerFunction,
//	mf.URL,
//	mf.Expl
//FROM ManagerFunction mf
//{0}
//ORDER BY mf.ManagerFunctionID",
//				q
//			);
//			var functions = new List<SponsorAdminFunction>();
//			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
//				while (rs.Read()) {
//					var f = new SponsorAdminFunction {
//						Function = new ManagerFunction {
//							Function = GetString(rs, 0),
//							URL = GetString(rs, 1),
//							Expl = GetString(rs, 2)
//						}
//					};
//					functions.Add(f);
//				}
//			}
//			return functions;
//		}
		
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
			var functions = new List<ManagerFunction>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var f = new ManagerFunction {
						Function = GetString(rs, 0),
						URL = GetString(rs, 1),
						Expl = GetString(rs, 2)
					};
					functions.Add(f);
				}
			}
			return functions;
		}
		
		public IList<ManagerFunctionLang> FindBySponsorAdmin(int sponsorAdminID, int langID)
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
FROM ManagerFunctionLang mf
{0}
{1}
ORDER BY mf.ManagerFunctionID",
				q,
				sponsorAdminID != -1 ?
				string.Format("AND mf.LangID = {0}", langID)
				: string.Format("WHERE mf.LangID = {0}", langID)
			);
			var functions = new List<ManagerFunctionLang>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var f = new ManagerFunctionLang {
						Function = GetString(rs, 0),
						URL = GetString(rs, 1),
						Expl = GetString(rs, 2)
					};
					functions.Add(f);
				}
			}
			return functions;
		}
	}
}

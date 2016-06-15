using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlManagerFunctionRepository : BaseSqlRepository<ManagerFunction>, IManagerFunctionRepository
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
//					f.Function = rs.GetString(0);
					f.URL = rs.GetString(1);
//					f.Expl = rs.GetString(2);
					return f;
				}
			}
			return null;
		}

        public override void Update(ManagerFunction t, int id)
        {
            string query = @"
UPDATE dbo.ManagerFunction SET URL = @URL
WHERE ManagerFunctionID = @ManagerFunctionID";
            ExecuteNonQuery(query, "SqlConnection", new SqlParameter("@URL", t.URL), new SqlParameter("@ManagerFunctionID", id));
        }
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM dbo.ManagerFunction WHERE ManagerFunctionID = @ManagerFunctionID";
            ExecuteNonQuery(query, "SqlConnection", new SqlParameter("@ManagerFunctionID", id));
            query = @"
DELETE FROM dbo.ManagerFunctionLang WHERE ManagerFunctionID = @ManagerFunctionID";
            ExecuteNonQuery(query, "SqlConnection", new SqlParameter("@ManagerFunctionID", id));
		}

        public int SaveManagerFunction(ManagerFunction f)
        {
            string query = @"
INSERT INTO dbo.ManagerFunction(URL)
VALUES(@URL)";
            ExecuteNonQuery(query, "SqlConnection", new SqlParameter("@URL", f.URL));
            int id = 0;
            using (var rs = ExecuteReader("SELECT TOP 1 ManagerFunctionID FROM dbo.ManagerFunction ORDER BY ManagerFunctionID DESC"))
            {
                if (rs.Read())
                {
                    id = GetInt32(rs, 0);
                }
            }
            CloseConnection();
            return id;
        }

        public void SaveManagerFunctionLanguage(ManagerFunctionLang f)
        {
            string query = @"
INSERT INTO dbo.ManagerFunctionLang(ManagerFunctionID, ManagerFunction, URL, Expl, LangID)
VALUES(@ManagerFunctionID, @ManagerFunction, @URL, @Expl, @LangID)";
            ExecuteNonQuery(
                query,
                "SqlConnection",
                new SqlParameter("@ManagerFunctionID", f.ManagerFunction.Id),
                new SqlParameter("@ManagerFunction", f.Function),
                new SqlParameter("@URL", f.URL),
                new SqlParameter("@Expl", f.Expl),
                new SqlParameter("@LangID", f.Language.Id)
            );
        }

        public void UpdateManagerFunctionLanguage(ManagerFunctionLang f, int id)
        {
            string query = @"
UPDATE dbo.ManagerFunctionLang SET ManagerFunction = @ManagerFunction,
URL = @URL,
Expl = @Expl
WHERE ManagerFunctionLangID = @ManagerFunctionLangID";
            ExecuteNonQuery(
                query,
                "SqlConnection",
                new SqlParameter("@ManagerFunction", f.Function),
                new SqlParameter("@URL", f.URL),
                new SqlParameter("@Expl", f.Expl),
                new SqlParameter("@ManagerFunctionLangID", id)
            );
        }

        public ManagerFunctionLang ReadManagerFunctionLanguage(int functionID, int langID)
        {
            string query = @"
SELECT ManagerFunctionLangID,
ManagerFunctionID,
ManagerFunction,
URL,
Expl,
LangID
FROM dbo.ManagerFunctionLang
WHERE ManagerFunctionID = @ManagerFunctionID
AND LangID = @LangID";
            ManagerFunctionLang l = null;
            using (var rs = ExecuteReader(query, "SqlConnection", new SqlParameter("@ManagerFunctionID", functionID), new SqlParameter("@LangID", langID)))
            {
                if (rs.Read())
                {
                    l = new ManagerFunctionLang
                    {
                        Id = GetInt32(rs, 0),
                        ManagerFunction = new ManagerFunction { Id = GetInt32(rs, 1) },
                        Function = GetString(rs, 2),
                        URL = GetString(rs, 3),
                        Expl = GetString(rs, 4),
                        Language = new Language { Id = GetInt32(rs, 5) },
                    };
                }
            }
            CloseConnection();
            return l;
        }

        public override ManagerFunction Read(int id)
        {
            string query = @"
SELECT ManagerFunctionID,
ManagerFunction,
URL,
Expl
FROM ManagerFunction
WHERE ManagerFunctionID = @ManagerFunctionID";
            ManagerFunction f = null;
            using (var rs = ExecuteReader(query, "SqlConnection", new SqlParameter("@ManagerFunctionID", id)))
            {
                if (rs.Read())
                {
                    f = new ManagerFunction
                    {
                        Id = GetInt32(rs, 0),
                        URL = GetString(rs, 2)
                    };
                }
            }
            CloseConnection();
            if (f != null)
            {
                f.Languages = GetLanguages(id);
            }
            return f;
        }

        List<ManagerFunctionLang> GetLanguages(int functionID)
        {
            string query = @"
SELECT ManagerFunctionLangID,
ManagerFunctionID,
ManagerFunction,
URL,
Expl,
LangID
FROM dbo.ManagerFunctionLang
WHERE ManagerFunctionID = @ManagerFunctionID";
            var l = new List<ManagerFunctionLang>();
            using (var rs = ExecuteReader(query, "SqlConnection", new SqlParameter("@ManagerFunctionID", functionID)))
            {
                while (rs.Read())
                {
                    l.Add(new ManagerFunctionLang
                    {
                        Id = GetInt32(rs, 0),
                        ManagerFunction = new ManagerFunction { Id = GetInt32(rs, 1) },
                        Function = GetString(rs, 2),
                        URL = GetString(rs, 3),
                        Expl = GetString(rs, 4),
                        Language = new Language { Id = GetInt32(rs, 5) },
                    });
                }
            }
            CloseConnection();
            return l;
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
//						Function = GetString(rs, 1),
//						Expl = GetString(rs, 2),
						URL = GetString(rs, 3)
					};
					functions.Add(f);
				}
			}
			return functions;
		}

		public IList<ManagerFunctionLang> FindAll(int langID)
		{
			string query = string.Format(
				@"
SELECT ManagerFunctionID,
	ManagerFunction,
	Expl,
	URL
FROM ManagerFunctionLang
WHERE LangID = {0}",
				langID
			);
			var functions = new List<ManagerFunctionLang>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var f = new ManagerFunctionLang {
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
		
//		public IList<ManagerFunction> FindBySponsorAdmin(int sponsorAdminID)
//		{
//			string q = sponsorAdminID != -1 ?
//				string.Format(@"
		//INNER JOIN SponsorAdminFunction saf ON saf.ManagerFunctionID = mf.ManagerFunctionID
		//WHERE saf.SponsorAdminID = {0}", sponsorAdminID)
//				: "";
//			string query = string.Format(
//				@"
		//SELECT mf.ManagerFunction,
		//mf.URL,
		//mf.Expl
		//FROM ManagerFunction mf
		//{0}
		//ORDER BY mf.ManagerFunctionID",
//				q
//			);
//			var functions = new List<ManagerFunction>();
//			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
//				while (rs.Read()) {
//					var f = new ManagerFunction {
//						Function = GetString(rs, 0),
//						URL = GetString(rs, 1),
//						Expl = GetString(rs, 2)
//					};
//					functions.Add(f);
//				}
//			}
//			return functions;
//		}
		
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
				sponsorAdminID != -1 ? string.Format("AND mf.LangID = {0}", langID) : string.Format("WHERE mf.LangID = {0}", langID)
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
		
		public IList<ManagerFunction> FindBySponsorAdminX(int sponsorAdminID, int langID)
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
				sponsorAdminID != -1 ? string.Format("AND mf.LangID = {0}", langID) : string.Format("WHERE mf.LangID = {0}", langID)
			);
			var functions = new List<ManagerFunction>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var f = new ManagerFunction {
						Languages = new [] {
							new ManagerFunctionLang {
								Function = GetString(rs, 0),
								URL = GetString(rs, 1),
								Expl = GetString(rs, 2),
							}
						}
					};
					functions.Add(f);
				}
			}
			return functions;
		}
	}
}

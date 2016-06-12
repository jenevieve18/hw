using System;
using System.Collections.Generic;
using HW.Core.Models;
using System.Data.SqlClient;

namespace HW.Core.Repositories.Sql
{
	public class SqlWiseRepository : BaseSqlRepository<Wise>
    {
        public override void Delete(int id)
        {
            string query = @"
DELETE FROM Wise WHERE WiseID = @WiseID";
            ExecuteNonQuery(query, "SqlConnection", new SqlParameter("@WiseID", id));
            query = @"
DELETE FROM WiseLang WHERE WiseID = @WiseID";
            ExecuteNonQuery(query, "SqlConnection", new SqlParameter("@WiseID", id));
        }

        public int SaveWise(Wise w)
        {
            string query = @"
INSERT INTO Wise(LastShown)
VALUES(NULL)";
            ExecuteNonQuery(query, "SqlConnection");
            int id = 0;
            using (var rs = ExecuteReader("SELECT TOP 1 WiseID FROM Wise ORDER BY WiseID DESC"))
            {
                if (rs.Read())
                {
                    id = GetInt32(rs, 0);
                }
            }
            CloseConnection();
            return id;
        }

        public void SaveWiseLanguage(WiseLanguage w)
        {
            string query = @"
INSERT INTO WiseLang(WiseID, LangID, Wise, WiseBy)
VALUES(@WiseID, @LangID, @Wise, @WiseBy)";
            ExecuteNonQuery(
                query,
                "SqlConnection",
                new SqlParameter("@WiseID", w.Wise.Id),
                new SqlParameter("@LangID", w.Language.Id),
                new SqlParameter("@Wise", w.WiseName),
                new SqlParameter("@WiseBy", w.WiseBy)
            );
        }

        public void UpdateWiseLanguage(WiseLanguage w, int id)
        {
            string query = @"
UPDATE WiseLang SET Wise = @Wise, WiseBy = @WiseBy
WHERE WiseLangID = @WiseLangID";
            ExecuteNonQuery(
                query,
                "SqlConnection",
                new SqlParameter("@Wise", w.WiseName),
                new SqlParameter("@WiseBy", w.WiseBy),
                new SqlParameter("@WiseLangID", id)
            );
        }

        public WiseLanguage ReadWiseLanguage(int wiseID, int langID)
        {
            string query = @"
SELECT WiseLangID, WiseID, LangID, Wise, WiseBy
FROM dbo.WiseLang
WHERE WiseID = @WiseID
AND LangID = @LangID";
            WiseLanguage l = null;
            using (var rs = ExecuteReader(query, "SqlConnection", new SqlParameter("@WiseID", wiseID), new SqlParameter("@LangID", langID)))
            {
                if (rs.Read())
                {
                    l = new WiseLanguage
                    {
                        Id = GetInt32(rs, 0),
                        Wise = new Wise { Id = GetInt32(rs, 1) },
                        Language = new Language { Id = GetInt32(rs, 2) },
                        WiseName = GetString(rs, 3),
                        WiseBy = GetString(rs, 4)
                    };
                }
            }
            CloseConnection();
            return l;
        }

        public override Wise Read(int id)
        {
            string query = @"
SELECT WiseID, LastShown
FROM Wise
WHERE WiseID = @WiseID";
            Wise w = null;
            using (var rs = ExecuteReader(query, "SqlConnection", new SqlParameter("@WiseID", id)))
            {
                if (rs.Read())
                {
                    w = new Wise
                    {
                        Id = GetInt32(rs, 0),
                        LastShown = GetDateTime(rs, 1)
                    };
                }
            }
            CloseConnection();
            if (w != null)
            {
                w.Languages = GetLanguages(id);
            }
            return w;
        }

        public override IList<Wise> FindAll()
        {
            string query = @"
SELECT WiseID, LastShown
FROM dbo.Wise";
            var w = new List<Wise>();
            using (var rs = ExecuteReader(query, "SqlConnection"))
            {
                while (rs.Read())
                {
                    w.Add(new Wise {
                        Id = GetInt32(rs, 0),
                        LastShown = GetDateTime(rs, 1)
                    });
                }
            }
            CloseConnection();
            foreach (var x in w)
            {
                x.Languages = GetLanguages(x.Id);
            }
            return w;
        }

        List<WiseLanguage> GetLanguages(int wiseID)
        {
            string query = @"
SELECT WiseLangID, WiseID, LangID, Wise, WiseBy
FROM dbo.WiseLang
WHERE WiseID = @WiseID";
            var l = new List<WiseLanguage>();
            using (var rs = ExecuteReader(query, "SqlConnection", new SqlParameter("@WiseID", wiseID)))
            {
                while (rs.Read())
                {
                    l.Add(new WiseLanguage
                    {
                        Id = GetInt32(rs, 0),
                        Wise = new Wise { Id = GetInt32(rs, 1) },
                        Language = new Language { Id = GetInt32(rs, 2) },
                        WiseName = GetString(rs, 3),
                        WiseBy = GetString(rs, 4)
                    });
                }
            }
            CloseConnection();
            return l;
        }

		public IList<WiseLanguage> FindLanguages()
		{
			string query = @"
SELECT wl.Wise, wl.WiseBy, w.LastShown
FROM Wise w
INNER JOIN WiseLang wl ON w.WiseID = wl.WiseID
ORDER BY w.LastShown DESC";
			var wise = new List<WiseLanguage>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					wise.Add(
						new WiseLanguage {
							WiseName = GetString(rs, 0),
							WiseBy = GetString(rs, 1),
							Wise = new Wise {
								LastShown = GetDateTime(rs, 2)
							}
						}
					);
				}
			}
			CloseConnection();
			return wise;
		}
	}
}

using System;
using System.Collections.Generic;
using HW.Core.Models;
using System.Data.SqlClient;

namespace HW.Core.Repositories.Sql
{
    public class SqlFAQRepository : BaseSqlRepository<FAQ>
    {
        public override IList<FAQ> FindAll()
        {
            string query = @"
SELECT FAQID, Name
FROM FAQ";
            var f = new List<FAQ>();
            using (var rs = ExecuteReader(query, "SqlConnection"))
            {
                while (rs.Read())
                {
                    f.Add(new FAQ {
                        Id = GetInt32(rs, 0),
                        Name = GetString(rs, 1)
                    });
                }
            }
            foreach (var x in f)
            {
                x.Languages = GetLanguages(x.Id);
            }
            return f;
        }

        IList<FAQLanguage> GetLanguages(int faqId)
        {
            string query = @"
SELECT FAQLangID, Question, Answer, LangID
FROM FAQLang
WHERE FAQID = @FAQID";
            var l = new List<FAQLanguage>();
            using (var rs = ExecuteReader(query, "SqlConnection", new SqlParameter("@FAQID", faqId)))
            {
                while (rs.Read())
                {
                    l.Add(new FAQLanguage
                    {
                        Id = GetInt32(rs, 0),
                        Question = GetString(rs, 1),
                        Answer = GetString(rs, 2),
                        Language = new Language { Id = GetInt32(rs, 3) }
                    });
                }
            }
            return l;
        }

        public override void Update(FAQ t, int id)
        {
            string query = @"
UPDATE FAQ SET Name = @Name
WHERE FAQID = @FAQID";
            ExecuteNonQuery(query, "SqlConnection", new SqlParameter("@Name", t.Name), new SqlParameter("@FAQID", id));
        }

        public override FAQ Read(int id)
        {
            string query = @"
SELECT FAQID, Name
FROM FAQ
WHERE FAQID = @FAQID";
            FAQ f = null;
            using (var rs = ExecuteReader(query, "SqlConnection", new SqlParameter("@FAQID", id)))
            {
                if (rs.Read())
                {
                    f = new FAQ {
                        Id = GetInt32(rs, 0),
                        Name = GetString(rs, 1)
                    };
                }
            }
            if (f != null)
            {
                f.Languages = GetLanguages(id);
            }
            return f;
        }

        public void SaveFAQLanguage(FAQLanguage f)
        {
            string query = @"
INSERT INTO FAQLang(FAQID, LangID, Question, Answer)
VALUES(@FAQID, @LangID, @Question, @Answer)";
            ExecuteNonQuery(
                query,
                "SqlConnection",
                new SqlParameter("@FAQID", f.FAQ.Id),
                new SqlParameter("@LangID", f.Language.Id),
                new SqlParameter("@Question", f.Question),
                new SqlParameter("@Answer", f.Answer)
            );
        }

        public FAQLanguage ReadFAQLanguage(int faqID, int langID)
        {
            string query = @"
SELECT FAQLangID, FAQID, LangID, Question, Answer
FROM FAQLang
WHERE FAQID = @FAQID
AND LangID = @LangID";
            FAQLanguage l = null;
            using (var rs = ExecuteReader(query, "SqlConnection", new SqlParameter("@FAQID", faqID), new SqlParameter("@LangID", langID)))
            {
                if (rs.Read())
                {
                    l = new FAQLanguage
                    {
                        Id = GetInt32(rs, 0),
                        FAQ = new FAQ { Id = GetInt32(rs, 1) },
                        Language = new Language { Id = GetInt32(rs, 2) },
                        Question = GetString(rs, 3),
                        Answer = GetString(rs, 4)
                    };
                }
            }
            return l;
        }
        
        public void UpdateFAQLanguage(FAQLanguage f, int id)
        {
            string query = @"
UPDATE FAQLang SET Question = @Question, Answer = @Answer
WHERE FAQLangID = @FAQLangID";
            ExecuteNonQuery(
                query,
                "SqlConnection",
                new SqlParameter("@Question", f.Question),
                new SqlParameter("@Answer", f.Answer),
                new SqlParameter("@FAQLangID", id)
            );
        }

        public int SaveFAQ(FAQ f)
        {
            string query = @"
INSERT INTO FAQ(Name)
VALUES(@Name)";
            ExecuteNonQuery(query, "SqlConnection", new SqlParameter("@Name", f.Name));
            int id = 0;
            using (var rs = ExecuteReader("SELECT TOP 1 FAQID FROM FAQ ORDER BY FAQID DESC"))
            {
                if (rs.Read())
                {
                    id = GetInt32(rs, 0);
                }
            }
            return id;
        }

        public IList<FAQLanguage> FindLanguages()
        {
            string query = @"
SELECT fl.Question,
    fl.Answer,
    fl.FAQLangID,
    f.FAQID
FROM FAQ f
INNER JOIN FAQLang fl ON fl.FAQID = f.FAQID";
            var faqs = new List<FAQLanguage>();
            using (var rs = ExecuteReader(query))
            {
                while (rs.Read())
                {
                    faqs.Add(
                        new FAQLanguage
                        {
                            Question = GetString(rs, 0),
                            Answer = GetString(rs, 1),
                            Id = GetInt32(rs, 2),
                            FAQ = new FAQ { Id = GetInt32(rs, 3) }
                        }
                    );
                }
            }
            return faqs;
        }
    }

	public class SqlWiseRepository : BaseSqlRepository<Wise>
	{
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
			return wise;
		}
	}
}

using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlFAQRepository : BaseSqlRepository<FAQ>
	{
		public SqlFAQRepository()
		{
		}
		
		public override void Save(FAQ fAQ)
		{
			string query = @"
INSERT INTO FAQ(
	FAQID, 
	Name
)
VALUES(
	@FAQID, 
	@Name
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FAQID", fAQ.FAQID),
				new SqlParameter("@Name", fAQ.Name)
			);
		}
		
		public override void Update(FAQ fAQ, int id)
		{
			string query = @"
UPDATE FAQ SET
	FAQID = @FAQID,
	Name = @Name
WHERE FAQID = @FAQID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FAQID", fAQ.FAQID),
				new SqlParameter("@Name", fAQ.Name)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM FAQ
WHERE FAQID = @FAQID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FAQID", id)
			);
		}
		
		public override FAQ Read(int id)
		{
			string query = @"
SELECT 	FAQID, 
	Name
FROM FAQ
WHERE FAQID = @FAQID";
			FAQ fAQ = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@FAQID", id))) {
				if (rs.Read()) {
					fAQ = new FAQ {
						FAQID = GetInt32(rs, 0),
						Name = GetString(rs, 1)
					};
				}
			}
			return fAQ;
		}
		
		public override IList<FAQ> FindAll()
		{
			string query = @"
SELECT 	FAQID, 
	Name
FROM FAQ";
			var fAQs = new List<FAQ>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					fAQs.Add(new FAQ {
						FAQID = GetInt32(rs, 0),
						Name = GetString(rs, 1)
					});
				}
			}
			return fAQs;
		}
	}
}

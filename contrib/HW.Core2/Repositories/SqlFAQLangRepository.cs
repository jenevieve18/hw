using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlFAQLangRepository : BaseSqlRepository<FAQLang>
	{
		public SqlFAQLangRepository()
		{
		}
		
		public override void Save(FAQLang fAQLang)
		{
			string query = @"
INSERT INTO FAQLang(
	FAQLangID, 
	FAQID, 
	LangID, 
	Question, 
	Answer
)
VALUES(
	@FAQLangID, 
	@FAQID, 
	@LangID, 
	@Question, 
	@Answer
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FAQLangID", fAQLang.FAQLangID),
				new SqlParameter("@FAQID", fAQLang.FAQID),
				new SqlParameter("@LangID", fAQLang.LangID),
				new SqlParameter("@Question", fAQLang.Question),
				new SqlParameter("@Answer", fAQLang.Answer)
			);
		}
		
		public override void Update(FAQLang fAQLang, int id)
		{
			string query = @"
UPDATE FAQLang SET
	FAQLangID = @FAQLangID,
	FAQID = @FAQID,
	LangID = @LangID,
	Question = @Question,
	Answer = @Answer
WHERE FAQLangID = @FAQLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FAQLangID", fAQLang.FAQLangID),
				new SqlParameter("@FAQID", fAQLang.FAQID),
				new SqlParameter("@LangID", fAQLang.LangID),
				new SqlParameter("@Question", fAQLang.Question),
				new SqlParameter("@Answer", fAQLang.Answer)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM FAQLang
WHERE FAQLangID = @FAQLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FAQLangID", id)
			);
		}
		
		public override FAQLang Read(int id)
		{
			string query = @"
SELECT 	FAQLangID, 
	FAQID, 
	LangID, 
	Question, 
	Answer
FROM FAQLang
WHERE FAQLangID = @FAQLangID";
			FAQLang fAQLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@FAQLangID", id))) {
				if (rs.Read()) {
					fAQLang = new FAQLang {
						FAQLangID = GetInt32(rs, 0),
						FAQID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Question = GetString(rs, 3),
						Answer = GetString(rs, 4)
					};
				}
			}
			return fAQLang;
		}
		
		public override IList<FAQLang> FindAll()
		{
			string query = @"
SELECT 	FAQLangID, 
	FAQID, 
	LangID, 
	Question, 
	Answer
FROM FAQLang";
			var fAQLangs = new List<FAQLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					fAQLangs.Add(new FAQLang {
						FAQLangID = GetInt32(rs, 0),
						FAQID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Question = GetString(rs, 3),
						Answer = GetString(rs, 4)
					});
				}
			}
			return fAQLangs;
		}
	}
}

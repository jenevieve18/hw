using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlSponsorLangRepository : BaseSqlRepository<SponsorLang>
	{
		public SqlSponsorLangRepository()
		{
		}
		
		public override void Save(SponsorLang sponsorLang)
		{
			string query = @"
INSERT INTO SponsorLang(
	SponsorLangID, 
	SponsorID, 
	LangID
)
VALUES(
	@SponsorLangID, 
	@SponsorID, 
	@LangID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorLangID", sponsorLang.SponsorLangID),
				new SqlParameter("@SponsorID", sponsorLang.SponsorID),
				new SqlParameter("@LangID", sponsorLang.LangID)
			);
		}
		
		public override void Update(SponsorLang sponsorLang, int id)
		{
			string query = @"
UPDATE SponsorLang SET
	SponsorLangID = @SponsorLangID,
	SponsorID = @SponsorID,
	LangID = @LangID
WHERE SponsorLangID = @SponsorLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorLangID", sponsorLang.SponsorLangID),
				new SqlParameter("@SponsorID", sponsorLang.SponsorID),
				new SqlParameter("@LangID", sponsorLang.LangID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorLang
WHERE SponsorLangID = @SponsorLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorLangID", id)
			);
		}
		
		public override SponsorLang Read(int id)
		{
			string query = @"
SELECT 	SponsorLangID, 
	SponsorID, 
	LangID
FROM SponsorLang
WHERE SponsorLangID = @SponsorLangID";
			SponsorLang sponsorLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorLangID", id))) {
				if (rs.Read()) {
					sponsorLang = new SponsorLang {
						SponsorLangID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2)
					};
				}
			}
			return sponsorLang;
		}
		
		public override IList<SponsorLang> FindAll()
		{
			string query = @"
SELECT 	SponsorLangID, 
	SponsorID, 
	LangID
FROM SponsorLang";
			var sponsorLangs = new List<SponsorLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorLangs.Add(new SponsorLang {
						SponsorLangID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2)
					});
				}
			}
			return sponsorLangs;
		}
	}
}

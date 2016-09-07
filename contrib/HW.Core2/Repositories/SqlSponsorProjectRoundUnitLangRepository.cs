using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorProjectRoundUnitLangRepository : BaseSqlRepository<SponsorProjectRoundUnitLang>
	{
		public SqlSponsorProjectRoundUnitLangRepository()
		{
		}
		
		public override void Save(SponsorProjectRoundUnitLang sponsorProjectRoundUnitLang)
		{
			string query = @"
INSERT INTO SponsorProjectRoundUnitLang(
	SponsorProjectRoundUnitLangID, 
	SponsorProjectRoundUnitID, 
	LangID, 
	Nav, 
	Feedback
)
VALUES(
	@SponsorProjectRoundUnitLangID, 
	@SponsorProjectRoundUnitID, 
	@LangID, 
	@Nav, 
	@Feedback
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorProjectRoundUnitLangID", sponsorProjectRoundUnitLang.SponsorProjectRoundUnitLangID),
				new SqlParameter("@SponsorProjectRoundUnitID", sponsorProjectRoundUnitLang.SponsorProjectRoundUnitID),
				new SqlParameter("@LangID", sponsorProjectRoundUnitLang.LangID),
				new SqlParameter("@Nav", sponsorProjectRoundUnitLang.Nav),
				new SqlParameter("@Feedback", sponsorProjectRoundUnitLang.Feedback)
			);
		}
		
		public override void Update(SponsorProjectRoundUnitLang sponsorProjectRoundUnitLang, int id)
		{
			string query = @"
UPDATE SponsorProjectRoundUnitLang SET
	SponsorProjectRoundUnitLangID = @SponsorProjectRoundUnitLangID,
	SponsorProjectRoundUnitID = @SponsorProjectRoundUnitID,
	LangID = @LangID,
	Nav = @Nav,
	Feedback = @Feedback
WHERE SponsorProjectRoundUnitLangID = @SponsorProjectRoundUnitLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorProjectRoundUnitLangID", sponsorProjectRoundUnitLang.SponsorProjectRoundUnitLangID),
				new SqlParameter("@SponsorProjectRoundUnitID", sponsorProjectRoundUnitLang.SponsorProjectRoundUnitID),
				new SqlParameter("@LangID", sponsorProjectRoundUnitLang.LangID),
				new SqlParameter("@Nav", sponsorProjectRoundUnitLang.Nav),
				new SqlParameter("@Feedback", sponsorProjectRoundUnitLang.Feedback)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorProjectRoundUnitLang
WHERE SponsorProjectRoundUnitLangID = @SponsorProjectRoundUnitLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorProjectRoundUnitLangID", id)
			);
		}
		
		public override SponsorProjectRoundUnitLang Read(int id)
		{
			string query = @"
SELECT 	SponsorProjectRoundUnitLangID, 
	SponsorProjectRoundUnitID, 
	LangID, 
	Nav, 
	Feedback
FROM SponsorProjectRoundUnitLang
WHERE SponsorProjectRoundUnitLangID = @SponsorProjectRoundUnitLangID";
			SponsorProjectRoundUnitLang sponsorProjectRoundUnitLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorProjectRoundUnitLangID", id))) {
				if (rs.Read()) {
					sponsorProjectRoundUnitLang = new SponsorProjectRoundUnitLang {
						SponsorProjectRoundUnitLangID = GetInt32(rs, 0),
						SponsorProjectRoundUnitID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Nav = GetString(rs, 3),
						Feedback = GetString(rs, 4)
					};
				}
			}
			return sponsorProjectRoundUnitLang;
		}
		
		public override IList<SponsorProjectRoundUnitLang> FindAll()
		{
			string query = @"
SELECT 	SponsorProjectRoundUnitLangID, 
	SponsorProjectRoundUnitID, 
	LangID, 
	Nav, 
	Feedback
FROM SponsorProjectRoundUnitLang";
			var sponsorProjectRoundUnitLangs = new List<SponsorProjectRoundUnitLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorProjectRoundUnitLangs.Add(new SponsorProjectRoundUnitLang {
						SponsorProjectRoundUnitLangID = GetInt32(rs, 0),
						SponsorProjectRoundUnitID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Nav = GetString(rs, 3),
						Feedback = GetString(rs, 4)
					});
				}
			}
			return sponsorProjectRoundUnitLangs;
		}
	}
}

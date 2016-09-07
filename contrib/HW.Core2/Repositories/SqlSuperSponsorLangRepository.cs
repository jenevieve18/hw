using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSuperSponsorLangRepository : BaseSqlRepository<SuperSponsorLang>
	{
		public SqlSuperSponsorLangRepository()
		{
		}
		
		public override void Save(SuperSponsorLang superSponsorLang)
		{
			string query = @"
INSERT INTO SuperSponsorLang(
	SuperSponsorLangID, 
	SuperSponsorID, 
	LangID, 
	Slogan, 
	Header
)
VALUES(
	@SuperSponsorLangID, 
	@SuperSponsorID, 
	@LangID, 
	@Slogan, 
	@Header
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SuperSponsorLangID", superSponsorLang.SuperSponsorLangID),
				new SqlParameter("@SuperSponsorID", superSponsorLang.SuperSponsorID),
				new SqlParameter("@LangID", superSponsorLang.LangID),
				new SqlParameter("@Slogan", superSponsorLang.Slogan),
				new SqlParameter("@Header", superSponsorLang.Header)
			);
		}
		
		public override void Update(SuperSponsorLang superSponsorLang, int id)
		{
			string query = @"
UPDATE SuperSponsorLang SET
	SuperSponsorLangID = @SuperSponsorLangID,
	SuperSponsorID = @SuperSponsorID,
	LangID = @LangID,
	Slogan = @Slogan,
	Header = @Header
WHERE SuperSponsorLangID = @SuperSponsorLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SuperSponsorLangID", superSponsorLang.SuperSponsorLangID),
				new SqlParameter("@SuperSponsorID", superSponsorLang.SuperSponsorID),
				new SqlParameter("@LangID", superSponsorLang.LangID),
				new SqlParameter("@Slogan", superSponsorLang.Slogan),
				new SqlParameter("@Header", superSponsorLang.Header)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SuperSponsorLang
WHERE SuperSponsorLangID = @SuperSponsorLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SuperSponsorLangID", id)
			);
		}
		
		public override SuperSponsorLang Read(int id)
		{
			string query = @"
SELECT 	SuperSponsorLangID, 
	SuperSponsorID, 
	LangID, 
	Slogan, 
	Header
FROM SuperSponsorLang
WHERE SuperSponsorLangID = @SuperSponsorLangID";
			SuperSponsorLang superSponsorLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SuperSponsorLangID", id))) {
				if (rs.Read()) {
					superSponsorLang = new SuperSponsorLang {
						SuperSponsorLangID = GetInt32(rs, 0),
						SuperSponsorID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Slogan = GetString(rs, 3),
						Header = GetString(rs, 4)
					};
				}
			}
			return superSponsorLang;
		}
		
		public override IList<SuperSponsorLang> FindAll()
		{
			string query = @"
SELECT 	SuperSponsorLangID, 
	SuperSponsorID, 
	LangID, 
	Slogan, 
	Header
FROM SuperSponsorLang";
			var superSponsorLangs = new List<SuperSponsorLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					superSponsorLangs.Add(new SuperSponsorLang {
						SuperSponsorLangID = GetInt32(rs, 0),
						SuperSponsorID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Slogan = GetString(rs, 3),
						Header = GetString(rs, 4)
					});
				}
			}
			return superSponsorLangs;
		}
	}
}

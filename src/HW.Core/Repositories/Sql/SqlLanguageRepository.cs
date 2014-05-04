using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlLanguageRepository : BaseSqlRepository<Language>//, ILanguageRepository
	{
		public override IList<Language> FindAll()
		{
			string query = @"
SELECT LangID,
Lang
FROM Lang";
			var languages = new List<Language>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					var l = new Language {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1)
					};
					languages.Add(l);
				}
			}
			return languages;
		}
		
		public IList<SponsorProjectRoundUnitLanguage> FindBySponsor(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT sprul.LangID,
	spru.ProjectRoundUnitID,
	l.LID,
	l.Language
FROM SponsorProjectRoundUnit spru
LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID
INNER JOIN LID l ON ISNULL(sprul.LangID, 1) = l.LID
WHERE spru.SponsorID = {0}
ORDER BY spru.SortOrder, spru.SponsorProjectRoundUnitID, l.LID",
				sponsorID);
			var languages = new List<SponsorProjectRoundUnitLanguage>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var l = new SponsorProjectRoundUnitLanguage {
						Language = new Language { Id = rs.GetInt32(2), Name = rs.GetString(3) },
						SponsorProjectRoundUnit = new SponsorProjectRoundUnit {
							ProjectRoundUnit = new ProjectRoundUnit { Id = rs.GetInt32(1) }
						}
					};
					languages.Add(l);
				}
			}
			return languages;
		}
	}
	
}

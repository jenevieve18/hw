using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlManagerFunctionLangRepository : BaseSqlRepository<ManagerFunctionLang>
	{
		public SqlManagerFunctionLangRepository()
		{
		}
		
		public override void Save(ManagerFunctionLang managerFunctionLang)
		{
			string query = @"
INSERT INTO ManagerFunctionLang(
	ManagerFunctionLangID, 
	ManagerFunctionID, 
	ManagerFunction, 
	URL, 
	Expl, 
	LangID
)
VALUES(
	@ManagerFunctionLangID, 
	@ManagerFunctionID, 
	@ManagerFunction, 
	@URL, 
	@Expl, 
	@LangID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ManagerFunctionLangID", managerFunctionLang.ManagerFunctionLangID),
				new SqlParameter("@ManagerFunctionID", managerFunctionLang.ManagerFunctionID),
				new SqlParameter("@ManagerFunction", managerFunctionLang.ManagerFunction),
				new SqlParameter("@URL", managerFunctionLang.URL),
				new SqlParameter("@Expl", managerFunctionLang.Expl),
				new SqlParameter("@LangID", managerFunctionLang.LangID)
			);
		}
		
		public override void Update(ManagerFunctionLang managerFunctionLang, int id)
		{
			string query = @"
UPDATE ManagerFunctionLang SET
	ManagerFunctionLangID = @ManagerFunctionLangID,
	ManagerFunctionID = @ManagerFunctionID,
	ManagerFunction = @ManagerFunction,
	URL = @URL,
	Expl = @Expl,
	LangID = @LangID
WHERE ManagerFunctionLangID = @ManagerFunctionLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ManagerFunctionLangID", managerFunctionLang.ManagerFunctionLangID),
				new SqlParameter("@ManagerFunctionID", managerFunctionLang.ManagerFunctionID),
				new SqlParameter("@ManagerFunction", managerFunctionLang.ManagerFunction),
				new SqlParameter("@URL", managerFunctionLang.URL),
				new SqlParameter("@Expl", managerFunctionLang.Expl),
				new SqlParameter("@LangID", managerFunctionLang.LangID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ManagerFunctionLang
WHERE ManagerFunctionLangID = @ManagerFunctionLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ManagerFunctionLangID", id)
			);
		}
		
		public override ManagerFunctionLang Read(int id)
		{
			string query = @"
SELECT 	ManagerFunctionLangID, 
	ManagerFunctionID, 
	ManagerFunction, 
	URL, 
	Expl, 
	LangID
FROM ManagerFunctionLang
WHERE ManagerFunctionLangID = @ManagerFunctionLangID";
			ManagerFunctionLang managerFunctionLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ManagerFunctionLangID", id))) {
				if (rs.Read()) {
					managerFunctionLang = new ManagerFunctionLang {
						ManagerFunctionLangID = GetInt32(rs, 0),
						ManagerFunctionID = GetInt32(rs, 1),
						ManagerFunction = GetString(rs, 2),
						URL = GetString(rs, 3),
						Expl = GetString(rs, 4),
						LangID = GetInt32(rs, 5)
					};
				}
			}
			return managerFunctionLang;
		}
		
		public override IList<ManagerFunctionLang> FindAll()
		{
			string query = @"
SELECT 	ManagerFunctionLangID, 
	ManagerFunctionID, 
	ManagerFunction, 
	URL, 
	Expl, 
	LangID
FROM ManagerFunctionLang";
			var managerFunctionLangs = new List<ManagerFunctionLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					managerFunctionLangs.Add(new ManagerFunctionLang {
						ManagerFunctionLangID = GetInt32(rs, 0),
						ManagerFunctionID = GetInt32(rs, 1),
						ManagerFunction = GetString(rs, 2),
						URL = GetString(rs, 3),
						Expl = GetString(rs, 4),
						LangID = GetInt32(rs, 5)
					});
				}
			}
			return managerFunctionLangs;
		}
	}
}

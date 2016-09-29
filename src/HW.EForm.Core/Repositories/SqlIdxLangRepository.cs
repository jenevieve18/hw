using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlIdxLangRepository : BaseSqlRepository<IndexLang>
	{
		public SqlIdxLangRepository()
		{
		}
		
		public override void Save(IndexLang idxLang)
		{
			string query = @"
INSERT INTO IdxLang(
	IdxLangID, 
	IdxID, 
	LangID, 
	Idx, 
	IdxJapaneseUnicode
)
VALUES(
	@IdxLangID, 
	@IdxID, 
	@LangID, 
	@Idx, 
	@IdxJapaneseUnicode
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@IdxLangID", idxLang.IdxLangID),
				new SqlParameter("@IdxID", idxLang.IdxID),
				new SqlParameter("@LangID", idxLang.LangID),
				new SqlParameter("@Idx", idxLang.Idx),
				new SqlParameter("@IdxJapaneseUnicode", idxLang.IdxJapaneseUnicode)
			);
		}
		
		public override void Update(IndexLang idxLang, int id)
		{
			string query = @"
UPDATE IdxLang SET
	IdxLangID = @IdxLangID,
	IdxID = @IdxID,
	LangID = @LangID,
	Idx = @Idx,
	IdxJapaneseUnicode = @IdxJapaneseUnicode
WHERE IdxLangID = @IdxLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@IdxLangID", idxLang.IdxLangID),
				new SqlParameter("@IdxID", idxLang.IdxID),
				new SqlParameter("@LangID", idxLang.LangID),
				new SqlParameter("@Idx", idxLang.Idx),
				new SqlParameter("@IdxJapaneseUnicode", idxLang.IdxJapaneseUnicode)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM IdxLang
WHERE IdxLangID = @IdxLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@IdxLangID", id)
			);
		}
		
		public override IndexLang Read(int id)
		{
			string query = @"
SELECT 	IdxLangID, 
	IdxID, 
	LangID, 
	Idx, 
	IdxJapaneseUnicode
FROM IdxLang
WHERE IdxLangID = @IdxLangID";
			IndexLang idxLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@IdxLangID", id))) {
				if (rs.Read()) {
					idxLang = new IndexLang {
						IdxLangID = GetInt32(rs, 0),
						IdxID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Idx = GetString(rs, 3),
						IdxJapaneseUnicode = GetString(rs, 4)
					};
				}
			}
			return idxLang;
		}
		
		public override IList<IndexLang> FindAll()
		{
			string query = @"
SELECT 	IdxLangID, 
	IdxID, 
	LangID, 
	Idx, 
	IdxJapaneseUnicode
FROM IdxLang";
			var idxLangs = new List<IndexLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					idxLangs.Add(new IndexLang {
						IdxLangID = GetInt32(rs, 0),
						IdxID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Idx = GetString(rs, 3),
						IdxJapaneseUnicode = GetString(rs, 4)
					});
				}
			}
			return idxLangs;
		}
	}
}

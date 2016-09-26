using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlIndexLangRepository : BaseSqlRepository<IndexLang>
	{
		public SqlIndexLangRepository()
		{
		}
		
		public override void Save(IndexLang indexLang)
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
				new SqlParameter("@IdxLangID", indexLang.IdxLangID),
				new SqlParameter("@IdxID", indexLang.IdxID),
				new SqlParameter("@LangID", indexLang.LangID),
				new SqlParameter("@Idx", indexLang.Idx),
				new SqlParameter("@IdxJapaneseUnicode", indexLang.IdxJapaneseUnicode)
			);
		}
		
		public override void Update(IndexLang indexLang, int id)
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
				new SqlParameter("@IdxLangID", indexLang.IdxLangID),
				new SqlParameter("@IdxID", indexLang.IdxID),
				new SqlParameter("@LangID", indexLang.LangID),
				new SqlParameter("@Idx", indexLang.Idx),
				new SqlParameter("@IdxJapaneseUnicode", indexLang.IdxJapaneseUnicode)
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
			IndexLang indexLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@IdxLangID", id))) {
				if (rs.Read()) {
					indexLang = new IndexLang {
						IdxLangID = GetInt32(rs, 0),
						IdxID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Idx = GetString(rs, 3),
						IdxJapaneseUnicode = GetString(rs, 4)
					};
				}
			}
			return indexLang;
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
			var indexLangs = new List<IndexLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					indexLangs.Add(new IndexLang {
						IdxLangID = GetInt32(rs, 0),
						IdxID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Idx = GetString(rs, 3),
						IdxJapaneseUnicode = GetString(rs, 4)
					});
				}
			}
			return indexLangs;
		}
		
		public IList<IndexLang> FindByIndex(int indexID)
		{
			string query = @"
SELECT 	IdxLangID, 
	IdxID, 
	LangID, 
	Idx, 
	IdxJapaneseUnicode
FROM IdxLang
WHERE IdxID = @IdxID";
			var indexLangs = new List<IndexLang>();
			using (var rs = ExecuteReader(query, new SqlParameter("@IdxID", indexID))) {
				while (rs.Read()) {
					indexLangs.Add(new IndexLang {
						IdxLangID = GetInt32(rs, 0),
						IdxID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Idx = GetString(rs, 3),
						IdxJapaneseUnicode = GetString(rs, 4)
					});
				}
			}
			return indexLangs;
		}
	}
}

using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public interface IOptionComponentLangRepository : IBaseRepository<OptionComponentLang>
	{
		IList<OptionComponentLang> FindByOptionComponent(int optionComponentID);
	}
	
	public class SqlOptionComponentLangRepository : BaseSqlRepository<OptionComponentLang>, IOptionComponentLangRepository
	{
		public SqlOptionComponentLangRepository()
		{
		}
		
		public override void Save(OptionComponentLang optionComponentLang)
		{
			string query = @"
INSERT INTO OptionComponentLang(
	OptionComponentLangID, 
	OptionComponentID, 
	LangID, 
	Text, 
	TextJapaneseUnicode
)
VALUES(
	@OptionComponentLangID, 
	@OptionComponentID, 
	@LangID, 
	@Text, 
	@TextJapaneseUnicode
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@OptionComponentLangID", optionComponentLang.OptionComponentLangID),
				new SqlParameter("@OptionComponentID", optionComponentLang.OptionComponentID),
				new SqlParameter("@LangID", optionComponentLang.LangID),
				new SqlParameter("@Text", optionComponentLang.Text),
				new SqlParameter("@TextJapaneseUnicode", optionComponentLang.TextJapaneseUnicode)
			);
		}
		
		public override void Update(OptionComponentLang optionComponentLang, int id)
		{
			string query = @"
UPDATE OptionComponentLang SET
	OptionComponentLangID = @OptionComponentLangID,
	OptionComponentID = @OptionComponentID,
	LangID = @LangID,
	Text = @Text,
	TextJapaneseUnicode = @TextJapaneseUnicode
WHERE OptionComponentLangID = @OptionComponentLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@OptionComponentLangID", optionComponentLang.OptionComponentLangID),
				new SqlParameter("@OptionComponentID", optionComponentLang.OptionComponentID),
				new SqlParameter("@LangID", optionComponentLang.LangID),
				new SqlParameter("@Text", optionComponentLang.Text),
				new SqlParameter("@TextJapaneseUnicode", optionComponentLang.TextJapaneseUnicode)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM OptionComponentLang
WHERE OptionComponentLangID = @OptionComponentLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@OptionComponentLangID", id)
			);
		}
		
		public override OptionComponentLang Read(int id)
		{
			string query = @"
SELECT 	OptionComponentLangID, 
	OptionComponentID, 
	LangID, 
	Text, 
	TextJapaneseUnicode
FROM OptionComponentLang
WHERE OptionComponentLangID = @OptionComponentLangID";
			OptionComponentLang optionComponentLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@OptionComponentLangID", id))) {
				if (rs.Read()) {
					optionComponentLang = new OptionComponentLang {
						OptionComponentLangID = GetInt32(rs, 0),
						OptionComponentID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Text = GetString(rs, 3),
						TextJapaneseUnicode = GetString(rs, 4)
					};
				}
			}
			return optionComponentLang;
		}
		
//		public OptionComponentLang ReadByLang(int optionComponentID, int langID)
//		{
//			string query = @"
//SELECT 	OptionComponentLangID, 
//	OptionComponentID, 
//	LangID, 
//	Text, 
//	TextJapaneseUnicode
//FROM OptionComponentLang
//WHERE OptionComponentID = @OptionComponentID
//AND LangID = @LangID";
//			OptionComponentLang optionComponentLang = null;
//			using (var rs = ExecuteReader(query, new SqlParameter("@OptionComponentID", optionComponentID), new SqlParameter("@LangID", langID))) {
//				if (rs.Read()) {
//					optionComponentLang = new OptionComponentLang {
//						OptionComponentLangID = GetInt32(rs, 0),
//						OptionComponentID = GetInt32(rs, 1),
//						LangID = GetInt32(rs, 2),
//						Text = GetString(rs, 3),
//						TextJapaneseUnicode = GetString(rs, 4)
//					};
//				}
//			}
//			return optionComponentLang;
//		}
		
		public override IList<OptionComponentLang> FindAll()
		{
			string query = @"
SELECT 	OptionComponentLangID, 
	OptionComponentID, 
	LangID, 
	Text, 
	TextJapaneseUnicode
FROM OptionComponentLang";
			var optionComponentLangs = new List<OptionComponentLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					optionComponentLangs.Add(new OptionComponentLang {
						OptionComponentLangID = GetInt32(rs, 0),
						OptionComponentID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Text = GetString(rs, 3),
						TextJapaneseUnicode = GetString(rs, 4)
					});
				}
			}
			return optionComponentLangs;
		}
		
		public IList<OptionComponentLang> FindByOptionComponent(int optionComponentID)
		{
			string query = @"
SELECT 	OptionComponentLangID, 
	OptionComponentID, 
	LangID, 
	Text, 
	TextJapaneseUnicode
FROM OptionComponentLang
WHERE OptionComponentID = @OptionComponentID";
			var optionComponentLangs = new List<OptionComponentLang>();
			using (var rs = ExecuteReader(query, new SqlParameter("@OptionComponentID", optionComponentID))) {
				while (rs.Read()) {
					optionComponentLangs.Add(new OptionComponentLang {
						OptionComponentLangID = GetInt32(rs, 0),
						OptionComponentID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Text = GetString(rs, 3),
						TextJapaneseUnicode = GetString(rs, 4)
					});
				}
			}
			return optionComponentLangs;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlOptionRepository : BaseSqlRepository<Option>, IOptionRepository
	{
		public int CountByOption(int optionID)
		{
			string query = string.Format(
				@"
SELECT COUNT(*) FROM OptionComponents WHERE OptionID = {0}",
				optionID
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
//		public IList<OptionComponentLanguage> FindComponentsByLanguage(int optionID, int langID)
		public IList<OptionComponents> FindComponentsByLanguage(int optionID, int langID)
		{
			string query = string.Format(
				@"
SELECT oc.OptionComponentID,
	ocl.Text
FROM OptionComponents ocs
INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID
INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = {1}
WHERE ocs.OptionID = {0}
ORDER BY ocs.SortOrder",
				optionID,
				langID
			);
//			var components = new List<OptionComponentLanguage>();
			var components = new List<OptionComponents>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
//					var c = new OptionComponentLanguage();
					var c = new OptionComponents();
					var o = new OptionComponent { Id = rs.GetInt32(0) };
					o.CurrentLanguage = new OptionComponentLanguage { Text = rs.GetString(1) };
					c.Component = o;
//					c.Text = rs.GetString(1);
//					c.Component = new OptionComponent { Id = rs.GetInt32(0) };
					components.Add(c);
				}
			}
			return components;
		}
	}
}

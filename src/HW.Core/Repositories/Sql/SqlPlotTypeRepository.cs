using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlPlotTypeRepository : BaseSqlRepository<PlotType>
	{
		public override IList<PlotType> FindAll()
		{
			string query = string.Format(
				@"
	select * from plottype"
			);
			var types = new List<PlotType>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var t = new PlotType() {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1),
						Description = GetString(rs, 2)
					};
					types.Add(t);
				}
			}
			return types;
		}
		
		public IList<PlotTypeLanguage> FindByLanguage(int langID)
		{
			string query = string.Format(
				@"
	select name, description from plottypelang"
			);
			var types = new List<PlotTypeLanguage>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var t = new PlotTypeLanguage() {
						Name = GetString(rs, 0),
						Description = GetString(rs, 1)
					};
					types.Add(t);
				}
			}
			return types;
		}
	}
}

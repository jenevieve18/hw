using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Repositories.Sql
{
	public class SqlNewsRepository : BaseSqlRepository<News>
	{
		public SqlNewsRepository()
		{
		}
		
		public News ReadLatest()
		{
			string query = @"
SELECT TOP 1 Id, Content
FROM News
ORDER BY Date DESC";
			News news = null;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing")) {
				while (rs.Read()) {
					news = new News {
						Id = GetInt32(rs, 0),
						Content = GetString(rs, 1)
					};
				}
			}
			return news;
		}
	}
}

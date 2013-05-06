//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlProjectRepository : BaseSqlRepository<Project>, IProjectRepository
	{
		public void UpdateProjectRoundUser(int projectRoundUnitID, int proejctRoundUserID)
		{
			string query = string.Format(
				@"
UPDATE [eform]..[ProjectRoundUser] SET ProjectRoundUnitID = {0} WHERE ProjectRoundUserID = {1}",
				projectRoundUnitID,
				proejctRoundUserID
			);
//			Db2.exec(query);
			Db.exec(query, "eFormSqlConnection");
		}
		
		public int CountForSortString(string sortString)
		{
			string query = string.Format(
				@"
SELECT COUNT(*) FROM ProjectRoundUnit pru WHERE LEFT(pru.SortString, {1}) = '{0}'",
				sortString,
				sortString.Length
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public ProjectRoundUnit ReadRoundUnit(int projectRoundUnitID)
		{
			string query = string.Format(
				@"
SELECT SortString,
	dbo.cf_unitLangID(ProjectRoundUnitID)
FROM ProjectRoundUnit
WHERE ProjectRoundUnitID = {0}",
				projectRoundUnitID
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var p = new ProjectRoundUnit();
					p.SortString = rs.GetString(0);
					p.Language = new Language { Id = rs.GetInt32(1) };
					return p;
				}
			}
			return null;
		}
		
		public ProjectRound ReadRound(int projectRoundID)
		{
			string query = string.Format(
				@"
SELECT Started,
   Closed
FROM ProjectRound
WHERE ProjectRoundID = {0}",
				projectRoundID
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var p = new ProjectRound {
						Started = GetDateTime(rs, 0),
						Closed = GetDateTime(rs, 1)
					};
					return p;
				}
			}
			return null;
		}
		
		public IList<ProjectRoundUnit> FindRoundUnitsBySortString(string sortString)
		{
			string query = string.Format(
				@"
SELECT dbo.cf_projectUnitTree(pru.ProjectRoundUnitID, ' » '),
	SortString
FROM ProjectRoundUnit pru
WHERE LEFT(pru.SortString, {1}) = '{0}'",
				sortString,
				sortString.Length
			);
			var units = new List<ProjectRoundUnit>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var u = new ProjectRoundUnit() {
						TreeString = rs.GetString(0),
						SortString = rs.GetString(1)
					};
					units.Add(u);
				}
			}
			return units;
		}
		
		public IList<ProjectRoundUnit> FindAllProjectRoundUnits()
		{
			throw new NotImplementedException();
		}
	}
}

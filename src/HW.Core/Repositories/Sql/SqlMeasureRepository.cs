using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlMeasureRepository : BaseSqlRepository<Measure>
	{
		public SqlMeasureRepository()
		{
		}
		
		public IList<UserMeasure> FindUserMeasures()
		{
			string query = string.Format(
				@"
SELECT um.UserID,
u.Email,
umc.MeasureComponentID,
SUM(umc.ValDec)
FROM UserMeasure um
INNER JOIN UserMeasureComponent umc ON umc.UserMeasureID = um.UserMeasureID
INNER JOIN [User] u ON u.UserID = um.UserID
WHERE umc.MeasureComponentID = 55
GROUP BY um.UserID, u.Email, umc.MeasureComponentID"
			);
			var measures = new List<UserMeasure>();
			using (SqlDataReader rs = ExecuteReader(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					if (rs.Read()) {
						bool done = false;
						while (!done) {
							var m = new UserMeasure { };
							do {
								m.User = new User { Id = GetInt32(rs, 0) };
								m.Values.Add(new UserMeasureComponent { ValueDecimal = GetDecimal(rs, 3) });
								done = !rs.Read();
							} while (!done && GetInt32(rs, 0) == m.User.Id);
							measures.Add(m);
						}
					}
				}
			}
			return measures;
		}
		
		public IList<SponsorProject> FindSponsorProjects()
		{
			string query = string.Format(
				@"
SELECT sp.SponsorProjectID,
	sp.SponsorID,
	sp.StartDT,
	sp.EndDT,
	sp.ProjectName
FROM SponsorProject sp"
			);
			var projects = new List<SponsorProject>();
			using (SqlDataReader rs = ExecuteReader(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new Sponsor { Id = GetInt32(rs, 1) };
					var p = new SponsorProject {
						Id = GetInt32(rs, 0),
						Sponsor = s,
						StartDate = GetDateTime(rs, 2),
						EndDate = GetDateTime(rs, 3),
						ProjectName = GetString(rs, 4)
					};
					projects.Add(p);
				}
			}
			return projects;
		}
	}
}

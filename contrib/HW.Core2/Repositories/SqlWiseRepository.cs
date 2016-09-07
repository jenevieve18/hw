using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlWiseRepository : BaseSqlRepository<Wise>
	{
		public SqlWiseRepository()
		{
		}
		
		public override void Save(Wise wise)
		{
			string query = @"
INSERT INTO Wise(
	WiseID, 
	LastShown
)
VALUES(
	@WiseID, 
	@LastShown
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@WiseID", wise.WiseID),
				new SqlParameter("@LastShown", wise.LastShown)
			);
		}
		
		public override void Update(Wise wise, int id)
		{
			string query = @"
UPDATE Wise SET
	WiseID = @WiseID,
	LastShown = @LastShown
WHERE WiseID = @WiseID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@WiseID", wise.WiseID),
				new SqlParameter("@LastShown", wise.LastShown)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Wise
WHERE WiseID = @WiseID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@WiseID", id)
			);
		}
		
		public override Wise Read(int id)
		{
			string query = @"
SELECT 	WiseID, 
	LastShown
FROM Wise
WHERE WiseID = @WiseID";
			Wise wise = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@WiseID", id))) {
				if (rs.Read()) {
					wise = new Wise {
						WiseID = GetInt32(rs, 0),
						LastShown = GetDateTime(rs, 1)
					};
				}
			}
			return wise;
		}
		
		public override IList<Wise> FindAll()
		{
			string query = @"
SELECT 	WiseID, 
	LastShown
FROM Wise";
			var wises = new List<Wise>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					wises.Add(new Wise {
						WiseID = GetInt32(rs, 0),
						LastShown = GetDateTime(rs, 1)
					});
				}
			}
			return wises;
		}
	}
}

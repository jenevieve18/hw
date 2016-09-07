using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlManagerFunctionRepository : BaseSqlRepository<ManagerFunction>
	{
		public SqlManagerFunctionRepository()
		{
		}
		
		public override void Save(ManagerFunction managerFunction)
		{
			string query = @"
INSERT INTO ManagerFunction(
	ManagerFunctionID, 
	ManagerFunction, 
	URL, 
	Expl
)
VALUES(
	@ManagerFunctionID, 
	@ManagerFunction, 
	@URL, 
	@Expl
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ManagerFunctionID", managerFunction.ManagerFunctionID),
				new SqlParameter("@ManagerFunction", managerFunction.Function),
				new SqlParameter("@URL", managerFunction.URL),
				new SqlParameter("@Expl", managerFunction.Expl)
			);
		}
		
		public override void Update(ManagerFunction managerFunction, int id)
		{
			string query = @"
UPDATE ManagerFunction SET
	ManagerFunctionID = @ManagerFunctionID,
	ManagerFunction = @ManagerFunction,
	URL = @URL,
	Expl = @Expl
WHERE ManagerFunctionID = @ManagerFunctionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ManagerFunctionID", managerFunction.ManagerFunctionID),
				new SqlParameter("@ManagerFunction", managerFunction.Function),
				new SqlParameter("@URL", managerFunction.URL),
				new SqlParameter("@Expl", managerFunction.Expl)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ManagerFunction
WHERE ManagerFunctionID = @ManagerFunctionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ManagerFunctionID", id)
			);
		}
		
		public override ManagerFunction Read(int id)
		{
			string query = @"
SELECT 	ManagerFunctionID, 
	ManagerFunction, 
	URL, 
	Expl
FROM ManagerFunction
WHERE ManagerFunctionID = @ManagerFunctionID";
			ManagerFunction managerFunction = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ManagerFunctionID", id))) {
				if (rs.Read()) {
					managerFunction = new ManagerFunction {
						ManagerFunctionID = GetInt32(rs, 0),
						Function = GetString(rs, 1),
						URL = GetString(rs, 2),
						Expl = GetString(rs, 3)
					};
				}
			}
			return managerFunction;
		}
		
		public override IList<ManagerFunction> FindAll()
		{
			string query = @"
SELECT 	ManagerFunctionID, 
	ManagerFunction, 
	URL, 
	Expl
FROM ManagerFunction";
			var managerFunctions = new List<ManagerFunction>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					managerFunctions.Add(new ManagerFunction {
						ManagerFunctionID = GetInt32(rs, 0),
						Function = GetString(rs, 1),
						URL = GetString(rs, 2),
						Expl = GetString(rs, 3)
					});
				}
			}
			return managerFunctions;
		}
	}
}

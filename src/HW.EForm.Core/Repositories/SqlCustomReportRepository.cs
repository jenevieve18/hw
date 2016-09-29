using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlCustomReportRepository : BaseSqlRepository<CustomReport>
	{
		public SqlCustomReportRepository()
		{
		}
		
		public override void Save(CustomReport customReport)
		{
			string query = @"
INSERT INTO CustomReport(
	CustomReportID, 
	UserID, 
	DT
)
VALUES(
	@CustomReportID, 
	@UserID, 
	@DT
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@CustomReportID", customReport.CustomReportID),
				new SqlParameter("@UserID", customReport.UserID),
				new SqlParameter("@DT", customReport.DT)
			);
		}
		
		public override void Update(CustomReport customReport, int id)
		{
			string query = @"
UPDATE CustomReport SET
	CustomReportID = @CustomReportID,
	UserID = @UserID,
	DT = @DT
WHERE CustomReportID = @CustomReportID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@CustomReportID", customReport.CustomReportID),
				new SqlParameter("@UserID", customReport.UserID),
				new SqlParameter("@DT", customReport.DT)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM CustomReport
WHERE CustomReportID = @CustomReportID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@CustomReportID", id)
			);
		}
		
		public override CustomReport Read(int id)
		{
			string query = @"
SELECT 	CustomReportID, 
	UserID, 
	DT
FROM CustomReport
WHERE CustomReportID = @CustomReportID";
			CustomReport customReport = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@CustomReportID", id))) {
				if (rs.Read()) {
					customReport = new CustomReport {
						CustomReportID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						DT = GetString(rs, 2)
					};
				}
			}
			return customReport;
		}
		
		public override IList<CustomReport> FindAll()
		{
			string query = @"
SELECT 	CustomReportID, 
	UserID, 
	DT
FROM CustomReport";
			var customReports = new List<CustomReport>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					customReports.Add(new CustomReport {
						CustomReportID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						DT = GetString(rs, 2)
					});
				}
			}
			return customReports;
		}
	}
}

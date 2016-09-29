using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlTempReportRepository : BaseSqlRepository<TempReport>
	{
		public SqlTempReportRepository()
		{
		}
		
		public override void Save(TempReport tempReport)
		{
			string query = @"
INSERT INTO TempReport(
	TempReportID, 
	TempReport
)
VALUES(
	@TempReportID, 
	@TempReport
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@TempReportID", tempReport.TempReportID),
				new SqlParameter("@TempReport", tempReport.TempReportText)
			);
		}
		
		public override void Update(TempReport tempReport, int id)
		{
			string query = @"
UPDATE TempReport SET
	TempReportID = @TempReportID,
	TempReport = @TempReport
WHERE TempReportID = @TempReportID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@TempReportID", tempReport.TempReportID),
				new SqlParameter("@TempReport", tempReport.TempReportText)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM TempReport
WHERE TempReportID = @TempReportID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@TempReportID", id)
			);
		}
		
		public override TempReport Read(int id)
		{
			string query = @"
SELECT 	TempReportID, 
	TempReport
FROM TempReport
WHERE TempReportID = @TempReportID";
			TempReport tempReport = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@TempReportID", id))) {
				if (rs.Read()) {
					tempReport = new TempReport {
						TempReportID = GetInt32(rs, 0),
						TempReportText = GetString(rs, 1)
					};
				}
			}
			return tempReport;
		}
		
		public override IList<TempReport> FindAll()
		{
			string query = @"
SELECT 	TempReportID, 
	TempReport
FROM TempReport";
			var tempReports = new List<TempReport>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					tempReports.Add(new TempReport {
						TempReportID = GetInt32(rs, 0),
						TempReportText = GetString(rs, 1)
					});
				}
			}
			return tempReports;
		}
	}
}

using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlReportRepository : BaseSqlRepository<Report>
	{
		public SqlReportRepository()
		{
		}
		
		public override void Save(Report report)
		{
			string query = @"
INSERT INTO Report(
	ReportID, 
	Internal, 
	ReportKey
)
VALUES(
	@ReportID, 
	@Internal, 
	@ReportKey
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ReportID", report.ReportID),
				new SqlParameter("@Internal", report.Internal),
				new SqlParameter("@ReportKey", report.ReportKey)
			);
		}
		
		public override void Update(Report report, int id)
		{
			string query = @"
UPDATE Report SET
	ReportID = @ReportID,
	Internal = @Internal,
	ReportKey = @ReportKey
WHERE ReportID = @ReportID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ReportID", report.ReportID),
				new SqlParameter("@Internal", report.Internal),
				new SqlParameter("@ReportKey", report.ReportKey)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Report
WHERE ReportID = @ReportID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ReportID", id)
			);
		}
		
		public override Report Read(int id)
		{
			string query = @"
SELECT 	ReportID, 
	Internal, 
	ReportKey
FROM Report
WHERE ReportID = @ReportID";
			Report report = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ReportID", id))) {
				if (rs.Read()) {
					report = new Report {
						ReportID = GetInt32(rs, 0),
						Internal = GetString(rs, 1),
						ReportKey = GetGuid(rs, 2)
					};
				}
			}
			return report;
		}
		
		public override IList<Report> FindAll()
		{
			string query = @"
SELECT 	ReportID, 
	Internal, 
	ReportKey
FROM Report";
			var reports = new List<Report>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					reports.Add(new Report {
						ReportID = GetInt32(rs, 0),
						Internal = GetString(rs, 1),
						ReportKey = GetGuid(rs, 2)
					});
				}
			}
			return reports;
		}
	}
}

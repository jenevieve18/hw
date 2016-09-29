using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlCustomReportRowRepository : BaseSqlRepository<CustomReportRow>
	{
		public SqlCustomReportRowRepository()
		{
		}
		
		public override void Save(CustomReportRow customReportRow)
		{
			string query = @"
INSERT INTO CustomReportRow(
	CustomReportRowID, 
	CustomReportID, 
	Before, 
	Editable, 
	After, 
	Width, 
	Height
)
VALUES(
	@CustomReportRowID, 
	@CustomReportID, 
	@Before, 
	@Editable, 
	@After, 
	@Width, 
	@Height
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@CustomReportRowID", customReportRow.CustomReportRowID),
				new SqlParameter("@CustomReportID", customReportRow.CustomReportID),
				new SqlParameter("@Before", customReportRow.Before),
				new SqlParameter("@Editable", customReportRow.Editable),
				new SqlParameter("@After", customReportRow.After),
				new SqlParameter("@Width", customReportRow.Width),
				new SqlParameter("@Height", customReportRow.Height)
			);
		}
		
		public override void Update(CustomReportRow customReportRow, int id)
		{
			string query = @"
UPDATE CustomReportRow SET
	CustomReportRowID = @CustomReportRowID,
	CustomReportID = @CustomReportID,
	Before = @Before,
	Editable = @Editable,
	After = @After,
	Width = @Width,
	Height = @Height
WHERE CustomReportRowID = @CustomReportRowID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@CustomReportRowID", customReportRow.CustomReportRowID),
				new SqlParameter("@CustomReportID", customReportRow.CustomReportID),
				new SqlParameter("@Before", customReportRow.Before),
				new SqlParameter("@Editable", customReportRow.Editable),
				new SqlParameter("@After", customReportRow.After),
				new SqlParameter("@Width", customReportRow.Width),
				new SqlParameter("@Height", customReportRow.Height)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM CustomReportRow
WHERE CustomReportRowID = @CustomReportRowID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@CustomReportRowID", id)
			);
		}
		
		public override CustomReportRow Read(int id)
		{
			string query = @"
SELECT 	CustomReportRowID, 
	CustomReportID, 
	Before, 
	Editable, 
	After, 
	Width, 
	Height
FROM CustomReportRow
WHERE CustomReportRowID = @CustomReportRowID";
			CustomReportRow customReportRow = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@CustomReportRowID", id))) {
				if (rs.Read()) {
					customReportRow = new CustomReportRow {
						CustomReportRowID = GetInt32(rs, 0),
						CustomReportID = GetInt32(rs, 1),
						Before = GetString(rs, 2),
						Editable = GetString(rs, 3),
						After = GetString(rs, 4),
						Width = GetInt32(rs, 5),
						Height = GetInt32(rs, 6)
					};
				}
			}
			return customReportRow;
		}
		
		public override IList<CustomReportRow> FindAll()
		{
			string query = @"
SELECT 	CustomReportRowID, 
	CustomReportID, 
	Before, 
	Editable, 
	After, 
	Width, 
	Height
FROM CustomReportRow";
			var customReportRows = new List<CustomReportRow>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					customReportRows.Add(new CustomReportRow {
						CustomReportRowID = GetInt32(rs, 0),
						CustomReportID = GetInt32(rs, 1),
						Before = GetString(rs, 2),
						Editable = GetString(rs, 3),
						After = GetString(rs, 4),
						Width = GetInt32(rs, 5),
						Height = GetInt32(rs, 6)
					});
				}
			}
			return customReportRows;
		}
	}
}

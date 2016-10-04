using System;
using HW.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core.Repositories.Sql
{
	public class SqlReportPartComponentRepository : BaseSqlRepository<ReportPartComponent>
	{
		public SqlReportPartComponentRepository()
		{
		}
		
		public override void Save(ReportPartComponent reportPartComponent)
		{
			string query = @"
INSERT INTO ReportPartComponent(
	ReportPartComponentID, 
	ReportPartID, 
	IdxID, 
	WeightedQuestionOptionID, 
	SortOrder
)
VALUES(
	@ReportPartComponentID, 
	@ReportPartID, 
	@IdxID, 
	@WeightedQuestionOptionID, 
	@SortOrder
)";
			ExecuteNonQuery(
				query,
				"eFormSqlConnection",
				new SqlParameter("@ReportPartComponentID", reportPartComponent.ReportPartComponentID),
				new SqlParameter("@ReportPartID", reportPartComponent.ReportPartID),
				new SqlParameter("@IdxID", reportPartComponent.IdxID),
				new SqlParameter("@WeightedQuestionOptionID", reportPartComponent.WeightedQuestionOptionID),
				new SqlParameter("@SortOrder", reportPartComponent.SortOrder)
			);
		}
		
		public override void Update(ReportPartComponent reportPartComponent, int id)
		{
			string query = @"
UPDATE ReportPartComponent SET
	ReportPartComponentID = @ReportPartComponentID,
	ReportPartID = @ReportPartID,
	IdxID = @IdxID,
	WeightedQuestionOptionID = @WeightedQuestionOptionID,
	SortOrder = @SortOrder
WHERE ReportPartComponentID = @ReportPartComponentID";
			ExecuteNonQuery(
				query,
				"eFormSqlConnection",
				new SqlParameter("@ReportPartComponentID", reportPartComponent.ReportPartComponentID),
				new SqlParameter("@ReportPartID", reportPartComponent.ReportPartID),
				new SqlParameter("@IdxID", reportPartComponent.IdxID),
				new SqlParameter("@WeightedQuestionOptionID", reportPartComponent.WeightedQuestionOptionID),
				new SqlParameter("@SortOrder", reportPartComponent.SortOrder)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ReportPartComponent
WHERE ReportPartComponentID = @ReportPartComponentID";
			ExecuteNonQuery(
				query,
				"eFormSqlConnection",
				new SqlParameter("@ReportPartComponentID", id)
			);
		}
		
		public override ReportPartComponent Read(int id)
		{
			string query = @"
SELECT 	ReportPartComponentID, 
	ReportPartID, 
	IdxID, 
	WeightedQuestionOptionID, 
	SortOrder
FROM ReportPartComponent
WHERE ReportPartComponentID = @ReportPartComponentID";
			ReportPartComponent reportPartComponent = null;
			using (var rs = ExecuteReader(query, "eFormSqlConnection", new SqlParameter("@ReportPartComponentID", id))) {
				if (rs.Read()) {
					reportPartComponent = new ReportPartComponent {
						ReportPartComponentID = GetInt32(rs, 0),
						ReportPartID = GetInt32(rs, 1),
						IdxID = GetInt32(rs, 2),
						WeightedQuestionOptionID = GetInt32(rs, 3),
						SortOrder = GetInt32(rs, 4)
					};
				}
			}
			return reportPartComponent;
		}
		
		public override IList<ReportPartComponent> FindAll()
		{
			string query = @"
SELECT 	ReportPartComponentID, 
	ReportPartID, 
	IdxID, 
	WeightedQuestionOptionID, 
	SortOrder
FROM ReportPartComponent";
			var reportPartComponents = new List<ReportPartComponent>();
			using (var rs = ExecuteReader(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					reportPartComponents.Add(new ReportPartComponent {
						ReportPartComponentID = GetInt32(rs, 0),
						ReportPartID = GetInt32(rs, 1),
						IdxID = GetInt32(rs, 2),
						WeightedQuestionOptionID = GetInt32(rs, 3),
						SortOrder = GetInt32(rs, 4)
					});
				}
			}
			return reportPartComponents;
		}
		
		public IList<ReportPartComponent> FindByReportPart(int reportPartID)
		{
			string query = @"
SELECT 	ReportPartComponentID, 
	ReportPartID, 
	IdxID, 
	WeightedQuestionOptionID, 
	SortOrder
FROM ReportPartComponent
WHERE ReportPartID = @ReportPartID";
			var reportPartComponents = new List<ReportPartComponent>();
			using (var rs = ExecuteReader(query, "eFormSqlConnection", new SqlParameter("@ReportPartID", reportPartID))) {
				while (rs.Read()) {
					reportPartComponents.Add(new ReportPartComponent {
						ReportPartComponentID = GetInt32(rs, 0),
						ReportPartID = GetInt32(rs, 1),
						IdxID = GetInt32(rs, 2),
						WeightedQuestionOptionID = GetInt32(rs, 3),
						SortOrder = GetInt32(rs, 4)
					});
				}
			}
			return reportPartComponents;
		}
	}
}

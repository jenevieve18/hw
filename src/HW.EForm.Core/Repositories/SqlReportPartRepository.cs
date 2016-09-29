using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlReportPartRepository : BaseSqlRepository<ReportPart>
	{
		public SqlReportPartRepository()
		{
		}
		
		public override void Save(ReportPart reportPart)
		{
			string query = @"
INSERT INTO ReportPart(
	ReportPartID, 
	ReportID, 
	Internal, 
	Type, 
	QuestionID, 
	OptionID, 
	RequiredAnswerCount, 
	SortOrder, 
	PartLevel, 
	GroupingQuestionID, 
	GroupingOptionID
)
VALUES(
	@ReportPartID, 
	@ReportID, 
	@Internal, 
	@Type, 
	@QuestionID, 
	@OptionID, 
	@RequiredAnswerCount, 
	@SortOrder, 
	@PartLevel, 
	@GroupingQuestionID, 
	@GroupingOptionID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ReportPartID", reportPart.ReportPartID),
				new SqlParameter("@ReportID", reportPart.ReportID),
				new SqlParameter("@Internal", reportPart.Internal),
				new SqlParameter("@Type", reportPart.Type),
				new SqlParameter("@QuestionID", reportPart.QuestionID),
				new SqlParameter("@OptionID", reportPart.OptionID),
				new SqlParameter("@RequiredAnswerCount", reportPart.RequiredAnswerCount),
				new SqlParameter("@SortOrder", reportPart.SortOrder),
				new SqlParameter("@PartLevel", reportPart.PartLevel),
				new SqlParameter("@GroupingQuestionID", reportPart.GroupingQuestionID),
				new SqlParameter("@GroupingOptionID", reportPart.GroupingOptionID)
			);
		}
		
		public override void Update(ReportPart reportPart, int id)
		{
			string query = @"
UPDATE ReportPart SET
	ReportPartID = @ReportPartID,
	ReportID = @ReportID,
	Internal = @Internal,
	Type = @Type,
	QuestionID = @QuestionID,
	OptionID = @OptionID,
	RequiredAnswerCount = @RequiredAnswerCount,
	SortOrder = @SortOrder,
	PartLevel = @PartLevel,
	GroupingQuestionID = @GroupingQuestionID,
	GroupingOptionID = @GroupingOptionID
WHERE ReportPartID = @ReportPartID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ReportPartID", reportPart.ReportPartID),
				new SqlParameter("@ReportID", reportPart.ReportID),
				new SqlParameter("@Internal", reportPart.Internal),
				new SqlParameter("@Type", reportPart.Type),
				new SqlParameter("@QuestionID", reportPart.QuestionID),
				new SqlParameter("@OptionID", reportPart.OptionID),
				new SqlParameter("@RequiredAnswerCount", reportPart.RequiredAnswerCount),
				new SqlParameter("@SortOrder", reportPart.SortOrder),
				new SqlParameter("@PartLevel", reportPart.PartLevel),
				new SqlParameter("@GroupingQuestionID", reportPart.GroupingQuestionID),
				new SqlParameter("@GroupingOptionID", reportPart.GroupingOptionID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ReportPart
WHERE ReportPartID = @ReportPartID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ReportPartID", id)
			);
		}
		
		public override ReportPart Read(int id)
		{
			string query = @"
SELECT 	ReportPartID, 
	ReportID, 
	Internal, 
	Type, 
	QuestionID, 
	OptionID, 
	RequiredAnswerCount, 
	SortOrder, 
	PartLevel, 
	GroupingQuestionID, 
	GroupingOptionID
FROM ReportPart
WHERE ReportPartID = @ReportPartID";
			ReportPart reportPart = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ReportPartID", id))) {
				if (rs.Read()) {
					reportPart = new ReportPart {
						ReportPartID = GetInt32(rs, 0),
						ReportID = GetInt32(rs, 1),
						Internal = GetString(rs, 2),
						Type = GetInt32(rs, 3),
						QuestionID = GetInt32(rs, 4),
						OptionID = GetInt32(rs, 5),
						RequiredAnswerCount = GetInt32(rs, 6),
						SortOrder = GetInt32(rs, 7),
						PartLevel = GetInt32(rs, 8),
						GroupingQuestionID = GetInt32(rs, 9),
						GroupingOptionID = GetInt32(rs, 10)
					};
				}
			}
			return reportPart;
		}
		
		public override IList<ReportPart> FindAll()
		{
			string query = @"
SELECT 	ReportPartID, 
	ReportID, 
	Internal, 
	Type, 
	QuestionID, 
	OptionID, 
	RequiredAnswerCount, 
	SortOrder, 
	PartLevel, 
	GroupingQuestionID, 
	GroupingOptionID
FROM ReportPart";
			var reportParts = new List<ReportPart>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					reportParts.Add(new ReportPart {
						ReportPartID = GetInt32(rs, 0),
						ReportID = GetInt32(rs, 1),
						Internal = GetString(rs, 2),
						Type = GetInt32(rs, 3),
						QuestionID = GetInt32(rs, 4),
						OptionID = GetInt32(rs, 5),
						RequiredAnswerCount = GetInt32(rs, 6),
						SortOrder = GetInt32(rs, 7),
						PartLevel = GetInt32(rs, 8),
						GroupingQuestionID = GetInt32(rs, 9),
						GroupingOptionID = GetInt32(rs, 10)
					});
				}
			}
			return reportParts;
		}
	}
}

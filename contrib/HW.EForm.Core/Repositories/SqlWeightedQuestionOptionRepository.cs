using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.EForm.Core.Models;
	
namespace HW.EForm.Core.Repositories
{
	public interface IWeightedQuestionOptionRepository : IBaseRepository<WeightedQuestionOption>
	{
		WeightedQuestionOption ReadByQuestion(int questionID);
	}
	
	public class SqlWeightedQuestionOptionRepository : BaseSqlRepository<WeightedQuestionOption>, IWeightedQuestionOptionRepository
	{
		public SqlWeightedQuestionOptionRepository()
		{
		}
		
		public override void Save(WeightedQuestionOption weightedQuestionOption)
		{
			string query = @"
INSERT INTO WeightedQuestionOption(
	WeightedQuestionOptionID, 
	Internal, 
	QuestionID, 
	OptionID, 
	TargetVal, 
	YellowLow, 
	GreenLow, 
	GreenHigh, 
	YellowHigh, 
	SortOrder
)
VALUES(
	@WeightedQuestionOptionID, 
	@Internal, 
	@QuestionID, 
	@OptionID, 
	@TargetVal, 
	@YellowLow, 
	@GreenLow, 
	@GreenHigh, 
	@YellowHigh, 
	@SortOrder
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@WeightedQuestionOptionID", weightedQuestionOption.WeightedQuestionOptionID),
				new SqlParameter("@Internal", weightedQuestionOption.Internal),
				new SqlParameter("@QuestionID", weightedQuestionOption.QuestionID),
				new SqlParameter("@OptionID", weightedQuestionOption.OptionID),
				new SqlParameter("@TargetVal", weightedQuestionOption.TargetVal),
				new SqlParameter("@YellowLow", weightedQuestionOption.YellowLow),
				new SqlParameter("@GreenLow", weightedQuestionOption.GreenLow),
				new SqlParameter("@GreenHigh", weightedQuestionOption.GreenHigh),
				new SqlParameter("@YellowHigh", weightedQuestionOption.YellowHigh),
				new SqlParameter("@SortOrder", weightedQuestionOption.SortOrder)
			);
		}
		
		public override void Update(WeightedQuestionOption weightedQuestionOption, int id)
		{
			string query = @"
UPDATE WeightedQuestionOption SET
	WeightedQuestionOptionID = @WeightedQuestionOptionID,
	Internal = @Internal,
	QuestionID = @QuestionID,
	OptionID = @OptionID,
	TargetVal = @TargetVal,
	YellowLow = @YellowLow,
	GreenLow = @GreenLow,
	GreenHigh = @GreenHigh,
	YellowHigh = @YellowHigh,
	SortOrder = @SortOrder
WHERE WeightedQuestionOptionID = @WeightedQuestionOptionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@WeightedQuestionOptionID", weightedQuestionOption.WeightedQuestionOptionID),
				new SqlParameter("@Internal", weightedQuestionOption.Internal),
				new SqlParameter("@QuestionID", weightedQuestionOption.QuestionID),
				new SqlParameter("@OptionID", weightedQuestionOption.OptionID),
				new SqlParameter("@TargetVal", weightedQuestionOption.TargetVal),
				new SqlParameter("@YellowLow", weightedQuestionOption.YellowLow),
				new SqlParameter("@GreenLow", weightedQuestionOption.GreenLow),
				new SqlParameter("@GreenHigh", weightedQuestionOption.GreenHigh),
				new SqlParameter("@YellowHigh", weightedQuestionOption.YellowHigh),
				new SqlParameter("@SortOrder", weightedQuestionOption.SortOrder)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM WeightedQuestionOption
WHERE WeightedQuestionOptionID = @WeightedQuestionOptionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@WeightedQuestionOptionID", id)
			);
		}
		
		public override WeightedQuestionOption Read(int id)
		{
			string query = @"
SELECT 	WeightedQuestionOptionID, 
	Internal, 
	QuestionID, 
	OptionID, 
	TargetVal, 
	YellowLow, 
	GreenLow, 
	GreenHigh, 
	YellowHigh, 
	SortOrder
FROM WeightedQuestionOption
WHERE WeightedQuestionOptionID = @WeightedQuestionOptionID";
			WeightedQuestionOption weightedQuestionOption = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@WeightedQuestionOptionID", id))) {
				if (rs.Read()) {
					weightedQuestionOption = new WeightedQuestionOption {
						WeightedQuestionOptionID = GetInt32(rs, 0),
						Internal = GetString(rs, 1),
						QuestionID = GetInt32(rs, 2),
						OptionID = GetInt32(rs, 3),
						TargetVal = GetInt32(rs, 4),
						YellowLow = GetInt32(rs, 5),
						GreenLow = GetInt32(rs, 6),
						GreenHigh = GetInt32(rs, 7),
						YellowHigh = GetInt32(rs, 8),
						SortOrder = GetInt32(rs, 9)
					};
				}
			}
			return weightedQuestionOption;
		}
		
		public WeightedQuestionOption ReadByQuestion(int questionID)
		{
			string query = @"
SELECT 	WeightedQuestionOptionID, 
	Internal, 
	QuestionID, 
	OptionID, 
	TargetVal, 
	YellowLow, 
	GreenLow, 
	GreenHigh, 
	YellowHigh, 
	SortOrder
FROM WeightedQuestionOption
WHERE QuestionID = @QuestionID";
			WeightedQuestionOption weightedQuestionOption = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@QuestionID", questionID))) {
				if (rs.Read()) {
					weightedQuestionOption = new WeightedQuestionOption {
						WeightedQuestionOptionID = GetInt32(rs, 0),
						Internal = GetString(rs, 1),
						QuestionID = GetInt32(rs, 2),
						OptionID = GetInt32(rs, 3),
						TargetVal = GetInt32(rs, 4),
						YellowLow = GetInt32(rs, 5),
						GreenLow = GetInt32(rs, 6),
						GreenHigh = GetInt32(rs, 7),
						YellowHigh = GetInt32(rs, 8),
						SortOrder = GetInt32(rs, 9)
					};
				}
			}
			return weightedQuestionOption;
		}
		
		public WeightedQuestionOption ReadByQuestionAndOption(int questionID, int optionID)
		{
			string query = @"
SELECT 	WeightedQuestionOptionID, 
	Internal, 
	QuestionID, 
	OptionID, 
	TargetVal, 
	YellowLow, 
	GreenLow, 
	GreenHigh, 
	YellowHigh, 
	SortOrder
FROM WeightedQuestionOption
WHERE QuestionID = @QuestionID
AND OptionID = @OptionID";
			WeightedQuestionOption weightedQuestionOption = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@QuestionID", questionID), new SqlParameter("@OptionID", optionID))) {
				if (rs.Read()) {
					weightedQuestionOption = new WeightedQuestionOption {
						WeightedQuestionOptionID = GetInt32(rs, 0),
						Internal = GetString(rs, 1),
						QuestionID = GetInt32(rs, 2),
						OptionID = GetInt32(rs, 3),
						TargetVal = GetInt32(rs, 4),
						YellowLow = GetInt32(rs, 5),
						GreenLow = GetInt32(rs, 6),
						GreenHigh = GetInt32(rs, 7),
						YellowHigh = GetInt32(rs, 8),
						SortOrder = GetInt32(rs, 9)
					};
				}
			}
			return weightedQuestionOption;
		}
		
		public override IList<WeightedQuestionOption> FindAll()
		{
			string query = @"
SELECT 	WeightedQuestionOptionID, 
	Internal, 
	QuestionID, 
	OptionID, 
	TargetVal, 
	YellowLow, 
	GreenLow, 
	GreenHigh, 
	YellowHigh, 
	SortOrder
FROM WeightedQuestionOption";
			var weightedQuestionOptions = new List<WeightedQuestionOption>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					weightedQuestionOptions.Add(new WeightedQuestionOption {
						WeightedQuestionOptionID = GetInt32(rs, 0),
						Internal = GetString(rs, 1),
						QuestionID = GetInt32(rs, 2),
						OptionID = GetInt32(rs, 3),
						TargetVal = GetInt32(rs, 4),
						YellowLow = GetInt32(rs, 5),
						GreenLow = GetInt32(rs, 6),
						GreenHigh = GetInt32(rs, 7),
						YellowHigh = GetInt32(rs, 8),
						SortOrder = GetInt32(rs, 9)
					});
				}
			}
			return weightedQuestionOptions;
		}
	}
}

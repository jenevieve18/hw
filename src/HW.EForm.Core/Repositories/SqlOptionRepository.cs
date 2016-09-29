using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlOptionRepository : BaseSqlRepository<Option>
	{
		public SqlOptionRepository()
		{
		}
		
		public override void Save(Option option)
		{
			string query = @"
INSERT INTO Option(
	OptionID, 
	OptionType, 
	OptionPlacement, 
	Variablename, 
	Internal, 
	Width, 
	Height, 
	InnerWidth, 
	OptionContainerID, 
	BgColor, 
	RangeLow, 
	RangeHigh
)
VALUES(
	@OptionID, 
	@OptionType, 
	@OptionPlacement, 
	@Variablename, 
	@Internal, 
	@Width, 
	@Height, 
	@InnerWidth, 
	@OptionContainerID, 
	@BgColor, 
	@RangeLow, 
	@RangeHigh
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@OptionID", option.OptionID),
				new SqlParameter("@OptionType", option.OptionType),
				new SqlParameter("@OptionPlacement", option.OptionPlacement),
				new SqlParameter("@Variablename", option.Variablename),
				new SqlParameter("@Internal", option.Internal),
				new SqlParameter("@Width", option.Width),
				new SqlParameter("@Height", option.Height),
				new SqlParameter("@InnerWidth", option.InnerWidth),
				new SqlParameter("@OptionContainerID", option.OptionContainerID),
				new SqlParameter("@BgColor", option.BgColor),
				new SqlParameter("@RangeLow", option.RangeLow),
				new SqlParameter("@RangeHigh", option.RangeHigh)
			);
		}
		
		public override void Update(Option option, int id)
		{
			string query = @"
UPDATE Option SET
	OptionID = @OptionID,
	OptionType = @OptionType,
	OptionPlacement = @OptionPlacement,
	Variablename = @Variablename,
	Internal = @Internal,
	Width = @Width,
	Height = @Height,
	InnerWidth = @InnerWidth,
	OptionContainerID = @OptionContainerID,
	BgColor = @BgColor,
	RangeLow = @RangeLow,
	RangeHigh = @RangeHigh
WHERE OptionID = @OptionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@OptionID", option.OptionID),
				new SqlParameter("@OptionType", option.OptionType),
				new SqlParameter("@OptionPlacement", option.OptionPlacement),
				new SqlParameter("@Variablename", option.Variablename),
				new SqlParameter("@Internal", option.Internal),
				new SqlParameter("@Width", option.Width),
				new SqlParameter("@Height", option.Height),
				new SqlParameter("@InnerWidth", option.InnerWidth),
				new SqlParameter("@OptionContainerID", option.OptionContainerID),
				new SqlParameter("@BgColor", option.BgColor),
				new SqlParameter("@RangeLow", option.RangeLow),
				new SqlParameter("@RangeHigh", option.RangeHigh)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Option
WHERE OptionID = @OptionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@OptionID", id)
			);
		}
		
		public override Option Read(int id)
		{
			string query = @"
SELECT 	OptionID, 
	OptionType, 
	OptionPlacement, 
	Variablename, 
	Internal, 
	Width, 
	Height, 
	InnerWidth, 
	OptionContainerID, 
	BgColor, 
	RangeLow, 
	RangeHigh
FROM Option
WHERE OptionID = @OptionID";
			Option option = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@OptionID", id))) {
				if (rs.Read()) {
					option = new Option {
						OptionID = GetInt32(rs, 0),
						OptionType = GetInt32(rs, 1),
						OptionPlacement = GetInt32(rs, 2),
						Variablename = GetString(rs, 3),
						Internal = GetString(rs, 4),
						Width = GetInt32(rs, 5),
						Height = GetInt32(rs, 6),
						InnerWidth = GetInt32(rs, 7),
						OptionContainerID = GetInt32(rs, 8),
						BgColor = GetString(rs, 9),
						RangeLow = GetDecimal(rs, 10),
						RangeHigh = GetDecimal(rs, 11)
					};
				}
			}
			return option;
		}
		
		public override IList<Option> FindAll()
		{
			string query = @"
SELECT 	OptionID, 
	OptionType, 
	OptionPlacement, 
	Variablename, 
	Internal, 
	Width, 
	Height, 
	InnerWidth, 
	OptionContainerID, 
	BgColor, 
	RangeLow, 
	RangeHigh
FROM Option";
			var options = new List<Option>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					options.Add(new Option {
						OptionID = GetInt32(rs, 0),
						OptionType = GetInt32(rs, 1),
						OptionPlacement = GetInt32(rs, 2),
						Variablename = GetString(rs, 3),
						Internal = GetString(rs, 4),
						Width = GetInt32(rs, 5),
						Height = GetInt32(rs, 6),
						InnerWidth = GetInt32(rs, 7),
						OptionContainerID = GetInt32(rs, 8),
						BgColor = GetString(rs, 9),
						RangeLow = GetDecimal(rs, 10),
						RangeHigh = GetDecimal(rs, 11)
					});
				}
			}
			return options;
		}
	}
}

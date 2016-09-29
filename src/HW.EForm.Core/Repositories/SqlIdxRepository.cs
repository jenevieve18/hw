using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlIdxRepository : BaseSqlRepository<Index>
	{
		public SqlIdxRepository()
		{
		}
		
		public override void Save(Index idx)
		{
			string query = @"
INSERT INTO Idx(
	IdxID, 
	Internal, 
	RequiredAnswerCount, 
	AllPartsRequired, 
	MaxVal, 
	SortOrder, 
	TargetVal, 
	YellowLow, 
	GreenLow, 
	GreenHigh, 
	YellowHigh, 
	CX, 
	Description
)
VALUES(
	@IdxID, 
	@Internal, 
	@RequiredAnswerCount, 
	@AllPartsRequired, 
	@MaxVal, 
	@SortOrder, 
	@TargetVal, 
	@YellowLow, 
	@GreenLow, 
	@GreenHigh, 
	@YellowHigh, 
	@CX, 
	@Description
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@IdxID", idx.IdxID),
				new SqlParameter("@Internal", idx.Internal),
				new SqlParameter("@RequiredAnswerCount", idx.RequiredAnswerCount),
				new SqlParameter("@AllPartsRequired", idx.AllPartsRequired),
				new SqlParameter("@MaxVal", idx.MaxVal),
				new SqlParameter("@SortOrder", idx.SortOrder),
				new SqlParameter("@TargetVal", idx.TargetVal),
				new SqlParameter("@YellowLow", idx.YellowLow),
				new SqlParameter("@GreenLow", idx.GreenLow),
				new SqlParameter("@GreenHigh", idx.GreenHigh),
				new SqlParameter("@YellowHigh", idx.YellowHigh),
				new SqlParameter("@CX", idx.CX),
				new SqlParameter("@Description", idx.Description)
			);
		}
		
		public override void Update(Index idx, int id)
		{
			string query = @"
UPDATE Idx SET
	IdxID = @IdxID,
	Internal = @Internal,
	RequiredAnswerCount = @RequiredAnswerCount,
	AllPartsRequired = @AllPartsRequired,
	MaxVal = @MaxVal,
	SortOrder = @SortOrder,
	TargetVal = @TargetVal,
	YellowLow = @YellowLow,
	GreenLow = @GreenLow,
	GreenHigh = @GreenHigh,
	YellowHigh = @YellowHigh,
	CX = @CX,
	Description = @Description
WHERE IdxID = @IdxID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@IdxID", idx.IdxID),
				new SqlParameter("@Internal", idx.Internal),
				new SqlParameter("@RequiredAnswerCount", idx.RequiredAnswerCount),
				new SqlParameter("@AllPartsRequired", idx.AllPartsRequired),
				new SqlParameter("@MaxVal", idx.MaxVal),
				new SqlParameter("@SortOrder", idx.SortOrder),
				new SqlParameter("@TargetVal", idx.TargetVal),
				new SqlParameter("@YellowLow", idx.YellowLow),
				new SqlParameter("@GreenLow", idx.GreenLow),
				new SqlParameter("@GreenHigh", idx.GreenHigh),
				new SqlParameter("@YellowHigh", idx.YellowHigh),
				new SqlParameter("@CX", idx.CX),
				new SqlParameter("@Description", idx.Description)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Idx
WHERE IdxID = @IdxID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@IdxID", id)
			);
		}
		
		public override Index Read(int id)
		{
			string query = @"
SELECT 	IdxID, 
	Internal, 
	RequiredAnswerCount, 
	AllPartsRequired, 
	MaxVal, 
	SortOrder, 
	TargetVal, 
	YellowLow, 
	GreenLow, 
	GreenHigh, 
	YellowHigh, 
	CX, 
	Description
FROM Idx
WHERE IdxID = @IdxID";
			Index idx = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@IdxID", id))) {
				if (rs.Read()) {
					idx = new Index {
						IdxID = GetInt32(rs, 0),
						Internal = GetString(rs, 1),
						RequiredAnswerCount = GetInt32(rs, 2),
						AllPartsRequired = GetBoolean(rs, 3),
						MaxVal = GetInt32(rs, 4),
						SortOrder = GetInt32(rs, 5),
						TargetVal = GetInt32(rs, 6),
						YellowLow = GetInt32(rs, 7),
						GreenLow = GetInt32(rs, 8),
						GreenHigh = GetInt32(rs, 9),
						YellowHigh = GetInt32(rs, 10),
						CX = GetInt32(rs, 11),
						Description = GetString(rs, 12)
					};
				}
			}
			return idx;
		}
		
		public override IList<Index> FindAll()
		{
			string query = @"
SELECT 	IdxID, 
	Internal, 
	RequiredAnswerCount, 
	AllPartsRequired, 
	MaxVal, 
	SortOrder, 
	TargetVal, 
	YellowLow, 
	GreenLow, 
	GreenHigh, 
	YellowHigh, 
	CX, 
	Description
FROM Idx";
			var idxs = new List<Index>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					idxs.Add(new Index {
						IdxID = GetInt32(rs, 0),
						Internal = GetString(rs, 1),
						RequiredAnswerCount = GetInt32(rs, 2),
						AllPartsRequired = GetBoolean(rs, 3),
						MaxVal = GetInt32(rs, 4),
						SortOrder = GetInt32(rs, 5),
						TargetVal = GetInt32(rs, 6),
						YellowLow = GetInt32(rs, 7),
						GreenLow = GetInt32(rs, 8),
						GreenHigh = GetInt32(rs, 9),
						YellowHigh = GetInt32(rs, 10),
						CX = GetInt32(rs, 11),
						Description = GetString(rs, 12)
					});
				}
			}
			return idxs;
		}
	}
}

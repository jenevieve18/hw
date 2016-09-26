using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HW.EForm.Core.Repositories
{
	public class SqlIndexRepository : BaseSqlRepository<Index>
	{
		public SqlIndexRepository()
		{
		}
		
		public override void Save(Index index)
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
	CX
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
	@CX
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@IdxID", index.IdxID),
				new SqlParameter("@Internal", index.Internal),
				new SqlParameter("@RequiredAnswerCount", index.RequiredAnswerCount),
				new SqlParameter("@AllPartsRequired", index.AllPartsRequired),
				new SqlParameter("@MaxVal", index.MaxVal),
				new SqlParameter("@SortOrder", index.SortOrder),
				new SqlParameter("@TargetVal", index.TargetVal),
				new SqlParameter("@YellowLow", index.YellowLow),
				new SqlParameter("@GreenLow", index.GreenLow),
				new SqlParameter("@GreenHigh", index.GreenHigh),
				new SqlParameter("@YellowHigh", index.YellowHigh),
				new SqlParameter("@CX", index.CX)
			);
		}
		
		public override void Update(Index index, int id)
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
	CX = @CX
WHERE IdxID = @IdxID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@IdxID", index.IdxID),
				new SqlParameter("@Internal", index.Internal),
				new SqlParameter("@RequiredAnswerCount", index.RequiredAnswerCount),
				new SqlParameter("@AllPartsRequired", index.AllPartsRequired),
				new SqlParameter("@MaxVal", index.MaxVal),
				new SqlParameter("@SortOrder", index.SortOrder),
				new SqlParameter("@TargetVal", index.TargetVal),
				new SqlParameter("@YellowLow", index.YellowLow),
				new SqlParameter("@GreenLow", index.GreenLow),
				new SqlParameter("@GreenHigh", index.GreenHigh),
				new SqlParameter("@YellowHigh", index.YellowHigh),
				new SqlParameter("@CX", index.CX)
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
	CX
FROM Idx
WHERE IdxID = @IdxID";
			Index index = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@IdxID", id))) {
				if (rs.Read()) {
					index = new Index {
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
						CX = GetInt32(rs, 11)
					};
				}
			}
			return index;
		}
		
		public Index Lalala(int idxID, int langID, int groupID, string sql)
		{
			string query = string.Format(
				@"
SELECT
	AVG(tmp.AX),
	tmp.Idx,
	tmp.IdxID,
	COUNT(*) AS DX
FROM
(
	SELECT
		100*CAST(SUM(ipc.Val*ip.Multiple) AS REAL)/i.MaxVal AS AX,
		i.IdxID,
		il.Idx,
		i.CX,
		i.AllPartsRequired,
		COUNT(*) AS BX
	FROM Idx i
	INNER JOIN IdxLang il ON i.IdxID = il.IdxID AND il.LangID = @LangID
	INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID
	INNER JOIN IdxPartComponent ipc ON ip.IdxPartID = ipc.IdxPartID
	INNER JOIN AnswerValue av ON ip.QuestionID = av.QuestionID AND ip.OptionID = av.OptionID AND av.ValueInt = ipc.OptionComponentID AND av.DeletedSessionID IS NULL
	INNER JOIN Answer a ON av.AnswerID = a.AnswerID
	INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
	{0}
	WHERE a.EndDT IS NOT NULL AND i.IdxID = @IdxID
	{1}
	{2}
	GROUP BY i.IdxID, a.AnswerID, i.MaxVal, il.Idx, i.CX, i.AllPartsRequired
) tmp
WHERE tmp.AllPartsRequired = 0 OR tmp.CX = tmp.BX
GROUP BY tmp.IdxID, tmp.Idx",
				(groupID != 0 ? "INNER JOIN ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID " : ""),
				(groupID != 0 ? "AND u.GroupID = " + groupID + " " : ""),
				sql
			);
			Index index = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@IdxID", idxID), new SqlParameter("@LangID", langID))) {
				while (rs.Read()) {
					index = new Index {
						DX = GetInt32(rs, 3),
						Val = GetDouble(rs, 0),
						Description = GetString(rs, 1)
					};
				}
			}
			return index;
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
	CX
FROM Idx";
			var indexes = new List<Index>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					indexes.Add(new Index {
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
					            	CX = GetInt32(rs, 11)
					            });
				}
			}
			return indexes;
		}
	}
}

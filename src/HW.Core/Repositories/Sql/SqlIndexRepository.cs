using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlIndexRepository : BaseSqlRepository<Index>
	{
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
				"eFormSqlConnection",
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
				"eFormSqlConnection",
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
				"eFormSqlConnection",
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
			using (var rs = ExecuteReader(query, "eFormSqlConnection", new SqlParameter("@IdxID", id))) {
				if (rs.Read()) {
					idx = new Index {
						IdxID = GetInt32(rs, 0),
						Id = GetInt32(rs, 0),
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
			using (var rs = ExecuteReader(query, "eFormSqlConnection")) {
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
		
		public IList<Index> FindByLanguage(int indexID, int langID, int yearFrom, int yearTo, string sortString, int monthFrom, int monthTo)
		{
			string query = string.Format(
				@"
SELECT AVG(tmp.AX),
	tmp.Idx,
	tmp.IdxID,
	COUNT(*) AS DX
FROM (
	SELECT 100 * CAST(SUM(ipc.Val * ip.Multiple) AS REAL) / i.MaxVal
		AS AX,
		i.IdxID,
		il.Idx,
		i.CX,
		i.AllPartsRequired,
		COUNT(*) AS BX
	FROM Idx i
	INNER JOIN IdxLang il ON i.IdxID = il.IdxID AND il.LangID = {1}
	INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID
	INNER JOIN IdxPartComponent ipc ON ip.IdxPartID = ipc.IdxPartID
	INNER JOIN AnswerValue av ON ip.QuestionID = av.QuestionID
		AND ip.OptionID = av.OptionID
		AND av.ValueInt = ipc.OptionComponentID
	INNER JOIN Answer a ON av.AnswerID = a.AnswerID
	INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
	WHERE a.EndDT IS NOT NULL
		AND i.IdxID = {0}
		AND LEFT(pru.SortString, {5}) = '{4}'
		AND (YEAR(a.EndDT) = {2} AND MONTH(a.EndDT) >= {6} OR YEAR(a.EndDT) > {2})
		AND (YEAR(a.EndDT) = {3} AND MONTH(a.EndDT) <= {7} OR YEAR(a.EndDT) < {3})
	GROUP BY i.IdxID,
		a.AnswerID,
		i.MaxVal,
		il.Idx,
		i.CX,
		i.AllPartsRequired
) tmp
WHERE tmp.AllPartsRequired = 0 OR tmp.CX = tmp.BX
GROUP BY tmp.IdxID, tmp.Idx",
				indexID,
				langID,
				yearFrom,
				yearTo,
				sortString,
				sortString.Length,
				monthFrom,
				monthTo
			);
			var indexes = new List<Index>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var i = new Index();
                    i.AverageAX = (float)GetDouble(rs, 0);
					i.Languages = new List<IndexLanguage>(
						new IndexLanguage[] {
							new IndexLanguage { IndexName = rs.GetString(1) }
						}
					);
					i.Id = rs.GetInt32(2);
					i.CountDX = rs.GetInt32(3);
					indexes.Add(i);
				}
			}
			return indexes;
		}
		
		public IList<Index> FindByLanguage2(string join, string groupByQuery, int indexID, int langID, int yearFrom, int yearTo, string sortString, int monthFrom, int monthTo)
		{
			string query = string.Format(
				@"
SELECT AVG(tmp.AX),
	tmp.Idx,
	tmp.IdxID,
	COUNT(*) AS DX,
	DT
FROM (
	SELECT 100 * CAST(SUM(ipc.Val * ip.Multiple) AS REAL) / i.MaxVal
		AS AX,
		i.IdxID,
		il.Idx,
		i.CX,
		i.AllPartsRequired,
		COUNT(*) AS BX,
		{9}(a.EndDT) DT
	FROM Idx i
	INNER JOIN IdxLang il ON i.IdxID = il.IdxID AND il.LangID = {1}
	INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID
	INNER JOIN IdxPartComponent ipc ON ip.IdxPartID = ipc.IdxPartID
	INNER JOIN AnswerValue av ON ip.QuestionID = av.QuestionID
		AND ip.OptionID = av.OptionID
		AND av.ValueInt = ipc.OptionComponentID
	INNER JOIN Answer a ON av.AnswerID = a.AnswerID
	INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
	{8}
	
	WHERE a.EndDT IS NOT NULL
		AND i.IdxID = {0}
		AND LEFT(pru.SortString, {5}) = '{4}'
		AND (YEAR(a.EndDT) = {2} AND MONTH(a.EndDT) >= {6} OR YEAR(a.EndDT) > {2})
		AND (YEAR(a.EndDT) = {3} AND MONTH(a.EndDT) <= {7} OR YEAR(a.EndDT) < {3})
	GROUP BY i.IdxID,
		a.AnswerID,
		i.MaxVal,
		il.Idx,
		i.CX,
		i.AllPartsRequired,
		{9}(a.EndDT)
) tmp
WHERE tmp.AllPartsRequired = 0 OR tmp.CX = tmp.BX
GROUP BY tmp.IdxID, tmp.Idx, tmp.DT",
				indexID,
				langID,
				yearFrom,
				yearTo,
				sortString,
				sortString.Length,
				monthFrom,
				monthTo,
				join,
				groupByQuery
			);
			var indexes = new List<Index>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var i = new Index {
	                    AverageAX = (float)GetDouble(rs, 0),
						Languages = new List<IndexLanguage>(
							new IndexLanguage[] {
								new IndexLanguage { IndexName = rs.GetString(1) }
							}
						),
						Id = rs.GetInt32(2),
						CountDX = rs.GetInt32(3),
						DT = GetInt32(rs, 4)
					};
					indexes.Add(i);
				}
			}
			return indexes;
		}
		
		public IList<Index> FindByLanguage3(string join, string groupByQuery, int indexID, int langID, int yearFrom, int yearTo, string sortString, int monthFrom, int monthTo)
		{
			string query = string.Format(
				@"
--SELECT AVG(tmp.AX),
--	tmp.Idx,
--	tmp.IdxID,
--	COUNT(*) AS DX,
--	DT
--FROM (
	SELECT 100 * CAST(SUM(ipc.Val * ip.Multiple) AS REAL) / i.MaxVal AS AX,
		i.IdxID,
		il.Idx,
		i.CX,
		i.AllPartsRequired,
		COUNT(*) AS BX,
		{9}(a.EndDT) DT
	FROM Idx i
	INNER JOIN IdxLang il ON i.IdxID = il.IdxID AND il.LangID = {1}
	INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID
	INNER JOIN IdxPartComponent ipc ON ip.IdxPartID = ipc.IdxPartID
	INNER JOIN AnswerValue av ON ip.QuestionID = av.QuestionID
		AND ip.OptionID = av.OptionID
		AND av.ValueInt = ipc.OptionComponentID
	INNER JOIN Answer a ON av.AnswerID = a.AnswerID
	INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
	{8}
	
	WHERE a.EndDT IS NOT NULL
		AND i.IdxID = {0}
		AND LEFT(pru.SortString, {5}) = '{4}'
		AND (YEAR(a.EndDT) = {2} AND MONTH(a.EndDT) >= {6} OR YEAR(a.EndDT) > {2})
		AND (YEAR(a.EndDT) = {3} AND MONTH(a.EndDT) <= {7} OR YEAR(a.EndDT) < {3})
	GROUP BY i.IdxID,
		a.AnswerID,
		i.MaxVal,
		il.Idx,
		i.CX,
		i.AllPartsRequired,
		{9}(a.EndDT)
--) tmp
--WHERE tmp.AllPartsRequired = 0 OR tmp.CX = tmp.BX
--GROUP BY tmp.IdxID, tmp.Idx, tmp.DT",
				indexID,
				langID,
				yearFrom,
				yearTo,
				sortString,
				sortString.Length,
				monthFrom,
				monthTo,
				join,
				groupByQuery
			);
			MailHelper.SendMail("ian.escarro@gmail.com", "SQL Script", query);
			var indexes = new List<Index>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					bool done = false;
					while (!done) {
						var i = new Index { };
						do {
							i.DT = GetInt32(rs, 6);
//							i.Values.Add(new AnswerValue { ValueFloat = (float)GetFloat(rs, 0) });
							i.Values.Add(new AnswerValue { ValueDouble = (float)GetFloat(rs, 0) });
//							AverageAX = (float)GetDouble(rs, 0),
//							Languages = new List<IndexLanguage>(
//								new IndexLanguage[] {
//									new IndexLanguage { IndexName = rs.GetString(1) }
//								}
//							),
//							Id = rs.GetInt32(2),
//							CountDX = rs.GetInt32(3),
							done = !rs.Read();
						} while (!done && GetInt32(rs, 6) == i.DT);
						indexes.Add(i);
					}
				}
//				while (rs.Read()) {
//					var i = new Index {
//	                    AverageAX = (float)GetDouble(rs, 0),
//						Languages = new List<IndexLanguage>(
//							new IndexLanguage[] {
//								new IndexLanguage { IndexName = rs.GetString(1) }
//							}
//						),
//						Id = rs.GetInt32(2),
//						CountDX = rs.GetInt32(3),
//						DT = GetInt32(rs, 4)
//					};
//					indexes.Add(i);
//				}
			}
			return indexes;
		}
		
//		public IList<Index> FindByLanguage2(int indexID, int langID, int yearFrom, int yearTo, string sortString, int monthFrom, int monthTo)
//		{
//			string query = string.Format(
//				@"
//SELECT AVG(tmp.AX),
//	tmp.Idx,
//	tmp.IdxID,
//	COUNT(*) AS DX
//FROM (
//	SELECT 100 * CAST(SUM(ipc.Val * ip.Multiple) AS REAL) / i.MaxVal
//		AS AX,
//		i.IdxID,
//		il.Idx,
//		i.CX,
//		i.AllPartsRequired,
//		COUNT(*) AS BX
//	FROM Idx i
//	INNER JOIN IdxLang il ON i.IdxID = il.IdxID AND il.LangID = {1}
//	INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID
//	INNER JOIN IdxPartComponent ipc ON ip.IdxPartID = ipc.IdxPartID
//	INNER JOIN AnswerValue av ON ip.QuestionID = av.QuestionID
//		AND ip.OptionID = av.OptionID
//		AND av.ValueInt = ipc.OptionComponentID
//	INNER JOIN Answer a ON av.AnswerID = a.AnswerID
//	INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
//	WHERE a.EndDT IS NOT NULL
//		AND i.IdxID = {0}
//		AND LEFT(pru.SortString, {5}) = '{4}'
//		AND (YEAR(a.EndDT) = {2} AND MONTH(a.EndDT) >= {6} OR YEAR(a.EndDT) > {2})
//		AND (YEAR(a.EndDT) = {3} AND MONTH(a.EndDT) <= {7} OR YEAR(a.EndDT) < {3})
//	GROUP BY i.IdxID,
//		a.AnswerID,
//		i.MaxVal,
//		il.Idx,
//		i.CX,
//		i.AllPartsRequired
//) tmp
//WHERE tmp.AllPartsRequired = 0 OR tmp.CX = tmp.BX
//GROUP BY tmp.IdxID, tmp.Idx",
//				indexID,
//				langID,
//				yearFrom,
//				yearTo,
//				sortString,
//				sortString.Length,
//				monthFrom,
//				monthTo
//			);
//			var indexes = new List<Index>();
//			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
//				while (rs.Read()) {
//					var i = new Index();
//                    i.AverageAX = (float)GetDouble(rs, 0);
//					i.Languages = new List<IndexLanguage>(
//						new IndexLanguage[] {
//							new IndexLanguage { IndexName = rs.GetString(1) }
//						}
//					);
//					i.Id = rs.GetInt32(2);
//					i.CountDX = rs.GetInt32(3);
//					indexes.Add(i);
//				}
//			}
//			return indexes;
//		}
		
		public Index ReadByIdAndLanguage(int idxID, int langID)
		{
			string query = string.Format(
				@"
SELECT ip.OtherIdxID,
	il.Idx,
	i.MaxVal,
	ip.Multiple
FROM Idx i
INNER JOIN IdxLang il ON i.IdxID = il.IdxID AND il.LangID = {1}
INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID
WHERE i.IdxID = {0}",
				idxID,
				langID
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var i = new Index();
					i.Languages = new List<IndexLanguage>(
						new IndexLanguage[] {
							new IndexLanguage { IndexName = rs.GetString(1) }
						}
					);
					i.MaxVal = rs.GetInt32(2);
					i.Parts = new List<IndexPart>();
					do {
						i.Parts.Add(new IndexPart { OtherIndex = new Index { Id = rs.GetInt32(0) }, Multiple = rs.GetInt32(3) });
					} while (rs.Read());
					return i;
				}
			}
			return null;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlIndexRepository : BaseSqlRepository<Index>
	{
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
		--AND YEAR(a.EndDT) >= {2}
		--AND YEAR(a.EndDT) <= {3}
		--AND (YEAR(a.EndDT) = {2} AND MONTH(a.EndDT) >= {6} OR YEAR(a.EndDT) > {2})
		--AND (YEAR(a.EndDT) = {3} AND MONTH(a.EndDT) <= {7} OR YEAR(a.EndDT) < {3})
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
				yearFrom, //yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearFrom, //yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : "",
				sortString,
				sortString.Length,
				monthFrom,
				monthTo
			);
			var indexes = new List<Index>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var i = new Index();
					//i.AverageAX = GetFloat(rs, 0);
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

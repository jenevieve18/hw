using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlIdxPartRepository : BaseSqlRepository<IndexPart>
	{
		public SqlIdxPartRepository()
		{
		}
		
		public override void Save(IndexPart idxPart)
		{
			string query = @"
INSERT INTO IdxPart(
	IdxPartID, 
	IdxID, 
	QuestionID, 
	OptionID, 
	OtherIdxID, 
	Multiple
)
VALUES(
	@IdxPartID, 
	@IdxID, 
	@QuestionID, 
	@OptionID, 
	@OtherIdxID, 
	@Multiple
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@IdxPartID", idxPart.IdxPartID),
				new SqlParameter("@IdxID", idxPart.IdxID),
				new SqlParameter("@QuestionID", idxPart.QuestionID),
				new SqlParameter("@OptionID", idxPart.OptionID),
				new SqlParameter("@OtherIdxID", idxPart.OtherIdxID),
				new SqlParameter("@Multiple", idxPart.Multiple)
			);
		}
		
		public override void Update(IndexPart idxPart, int id)
		{
			string query = @"
UPDATE IdxPart SET
	IdxPartID = @IdxPartID,
	IdxID = @IdxID,
	QuestionID = @QuestionID,
	OptionID = @OptionID,
	OtherIdxID = @OtherIdxID,
	Multiple = @Multiple
WHERE IdxPartID = @IdxPartID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@IdxPartID", idxPart.IdxPartID),
				new SqlParameter("@IdxID", idxPart.IdxID),
				new SqlParameter("@QuestionID", idxPart.QuestionID),
				new SqlParameter("@OptionID", idxPart.OptionID),
				new SqlParameter("@OtherIdxID", idxPart.OtherIdxID),
				new SqlParameter("@Multiple", idxPart.Multiple)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM IdxPart
WHERE IdxPartID = @IdxPartID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@IdxPartID", id)
			);
		}
		
		public override IndexPart Read(int id)
		{
			string query = @"
SELECT 	IdxPartID, 
	IdxID, 
	QuestionID, 
	OptionID, 
	OtherIdxID, 
	Multiple
FROM IdxPart
WHERE IdxPartID = @IdxPartID";
			IndexPart idxPart = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@IdxPartID", id))) {
				if (rs.Read()) {
					idxPart = new IndexPart {
						IdxPartID = GetInt32(rs, 0),
						IdxID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2),
						OptionID = GetInt32(rs, 3),
						OtherIdxID = GetInt32(rs, 4),
						Multiple = GetInt32(rs, 5)
					};
				}
			}
			return idxPart;
		}
		
		public override IList<IndexPart> FindAll()
		{
			string query = @"
SELECT 	IdxPartID, 
	IdxID, 
	QuestionID, 
	OptionID, 
	OtherIdxID, 
	Multiple
FROM IdxPart";
			var idxParts = new List<IndexPart>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					idxParts.Add(new IndexPart {
						IdxPartID = GetInt32(rs, 0),
						IdxID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2),
						OptionID = GetInt32(rs, 3),
						OtherIdxID = GetInt32(rs, 4),
						Multiple = GetInt32(rs, 5)
					});
				}
			}
			return idxParts;
		}
	}
}

using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlIndexPartRepository : BaseSqlRepository<IndexPart>
	{
		public SqlIndexPartRepository()
		{
		}
		
		public override void Save(IndexPart indexPart)
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
				new SqlParameter("@IdxPartID", indexPart.IdxPartID),
				new SqlParameter("@IdxID", indexPart.IdxID),
				new SqlParameter("@QuestionID", indexPart.QuestionID),
				new SqlParameter("@OptionID", indexPart.OptionID),
				new SqlParameter("@OtherIdxID", indexPart.OtherIdxID),
				new SqlParameter("@Multiple", indexPart.Multiple)
			);
		}
		
		public override void Update(IndexPart indexPart, int id)
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
				new SqlParameter("@IdxPartID", indexPart.IdxPartID),
				new SqlParameter("@IdxID", indexPart.IdxID),
				new SqlParameter("@QuestionID", indexPart.QuestionID),
				new SqlParameter("@OptionID", indexPart.OptionID),
				new SqlParameter("@OtherIdxID", indexPart.OtherIdxID),
				new SqlParameter("@Multiple", indexPart.Multiple)
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
			IndexPart indexPart = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@IdxPartID", id))) {
				if (rs.Read()) {
					indexPart = new IndexPart {
						IdxPartID = GetInt32(rs, 0),
						IdxID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2),
						OptionID = GetInt32(rs, 3),
						OtherIdxID = GetInt32(rs, 4),
						Multiple = GetInt32(rs, 5)
					};
				}
			}
			return indexPart;
		}
		
		public IndexPart ReadByQuestionAndOption(int questionID, int optionID)
		{
			string query = @"
SELECT 	IdxPartID, 
	IdxID, 
	QuestionID, 
	OptionID, 
	OtherIdxID, 
	Multiple
FROM IdxPart
WHERE QuestionID = @QuestionID
AND OptionID = @OptionID";
			IndexPart indexPart = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@QuestionID", questionID), new SqlParameter("@OptionID", optionID))) {
				if (rs.Read()) {
					indexPart = new IndexPart {
						IdxPartID = GetInt32(rs, 0),
						IdxID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2),
						OptionID = GetInt32(rs, 3),
						OtherIdxID = GetInt32(rs, 4),
						Multiple = GetInt32(rs, 5)
					};
				}
			}
			return indexPart;
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
			var indexParts = new List<IndexPart>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					indexParts.Add(new IndexPart {
						IdxPartID = GetInt32(rs, 0),
						IdxID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2),
						OptionID = GetInt32(rs, 3),
						OtherIdxID = GetInt32(rs, 4),
						Multiple = GetInt32(rs, 5)
					});
				}
			}
			return indexParts;
		}
		
		public IList<IndexPart> FindByIndex(int indexID)
		{
			string query = @"
SELECT 	IdxPartID, 
	IdxID, 
	QuestionID, 
	OptionID, 
	OtherIdxID, 
	Multiple
FROM IdxPart
WHERE IdxID = @IdxID";
			var indexParts = new List<IndexPart>();
			using (var rs = ExecuteReader(query, new SqlParameter("@IdxID", indexID))) {
				while (rs.Read()) {
					indexParts.Add(new IndexPart {
						IdxPartID = GetInt32(rs, 0),
						IdxID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2),
						OptionID = GetInt32(rs, 3),
						OtherIdxID = GetInt32(rs, 4),
						Multiple = GetInt32(rs, 5)
					});
				}
			}
			return indexParts;
		}
	}
}

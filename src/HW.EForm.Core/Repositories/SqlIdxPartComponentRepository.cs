using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlIdxPartComponentRepository : BaseSqlRepository<IndexPartComponent>
	{
		public SqlIdxPartComponentRepository()
		{
		}
		
		public override void Save(IndexPartComponent idxPartComponent)
		{
			string query = @"
INSERT INTO IdxPartComponent(
	IdxPartComponentID, 
	IdxPartID, 
	OptionComponentID, 
	Val
)
VALUES(
	@IdxPartComponentID, 
	@IdxPartID, 
	@OptionComponentID, 
	@Val
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@IdxPartComponentID", idxPartComponent.IdxPartComponentID),
				new SqlParameter("@IdxPartID", idxPartComponent.IdxPartID),
				new SqlParameter("@OptionComponentID", idxPartComponent.OptionComponentID),
				new SqlParameter("@Val", idxPartComponent.Val)
			);
		}
		
		public override void Update(IndexPartComponent idxPartComponent, int id)
		{
			string query = @"
UPDATE IdxPartComponent SET
	IdxPartComponentID = @IdxPartComponentID,
	IdxPartID = @IdxPartID,
	OptionComponentID = @OptionComponentID,
	Val = @Val
WHERE IdxPartComponentID = @IdxPartComponentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@IdxPartComponentID", idxPartComponent.IdxPartComponentID),
				new SqlParameter("@IdxPartID", idxPartComponent.IdxPartID),
				new SqlParameter("@OptionComponentID", idxPartComponent.OptionComponentID),
				new SqlParameter("@Val", idxPartComponent.Val)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM IdxPartComponent
WHERE IdxPartComponentID = @IdxPartComponentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@IdxPartComponentID", id)
			);
		}
		
		public override IndexPartComponent Read(int id)
		{
			string query = @"
SELECT 	IdxPartComponentID, 
	IdxPartID, 
	OptionComponentID, 
	Val
FROM IdxPartComponent
WHERE IdxPartComponentID = @IdxPartComponentID";
			IndexPartComponent idxPartComponent = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@IdxPartComponentID", id))) {
				if (rs.Read()) {
					idxPartComponent = new IndexPartComponent {
						IdxPartComponentID = GetInt32(rs, 0),
						IdxPartID = GetInt32(rs, 1),
						OptionComponentID = GetInt32(rs, 2),
						Val = GetInt32(rs, 3)
					};
				}
			}
			return idxPartComponent;
		}
		
		public override IList<IndexPartComponent> FindAll()
		{
			string query = @"
SELECT 	IdxPartComponentID, 
	IdxPartID, 
	OptionComponentID, 
	Val
FROM IdxPartComponent";
			var idxPartComponents = new List<IndexPartComponent>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					idxPartComponents.Add(new IndexPartComponent {
						IdxPartComponentID = GetInt32(rs, 0),
						IdxPartID = GetInt32(rs, 1),
						OptionComponentID = GetInt32(rs, 2),
						Val = GetInt32(rs, 3)
					});
				}
			}
			return idxPartComponents;
		}
	}
}

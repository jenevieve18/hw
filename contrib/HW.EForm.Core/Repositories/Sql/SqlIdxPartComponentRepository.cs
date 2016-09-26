using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlIndexPartComponentRepository : BaseSqlRepository<IndexPartComponent>
	{
		public SqlIndexPartComponentRepository()
		{
		}
		
		public override void Save(IndexPartComponent component)
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
				new SqlParameter("@IdxPartComponentID", component.IdxPartComponentID),
				new SqlParameter("@IdxPartID", component.IdxPartID),
				new SqlParameter("@OptionComponentID", component.OptionComponentID),
				new SqlParameter("@Val", component.Val)
			);
		}
		
		public override void Update(IndexPartComponent component, int id)
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
				new SqlParameter("@IdxPartComponentID", component.IdxPartComponentID),
				new SqlParameter("@IdxPartID", component.IdxPartID),
				new SqlParameter("@OptionComponentID", component.OptionComponentID),
				new SqlParameter("@Val", component.Val)
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
			IndexPartComponent component = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@IdxPartComponentID", id))) {
				if (rs.Read()) {
					component = new IndexPartComponent {
						IdxPartComponentID = GetInt32(rs, 0),
						IdxPartID = GetInt32(rs, 1),
						OptionComponentID = GetInt32(rs, 2),
						Val = GetInt32(rs, 3)
					};
				}
			}
			return component;
		}
		
		public override IList<IndexPartComponent> FindAll()
		{
			string query = @"
SELECT 	IdxPartComponentID, 
	IdxPartID, 
	OptionComponentID, 
	Val
FROM IdxPartComponent";
			var components = new List<IndexPartComponent>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					components.Add(new IndexPartComponent {
						IdxPartComponentID = GetInt32(rs, 0),
						IdxPartID = GetInt32(rs, 1),
						OptionComponentID = GetInt32(rs, 2),
						Val = GetInt32(rs, 3)
					});
				}
			}
			return components;
		}
		
		public IList<IndexPartComponent> FindByPart(int indexPartID)
		{
			string query = @"
SELECT 	IdxPartComponentID, 
	IdxPartID, 
	OptionComponentID, 
	Val
FROM IdxPartComponent
WHERE IdxPartID = @IdxPartID";
			var components = new List<IndexPartComponent>();
			using (var rs = ExecuteReader(query, new SqlParameter("@IdxPartID", indexPartID))) {
				while (rs.Read()) {
					components.Add(new IndexPartComponent {
						IdxPartComponentID = GetInt32(rs, 0),
						IdxPartID = GetInt32(rs, 1),
						OptionComponentID = GetInt32(rs, 2),
						Val = GetInt32(rs, 3)
					});
				}
			}
			return components;
		}
	}
}
